using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroptBoxController : MonoBehaviour
{
    float CameraXBound = 0;
    SpriteRenderer spriteRenderer;
    bool canUpdate = false;
    private void Start()
    {
        CameraXBound = CameraController.CurrentCamera.MainCamera.orthographicSize
            * CameraController.CurrentCamera.MainCamera.aspect;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (canUpdate && !spriteRenderer.isVisible)
            gameObject.SetActive(false);
    }

    public void CanUpdate()
    {
        canUpdate = true;
    }
}
