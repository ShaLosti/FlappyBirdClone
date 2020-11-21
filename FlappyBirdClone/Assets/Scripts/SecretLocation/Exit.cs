using UnityEngine;

namespace FBClone.SecretLevel
{
    public class Exit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<PlrController>() != null)
            {
                PlayerPrefs.SetInt("SecretLvlPassed", 1);
                Loader.Load(Loader.Scene.Scene1);
            }
        }
    }
}