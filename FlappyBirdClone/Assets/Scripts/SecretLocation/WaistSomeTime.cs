using UnityEngine;

namespace FBClone.SecretLevel
{
    public class WaistSomeTime : MonoBehaviour
    {
        [SerializeField]
        SnakeController snakeController;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            snakeController.SetMode(SnakeController.SnakeModes.dabSkeleton);
        }
    }
}