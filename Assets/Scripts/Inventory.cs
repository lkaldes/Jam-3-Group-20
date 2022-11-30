using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable enable
public class Inventory : MonoBehaviour
{
    private readonly List<Item> items = new();

    public int Count
    {
        get
        {
            return items.Count;
        }
    }

    public MouseControl? Mouse;
    public GameObject? InventoryPanel;
    public Canvas? canvasContainer;
    public string inventoryInput = "Fire3";
    private Canvas? canvas;
    private RenderTexture? renderTexture;
    private RawImage? rawImage;
    private Camera? canvasCamera;
    private LayerMask inventoryLayerMask;
    public Popup? popup;
    private int inventoryLayer;

    private ItemInteraction? itemInteraction;
    public bool Opened
    {
        get
        {
            // if (canvas != null)
            // {
            //     return canvas.gameObject.activeSelf;
            // }
            if (popup != null)
            {
                return popup.Focused;
            }
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = new GameObject().AddComponent<Canvas>();
        canvas.gameObject.name = "InventoryCanvas";
        inventoryLayerMask = LayerMask.GetMask("Inventory");
        inventoryLayer = LayerMask.NameToLayer("Inventory");
        
        if (canvasContainer != null) {
            canvas.transform.SetParent(canvasContainer.transform);
        }
        rawImage = canvas.gameObject.AddComponent<RawImage>();
        rawImage.rectTransform.anchoredPosition = Vector2.zero;
        rawImage.rectTransform.anchorMin = Vector2.zero;
        rawImage.rectTransform.anchorMax = Vector2.one;
        rawImage.rectTransform.sizeDelta = Vector2.zero;

        renderTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
        rawImage.texture = renderTexture;
        
        // create and set up inventory camera
        canvasCamera = new GameObject().AddComponent<Camera>();
        canvasCamera.gameObject.name = "InventoryCamera";
        canvasCamera.orthographic = true;
        canvasCamera.cullingMask = inventoryLayerMask;
        
        canvasCamera.clearFlags = CameraClearFlags.SolidColor;
        canvasCamera.backgroundColor = Color.clear;
        
        canvasCamera.targetTexture = renderTexture;

        itemInteraction = gameObject.GetComponent<ItemInteraction>();

        if (popup != null)
        {
            popup.Events.On("opened", RenderItems);
        }

        // SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(inventoryInput))
        {
            if (popup != null)
            {
                if (popup.Focused)
                {
                    popup.Close();
                }
                else
                {
                    popup.Open();
                }
            }

        }

        if (Opened)
        {
            Ray? ray = canvasCamera?.ScreenPointToRay(Input.mousePosition);
            if (ray != null && Physics.Raycast((Ray)ray, out RaycastHit hit, 100f, inventoryLayerMask))
            {
                
                if (hit.collider)
                {
                    Item item = hit.collider.gameObject.GetComponent<Item>();
                    if (item != null && Input.GetButtonDown("Fire1"))
                    {
                        RemoveItem(item);
                        
                        if (itemInteraction != null)
                        {
                            item.Position = itemInteraction.HoldPosition;
                            item.transform.position = item.Position;
                            itemInteraction.PickUp(item);
                            item.SetVelocity(Vector3.zero);
                        } 
                        else 
                        {
                            item.Position = gameObject.transform.position;
                        }

                        popup?.Close();
                    }
                }
            }
        }
    }

    private void RenderItems()
    {
        if (canvasCamera == null)
        {
            return;
        }
        Vector3 canvasSize = canvasCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)) * 2;
        Vector3 fitProgress = new(0f, 0f, 20f);
        float padding = 1f;
        float gap = .5f;
        int index = 0;
        foreach (Item item in items)
        {
            item.SetVelocity(Vector3.zero);
            item.SetAngularVelocity(Vector3.zero);
            item.gameObject.transform.rotation = Quaternion.identity;

            if (index == 0)
            {
                Vector3 extents = item.gameObject.GetComponent<Renderer>().bounds.extents;
                fitProgress.x = extents.x - canvasSize.x / 2f + padding;
                fitProgress.y = canvasSize.y / 2f - extents.y - padding;
            }

            item.gameObject.transform.position = fitProgress;
            fitProgress.x += item.gameObject.GetComponent<Renderer>().bounds.size.x + gap;
            // item.SetCollisions(true);

            index += 1;
        }
    }

    public Item? GetItemAt(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            return items[index];
        }
        return null;
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }

    public int FindItem(Item item)
    {
        return items.FindIndex(0, Count, delegate(Item _item) { return item == _item; });
    }

    public void AddItem(Item item)
    {
        if (!HasItem(item))
        {
            items.Add(item);
            item.SetStored(true);

            if (canvasCamera != null)
            {
                // item.gameObject.layer = inventoryLayer;
                item.gameObject.transform.SetParent(canvasCamera.gameObject.transform);
            }
        }
    }

    public Item? RemoveItem(Item item)
    {
        int index = FindItem(item);
        if (index >= 0) 
        {
            return RemoveItemAt(index);
        }
        return null;
    }

    public Item? RemoveItemAt(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            Item item = items[index];
            items.RemoveAt(index);
            item.SetStored(false);
            return item;
        }
        return null;
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public void Clear()
    {
        items.Clear();
    }
}
