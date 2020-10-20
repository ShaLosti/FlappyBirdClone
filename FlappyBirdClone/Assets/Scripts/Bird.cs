using System;
using UnityEngine;

public class Bird : MonoBehaviour, IPlrBird
{
    private static Bird instance;

    public static Bird GetInstance { get => instance; }
    public bool AllowMove { get => allowMove; set => allowMove = value; }

    private Action<GameObject> fallingDown;
    private Action<GameObject> risingUp;

    public Rigidbody2D objRigidbody2D;

    [SerializeField]
    private int jumpForce = 100;

    private bool allowMove = true;
    private bool jump = false;
    public bool autoJump = false;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        instance = this;
    }

    public void SetNativeGameMode(GameMode gameMode)
    {
        fallingDown += gameMode.BirdGoDown;
        risingUp += gameMode.BirdGoUp;
    }

    public void SetNewGameMode(GameMode gameMode, GameObject prevPlrBird)
    {
        transform.position = prevPlrBird.transform.position;
        AllowMove = prevPlrBird.GetComponent<Bird>().AllowMove;
        objRigidbody2D.bodyType = prevPlrBird.GetComponent<Rigidbody2D>().bodyType;
        objRigidbody2D.velocity = prevPlrBird.GetComponent<Rigidbody2D>().velocity;
        autoJump = prevPlrBird.GetComponent<Bird>().autoJump;
    }

    private void Update()
    {
        if (autoJump)
        {
            if (transform.position.y < 0)
                Jump();

            return;
        }

        if (!allowMove)
        {
            objRigidbody2D.bodyType = RigidbodyType2D.Static;
            return;
        }

        switch (objRigidbody2D.bodyType)
        {
            case RigidbodyType2D.Static:
                if ((Input.GetKeyDown(KeyCode.Space)
                    || Input.GetMouseButtonDown(0) || Input.touchCount > 0))
                {
                    objRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                }
                break;

            case RigidbodyType2D.Dynamic:
                if (Input.GetKeyDown(KeyCode.Space)
                    || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
                {
                    Jump();
                }
                break;
        }
    }
    private void FixedUpdate()
    {
        if (jump)
        {
            objRigidbody2D.velocity = Vector2.up * jumpForce;
            jump = false;
        }

        if (objRigidbody2D.velocity.y < 0)
            fallingDown?.Invoke(gameObject);
        else if (objRigidbody2D.velocity.y > 0)
            risingUp?.Invoke(gameObject);
    }

    private void Jump()
    {
        StartCoroutine(SoundManager.PlaySound(Enums.AudioSounds.Jump, () => { }));
        jump = true;
    }
}