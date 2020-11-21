using System;
using UnityEngine;
using UnityEngine.Playables;

namespace FBClone.SecretLevel
{
    public class WaistSomeTime : MonoBehaviour
    {
        [SerializeField]
        SnakeController snakeController;
        [SerializeField]
        GameObject timeline;
        [SerializeField]
        Transform waistTimeSpawnPoint;
        [SerializeField]
        SpriteRenderer floor;
        [SerializeField]
        DroptBoxController dropBoxController;

        Transform mainCamera;
        public Transform WaistTimeSpawnPoint { get => waistTimeSpawnPoint; }

        private void Start()
        {
            mainCamera = CameraController.CurrentCamera.MainCamera.transform;
            timeline.GetComponent<PlayableDirector>().stopped += OnTimeLineEndPLaing;
        }

        private void OnTimeLineEndPLaing(PlayableDirector playableDirector)
        {
            timeline.GetComponent<PlayableDirector>().stopped -= OnTimeLineEndPLaing;
            timeline.SetActive(false);
            StartCoroutine(Coroutins.FadeIn(floor, 255, 100f));
            Vector3 moveTo = 
                new Vector3(0, mainCamera.localPosition.y, mainCamera.localPosition.z);
            StartCoroutine(Coroutins.MoveLocal(mainCamera, moveTo, 1f));
            Coroutins.eventCompleted += () => {
                PlrController.GetInstance.StopMoving(false);
                dropBoxController.CanUpdate();
                gameObject.SetActive(false);
                };
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.GetComponent<PlrController>() != null)
            {
                PlrController.GetInstance.StopMoving(true);
                snakeController.SetMode(SnakeController.SnakeModes.dabSkeleton);
                timeline.SetActive(true);

                Vector3 moveTo = new Vector3(mainCamera.localPosition.x - 1f, mainCamera.localPosition.y, mainCamera.localPosition.z);
                
                Utils.PerformWithDelay(this, 1.5f,
                    () =>
                    {
                        StartCoroutine(Coroutins.MoveLocal(mainCamera, moveTo, 1f));
                        moveTo = new Vector3(mainCamera.localPosition.x + 1f, mainCamera.localPosition.y, mainCamera.localPosition.z);
                        Utils.PerformWithDelay(this, 14.5f,
                            () => StartCoroutine(Coroutins.MoveLocal(mainCamera, moveTo, 1f)));
                    });
            }
        }
    }
}