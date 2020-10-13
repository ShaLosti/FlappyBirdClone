using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2d;

    Vector2 defaultVelocity = new Vector2(0, -5);
    Vector2 defaultPos;

    private void Start()
    {
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        TryGetComponent<Rigidbody2D>(out rigidbody2d);
        defaultPos = transform.position;
        if (GameInfo.GetGameMode() == null)
            return;

        spriteRenderer.sprite = GameInfo.GetGameMode().plrPref.GetComponent<SpriteRenderer>().sprite;
    }

    internal void ResetPosition()
    {
        transform.position = defaultPos;
    }

    private void Update()
    {
        if(rigidbody2d.velocity.y < defaultVelocity.y)
        {
            rigidbody2d.velocity = defaultVelocity;
        }
    }
}
