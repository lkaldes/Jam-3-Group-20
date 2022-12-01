using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable enable
public class ItemInteraction : MonoBehaviour
{
    public new Camera? camera;

    public float maxPickDistance = 10f;
    public float maxScreenDistance = 1000f;
    public float holdDistance = 5f;
    public float holdBreakForce = 1000f;
    public string pickUpInput = "Fire1";
    public string storeInput = "Fire2";
    public enum PickModes
    {
        ScreenDistance,
        Raycast
    }
    public PickModes pickMode = PickModes.ScreenDistance;

    private Inventory? inventory;

    public bool debug = true;
    private GameObject? debugObject;
    private Image? debugImage;
    public Sprite? debugSprite;

    private Item? heldItem = null;
    private GameObject? holdObject;
    private Renderer? holdObjectRenderer;
    private Rigidbody? holdBody;
    private Vector3 screenCenter = new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0);
    public Mesh? debugHoldMesh;
    private Quaternion originalRotation = new();
    private Vector3 eulerRotationDifference = new();
    private Vector3 originalPosition = new();

    public Vector3 HoldPosition
    {
        get
        {
            if (camera != null)
            {
                return camera.ViewportToWorldPoint(new Vector3(.5f, .5f, holdDistance));
            }
            return Vector3.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!camera) 
        {
            camera = Camera.main;
        }

        holdObject = new GameObject();
        holdObject.name ??= "ItemInteractionHoldTarget";
        holdBody = holdObject.AddComponent<Rigidbody>();
        holdBody.useGravity = false;
        holdBody.detectCollisions = false;
        holdBody.freezeRotation = true;

        Inventory? inventoryComponent = GetComponent<Inventory>();
        if (inventoryComponent)
        {
            inventory = inventoryComponent;
        }
        
        if (debugHoldMesh)
        {
            holdObjectRenderer = holdObject.AddComponent<MeshRenderer>();
            holdObject.AddComponent<MeshFilter>().mesh = debugHoldMesh;
        }        

        // debug indicator that shows which item is being picked
        // holy. this is so nasty. i have never had so much trouble just drawing a rectangle to the screen GUH!!
        debugObject = new GameObject();
        debugObject.name ??= "ItemInteractionItemIndicator";
        debugImage = debugObject.AddComponent<Image>();
        debugImage.transform.SetParent(GameObject.Find("Canvas").transform);
        if (debugSprite) 
        {
            debugImage.sprite = debugSprite;
        }
        // THIS IS HOW YOU SET THE SIZE OF A UI IMAGE?!!? https://forum.unity.com/threads/modify-the-width-and-height-of-recttransform.270993/#post-4053235
        debugImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20f);
        debugImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        // check for required objects
        if (camera == null || holdObject == null || holdBody == null) 
        {
            return;
        }

        if (inventory != null && inventory.Opened)
        {
            return;
        }

        if (heldItem != null)
        {
            holdDistance = Mathf.Clamp(holdDistance + Input.mouseScrollDelta.y * Time.deltaTime * 50f, .1f, maxPickDistance);
        }

        // get the closest pickable item
        Item? closestItem = GetClosestItem();
        

        // check for pick up button
        // if there is an item being held, it will drop it
        // if there is no item being held, and there is an item ready to be picked up, pick itup
        if (Input.GetButtonDown(pickUpInput))
        {
            if (!heldItem && closestItem != null) 
            {
                PickUp(closestItem);
            }
            else
            {
                Drop();
            }
        }

        if (Input.GetButtonDown(storeInput))
        {
            if (heldItem != null && heldItem.storeable)
            {
                Store(heldItem);
            }
            else if (closestItem != null && closestItem.storeable)
            {
                Store(closestItem);
            }
        }       

        // set the hold body position to the position holdDistance away in front of the camera
        // this is where held items will move to when picked up
        holdBody.position = HoldPosition;

        // set the hold body's rotation the same as the player's rotation
        // this will make the item rotate with the player
        holdBody.rotation = gameObject.transform.rotation;

        

        // debug display if enabled
        if (debug) 
        {
            if (debugObject != null && debugImage != null) 
            {
                if (closestItem != null && !heldItem) 
                {
                    if (!debugObject.activeSelf) 
                    {
                        debugObject.SetActive(true);
                    }
                    Vector3 closestScreenPosition = camera.WorldToScreenPoint(closestItem.transform.position);
                    debugImage.transform.position = closestScreenPosition;
                } 
                else 
                {
                    if (debugObject.activeSelf) 
                    {
                        debugObject.SetActive(false);
                    }
                }
            }
            
            if (holdObjectRenderer != null && !holdObjectRenderer.enabled) 
            {
                holdObjectRenderer.enabled = true;
            }
        }
        else 
        {
            if (debugObject != null)
            {
                if (debugObject.activeSelf) 
                {
                    debugObject.SetActive(false);
                }
            }
            
            if (holdObjectRenderer != null && holdObjectRenderer.enabled) 
            {
                holdObjectRenderer.enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        
        if (heldItem != null && holdObject != null)
        {
            if (heldItem.rigidbody != null)
            {
                Vector3 difference = holdObject.transform.position - heldItem.rigidbody.position;
                heldItem.rigidbody.velocity = difference * 15f;

                heldItem.rigidbody.rotation = Quaternion.Euler(holdObject.transform.rotation.eulerAngles + eulerRotationDifference);
                heldItem.rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }

    public void PickUp(Item item) 
    {       
        if (heldItem != null)
        {
            Drop();
        }
        heldItem = item;
        heldItem.SetGravity(false);
        if (heldItem.rigidbody != null && holdObject != null)
        {
            originalRotation = heldItem.rigidbody.rotation;
            originalPosition = heldItem.rigidbody.position;

            holdDistance = (transform.position - originalPosition).magnitude;

            eulerRotationDifference = originalRotation.eulerAngles - holdObject.transform.rotation.eulerAngles;
        }
        
    }

    public void Drop()
    {
        if (heldItem != null)
        {
            if (heldItem.rigidbody != null)
            {
                Vector3 velocity = heldItem.rigidbody.velocity;
                if (velocity.magnitude > 10f)
                {
                    velocity = velocity.normalized * 10f;
                }
                heldItem.SetVelocity(velocity);
            }

            heldItem.SetGravity(true);
            heldItem = null;
        }
    }

    public void Store(Item item)
    {
        if (inventory != null)
        {
            Drop();

            inventory.AddItem(item);
        }
    }

    private Item? GetClosestItem()
    {
        if (camera == null)
        {
            return null;
        }

        Item? closestItem = null;
        float closestScreenDistance = 0;

        if (pickMode == PickModes.ScreenDistance) 
        {
            foreach (Item item in Item.items) 
            {
                Transform transform = item.transform;
                Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);
                screenPosition.z = 0;
                float screenDistance = (screenPosition - screenCenter).sqrMagnitude;

                float worldDistance = (transform.position - this.transform.position).magnitude;

                if (item.pickable && item.Active && worldDistance < maxPickDistance && screenDistance < maxScreenDistance && (!closestItem || screenDistance < closestScreenDistance)) 
                {
                    closestItem = item;
                    closestScreenDistance = screenDistance;
                }
            }
        }
        else if(pickMode == PickModes.Raycast)
        {
            Ray ray = camera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            if (Physics.Raycast(ray, out RaycastHit raycastHit, maxPickDistance))
            {
                Item itemComponent = raycastHit.collider.gameObject.GetComponent<Item>();
                if (raycastHit.collider && itemComponent && itemComponent.pickable && itemComponent.Active)
                {
                    Vector3 screenPosition = camera.WorldToScreenPoint(raycastHit.collider.gameObject.transform.position);
                    screenPosition.z = 0;
                    closestItem = itemComponent;
                }
            }
        }
        return closestItem;
    }
}
