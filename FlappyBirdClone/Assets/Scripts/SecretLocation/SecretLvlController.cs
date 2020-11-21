using System;
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

        [Space(10)]
        [SerializeField]
        WaistSomeTime waistSomeTimeSpawnPoint;

        private bool musicStopPlaing = false;
        Transform mainCamera;
        Coroutine coroutine;
        private void Start()
        {
            mainCamera = CameraController.CurrentCamera.MainCamera.transform;

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
        public void OnSkipLadderBtnPress()
        {
            audioSource.Stop();
        }
        public void OnGoToCutscenePress()
        {
            OnSkipLadderBtnPress();
            mainCamera.localPosition = new Vector3(0, .5f, -1);
            Vector3 plrPos = PlrController.GetInstance.transform.position;
            Vector3 targetPos = waistSomeTimeSpawnPoint.WaistTimeSpawnPoint.position;
            PlrController.GetInstance.transform.position = targetPos;
        }
#warning END_DEBUG
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
            Vector2 topLadderPos;
            Vector2 botLadderPos;
            Vector2 midLadderPos;
            switch (currentLadderTypePassed)
            {
                case Enums.LadderType.middle:

                    topLadderPos = topLadder.transform.localPosition;
                    topLadderPos.y = topLadderPos.y + topLadderOffset.y;
                    topLadder.transform.localPosition = topLadderPos;

                    botLadderPos = botLadder.transform.localPosition;
                    botLadderPos.y = botLadderPos.y + midLadderOffset.y / 2;
                    botLadder.transform.localPosition = botLadderPos;

                    break;
                case Enums.LadderType.top:

                    midLadderPos = midLadder.transform.localPosition;
                    midLadderPos.y = midLadderPos.y + midLadderOffset.y;
                    midLadder.transform.localPosition = midLadderPos;

                    botLadderPos = botLadder.transform.localPosition;
                    botLadderPos.y = botLadderPos.y + topLadderOffset.y / 2;
                    botLadder.transform.localPosition = botLadderPos;

                    break;
            }
        }
    }
}