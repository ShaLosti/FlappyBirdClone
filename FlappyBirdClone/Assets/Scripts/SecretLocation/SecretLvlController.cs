using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBClone.SecretLevel
{
    public class SecretLvlController : MonoBehaviour
    {
        [SerializeField]
        AudioSource audioSource;
        [Header("Infinity ladder loop")]
        [SerializeField]
        Transform topLadder;
        [SerializeField]
        Transform midLadder;
        [SerializeField]
        Transform botLadder;
        [Space(10)]
        [SerializeField]
        Vector2 topLadderOffset;
        [SerializeField]
        Vector2 midLadderOffset;
        [SerializeField]
        Transform[] allTopLadderBricks;
        [SerializeField]
        Transform[] allMiddleLadderBricks;
        private bool musicStopPlaing = false;
        Transform mainCamera;
        Coroutine coroutine;
        private void Start()
        {
            mainCamera = Camera.main.transform;
            audioSource.Play();
            musicStopPlaing = false;
            audioSource.volume = 0;
            StartCoroutine(Coroutins.FadeVolume(audioSource, 1f, 1f));
            topLadderOffset = new Vector2(0, 0);
            midLadderOffset = new Vector2(0, 0);
            foreach (var item in allTopLadderBricks)
            {
                topLadderOffset.y += item.GetComponent<SpriteRenderer>().bounds.size.y;
            }
            foreach (var item in allMiddleLadderBricks)
            {
                midLadderOffset.y += item.GetComponent<SpriteRenderer>().bounds.size.y;
            }
            float _temp = topLadderOffset.y;
            topLadderOffset.y += midLadderOffset.y;
            midLadderOffset.y += _temp;

            topLadderOffset.y *= -1;
            midLadderOffset.y *= -1;
        }
#warning DEBUG
        public void OnScipLadderBtnPress()
        {
            audioSource.Stop();
        }
        public void LadderHandler(Enums.LadderType currentLadderTypePassed)
        {
            if (musicStopPlaing)
            {
                if (coroutine == null)
                    coroutine = StartCoroutine(Coroutins.MoveLocal(mainCamera, new Vector3(0, .5f, -1), 1.5f));

                return;
            }

            if (audioSource.time + 8f >= audioSource.clip.length || !audioSource.isPlaying)
            {
                musicStopPlaing = true;
                LadderHandler(currentLadderTypePassed);
                return;
            }

            switch (currentLadderTypePassed)
            {
                case Enums.LadderType.middle:
                    topLadder.transform.localPosition = new Vector2(topLadder.transform.localPosition.x,
                        topLadder.transform.localPosition.y + topLadderOffset.y);

                    botLadder.transform.localPosition = new Vector2(botLadder.transform.localPosition.x,
                        botLadder.transform.localPosition.y + midLadderOffset.y / 2);
                    break;
                case Enums.LadderType.top:
                    midLadder.transform.localPosition = new Vector2(midLadder.transform.localPosition.x,
                        midLadder.transform.localPosition.y + midLadderOffset.y);

                    botLadder.transform.localPosition = new Vector2(botLadder.transform.localPosition.x,
                        botLadder.transform.localPosition.y + topLadderOffset.y / 2);
                    break;
            }
        }
    }
}