using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float CAMERA_ORTO_SIZE = 50f;
    private const float WIDTH_PIPE = 15;
    [SerializeField]
    private Transform pipePref;


    private void Start()
    {
        CreatePipe(50f, 0);
        CreatePipe(50f, 20f);
    }
    private void CreatePipe(float height, float xPos)
    {
        Transform pipe = Instantiate(pipePref);

        pipe.position = new Vector3(xPos, -CAMERA_ORTO_SIZE);
        pipe.GetComponentInChildren<SpriteRenderer>().size = new Vector2(WIDTH_PIPE, height);
        pipe.GetComponentInChildren<BoxCollider2D>().size = new Vector2(WIDTH_PIPE, height);
        pipe.GetComponentInChildren<BoxCollider2D>().offset = new Vector2(0, height * .5f);

    }
}
