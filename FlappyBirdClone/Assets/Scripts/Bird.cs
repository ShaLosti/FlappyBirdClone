using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D objRigidbody2D;
    private const int JUMPAMPUNT = 100;
    private void Start()
    {
        objRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)
            || Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void Jump()
    {
        objRigidbody2D.velocity = Vector2.up * JUMPAMPUNT;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
