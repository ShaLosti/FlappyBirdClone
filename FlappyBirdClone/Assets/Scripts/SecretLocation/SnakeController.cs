using UnityEngine;

namespace FBClone.SecretLevel
{
    public class SnakeController : MonoBehaviour
    {
        [SerializeField]
        GameObject snakeSkeleton;
        [SerializeField]
        GameObject snakeSkeletonDab;

        internal void SetMode(SnakeModes state)
        {
            switch (state)
            {
                case SnakeModes.dabSkeleton:
                    snakeSkeleton.SetActive(false);
                    snakeSkeletonDab.SetActive(true);
                    break;
                case SnakeModes.normalSkeleton:
                    snakeSkeleton.SetActive(true);
                    snakeSkeletonDab.SetActive(false);
                    break;
            }
        }

        internal enum SnakeModes
        {
            normalSkeleton,
            dabSkeleton,
        }
    }
}