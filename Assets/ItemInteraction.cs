using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable enable
public class ItemInteraction : MonoBehaviour
{
    public new Camera? camera;

    public float maxPickDistance = 10f;

    public bool debug = true;
    private GameObject debugObject;
    private Image debugImage;
    public Sprite? debugSprite;

    private Vector3 screenCenter = new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (!camera) {
            // print("no camera, getting main camera");
            camera = Camera.main;
        }

        // holy. this is so nasty. i have never had so much trouble just drawing a rectangle to the screen GUH!!
        debugObject = new GameObject();
        debugImage = debugObject.AddComponent<Image>();
        debugImage.transform.SetParent(GameObject.Find("Canvas").transform);
        if (debugSprite) {
            debugImage.sprite = debugSprite;
        }
        // THIS IS HOW YOU SET THE SIZE OF A UI IMAGE?!!? https://forum.unity.com/threads/modify-the-width-and-height-of-recttransform.270993/#post-4053235
        debugImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 20f);
        debugImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!camera) {
            return;
        }

        float closestScreenDistance = 0;
        Item? closestItem = null;
        Vector3? closestScreenPosition = null;
        foreach (Item item in Item.items) 
        {
            Transform transform = item.transform;
            Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);
            screenPosition.z = 0;
            float screenDistance = (screenPosition - screenCenter).sqrMagnitude;

            float worldDistance = (transform.position - this.transform.position).magnitude;

            if (worldDistance < maxPickDistance && (!closestItem || screenDistance < closestScreenDistance)) {
                closestItem = item;
                closestScreenDistance = screenDistance;
                closestScreenPosition = screenPosition;
            }
        }

        if (closestItem) {
            
        }

        if (debug && closestItem) {
            if (!debugObject.activeSelf) {
                debugObject.SetActive(true);
            }
            if (closestScreenPosition != null) {
                debugImage.transform.position = (Vector3)closestScreenPosition;
            }
        } else {
            if (debugObject.activeSelf) {
                debugObject.SetActive(false);
            }
        }

    }
}
