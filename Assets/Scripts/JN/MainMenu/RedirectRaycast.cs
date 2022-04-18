using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class RedirectRaycast : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera canvasCamera;

    [SerializeField] Collider canvasMeshCollider;
    

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray;
            RaycastHit hit;
            ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());


            Debug.Log(ray);
            if (Physics.Raycast(ray, out hit) && hit.collider == canvasMeshCollider)
            {
                Debug.Log("Canvas was hit");

                ray = canvasCamera.ViewportPointToRay(hit.textureCoord);
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("something inside the render texture was hit " + hit);
                }
            }
        }
            
    }

}
