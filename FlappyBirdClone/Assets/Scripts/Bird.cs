using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private static Bird instance;

    private Action onDie;
    public static Bird Instance { get => instance; }
    public Action OnDie { get => onDie; set => onDie = value; }

    public Rigidbody2D objRigidbody2D;
    private const int JUMPAMPUNT = 100;

    public bool AllowMove = true;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        objRigidbody2D = GetComponent<Rigidbody2D>();
        objRigidbody2D.bodyType = RigidbodyType2D.Static;
    }
    private void OnEnable()
    {
        onDie += StopBirdMove;
    }

    private void OnDisable()
    {
        onDie -= StopBirdMove;
    }

    private void Update()
    {
        switch (objRigidbody2D.bodyType)
        {
            case RigidbodyType2D.Static:
                if ((Input.GetKeyDown(KeyCode.Space)
                    || Input.GetMouseButtonDown(0)) && AllowMove)
                {
                    objRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                }
                break;

            case RigidbodyType2D.Dynamic:
                if (Input.GetKeyDown(KeyCode.Space)
                    || Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
                break;
        }

        if (transform.position.y < -50 || transform.position.y > 50)
            OnDie?.Invoke();

    }

    private void StopBirdMove()
    {
        objRigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    private void Jump()
    {
        objRigidbody2D.velocity = Vector2.up * JUMPAMPUNT;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AllowMove = false;
        OnDie?.Invoke();
    }
}
