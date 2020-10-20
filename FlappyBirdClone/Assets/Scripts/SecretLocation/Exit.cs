using UnityEngine;

namespace FBClone.SecretLevel
{

    public class Exit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<PlrController>() != null)
                Loader.Load(Loader.Scene.Scene1);
        }
    }
}