using UnityEngine;
using UnityEngine.Events;

namespace FBClone.SecretLevel
{
    public class InfinityLadder : MonoBehaviour
    {
        [SerializeField]
        Enums.LadderType ladderType;
        [SerializeField]
        private OnLadderPass onLadderPassEvent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlrController>() != null)
            {
                onLadderPassEvent?.Invoke(ladderType);
            }
        }
        [System.Serializable]
        public class OnLadderPass : UnityEvent<Enums.LadderType> { }
    }
}