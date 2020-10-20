using System;
using UnityEngine;

namespace FBClone.SecretLevel
{
    public class PlrController : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        Rigidbody2D rigidbody2d;

        [SerializeField]
        Vector2 defaultNegativeVelocity = new Vector2(0, -5);
        [SerializeField]
        Vector2 defaultPositivVelocity = new Vector2(0, -5);
        Vector2 defaultPos;

        bool canMove = false;
        private bool jumpRight;
        private bool jumpLeft;
        [SerializeField]
        private Vector2Int jumpForce;

        public bool CanMove { get => canMove; set => canMove = value; }

        private void Start()
        {
            TryGetComponent<SpriteRenderer>(out spriteRenderer);
            TryGetComponent<Rigidbody2D>(out rigidbody2d);
            defaultPos = transform.position;
            if (GameInfo.GetGameMode() == null)
                return;

            spriteRenderer.sprite = GameInfo.GetGameMode().plrPref.GetComponent<SpriteRenderer>().sprite;
        }

        private void Update()
        {
            if (rigidbody2d.velocity.y < defaultNegativeVelocity.y)
            {
                defaultPos.x = rigidbody2d.velocity.x;
                defaultPos.y = defaultNegativeVelocity.y;
                rigidbody2d.velocity = defaultPos;
            }

            if (!canMove)
                return;

            if (Input.GetAxis("Horizontal") > 0)
            {
                jumpRight = true;
                canMove = false;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                jumpLeft = true;
                canMove = false;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x < Screen.width / 2f)
                    JumpLeft();
                else if (touch.position.x >= Screen.width / 2f)
                    JumpRight();
            }
        }

        private void FixedUpdate()
        {
            if (jumpRight)
                JumpRight();
            else if (jumpLeft)
                JumpLeft();
        }
        public void JumpLeft()
        {
            jumpLeft = false;
            rigidbody2d.AddForce(new Vector2(-1 * jumpForce.x, 1 * jumpForce.y), ForceMode2D.Impulse);
        }

        public void JumpRight()
        {
            jumpRight = false;
            rigidbody2d.AddForce(new Vector2(1 * jumpForce.x, 1 * jumpForce.y) , ForceMode2D.Impulse);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            canMove = true;
            if (rigidbody2d.velocity.x > defaultPositivVelocity.x)
            {
                defaultPos.x = defaultPositivVelocity.x;
                defaultPos.y = rigidbody2d.velocity.y;
                rigidbody2d.velocity = defaultPos;
            }
            else if (rigidbody2d.velocity.x < defaultNegativeVelocity.x)
            {
                defaultPos.x = defaultNegativeVelocity.x;
                defaultPos.y = rigidbody2d.velocity.y;
                rigidbody2d.velocity = defaultPos;
            }
            else if(rigidbody2d.velocity.y > defaultPositivVelocity.y)
            {
                defaultPos.x = rigidbody2d.velocity.x;
                defaultPos.y = defaultPositivVelocity.y;
                rigidbody2d.velocity = defaultPos;
            }
        }
    }
}