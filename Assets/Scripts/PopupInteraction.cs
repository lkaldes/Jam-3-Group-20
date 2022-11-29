using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class PopupInteraction : MonoBehaviour
{
    public new Camera? camera;
    public float maxInteractionDistance = 10f;
    private Vector3 screenCenter = new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0);
    private int layerMask = 0;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask(new string[] {"Passthrough"});
    }

    // Update is called once per frame
    void Update()
    {
        if (camera == null) 
        {
            return;
        }

        Ray ray = camera.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, maxInteractionDistance, layerMask))
        {
            if (hit.collider)
            {
                Popup? popup = hit.collider.gameObject.GetComponent<Popup>();
                if (popup)
                {
                    if (Input.GetButtonDown(popup.openInput))
                    {
                        popup.SetTargetActive(true);
                    }
                }
            }
        }
    }
}
