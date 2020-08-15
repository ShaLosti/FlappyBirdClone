using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
    private static Level instance;

    private Action onPipePassedBird;
    public Action OnPipePassedBird { get => onPipePassedBird; set => onPipePassedBird = value; }
    public static Level Instance { get => instance; }
    public GameMode CurrentGameMode { get => currentGameMode; }

    private const float CAMERA_ORTO_SIZE = 50f;
    private const float WIDTH_PIPE = 15;
    private const int PIPE_DESTROY_X_POS = -140;
    private const int PIPE_SPAWN_X_POS = 100;

    [SerializeField]
    private SpriteRenderer backGround;
    [SerializeField]
    private DifficultData difficulties;
    [SerializeField]
    public GameMods gameMods;

    private List<GameMode> gameModeForPlay = new List<GameMode>();
    private GameMode currentGameMode;
    private int currentGameModeId = 0;

    int difficultyPipeSpawnCount = 0;
    private int currentDifficulty = 0;
    [SerializeField]
    private Transform pipePref;
    [SerializeField]
    private int pipeMoveSpeed = 3;
    [SerializeField]
    private float pipeSpawnTimerMax = .5f;
    private float pipeSpawnTimer;
    [Range(10f, 80f)]
    [SerializeField]
    private float gapSize = 50f;

    private int pipesSwapnCount = 0;

    private List<Transform> pipeList;

    private void Awake()
    {
        gameModeForPlay.AddRange(gameMods.GameModes);

        instance = this;
        pipeList = new List<Transform>();
        if(difficulties.GetLenghth() != 0)
        {
            difficultyPipeSpawnCount = difficulties[currentDifficulty].pipeSpawnedCapacity;
            SetDifficulty(difficulties[currentDifficulty]);
        }
    }
    private void Start()
    {
        ChangeGameMode();
    }
    private void Update()
    {
        if (Bird.Instance.objRigidbody2D.bodyType == RigidbodyType2D.Dynamic)
        {
            HandlePipeSpawner();
            HandlePipeMovement();
        }
    }

    private void ChangeGameMode()
    {
        currentGameModeId = UnityEngine.Random.Range(0, gameModeForPlay.Count);
        currentGameMode = gameModeForPlay[currentGameModeId];
        gameModeForPlay.Remove(currentGameMode);
        backGround.sprite = currentGameMode.backGround;
        Bird.Instance.SetNewGameMode(currentGameMode);
    }

    private void HandlePipeSpawner()
    {
        pipeSpawnTimer -= Time.deltaTime;
        if (pipeSpawnTimer < 0)
        {
            pipeSpawnTimer += pipeSpawnTimerMax;

            float heightEdgeLimit = 10;
            float totalHeight = CAMERA_ORTO_SIZE * 2f;

            float minHeight = gapSize / 2f + heightEdgeLimit;
            float maxHeight = totalHeight - gapSize / 2f - heightEdgeLimit;

            float height = UnityEngine.Random.Range(minHeight, maxHeight);

            CreateGapPipes(height, gapSize, PIPE_SPAWN_X_POS);
        }
    }

    private void SetDifficulty(Difficult difficulty)
    {
        gapSize = difficulty.gapSize;
        pipeMoveSpeed = difficulty.pipeMoveSpeed;
        pipeSpawnTimer = difficulty.pipeSpawnTimerMax;
    }

    private void HandlePipeMovement()
    {
        if (Bird.Instance.objRigidbody2D == null)
            return;

        for (int i = 0; i < pipeList.Count; i++)
        {
            if (pipeList[i] == null)
                continue;

            Transform pipe = pipeList[i];
            bool isRightFromPlrBird = pipe.position.x > Bird.Instance.objRigidbody2D.transform.position.x;

            pipe.position += new Vector3(-1, 0, 0) * pipeMoveSpeed * Time.deltaTime;

            if (isRightFromPlrBird && pipe.position.x < Bird.Instance.objRigidbody2D.transform.position.x)
                OnPipePassedBird?.Invoke();

            if (pipe.position.x < PIPE_DESTROY_X_POS)
            {
                pipeList.Remove(pipe);
                Destroy(pipe.gameObject);
                i--;
            }
        }
    }


    private void CreateGapPipes(float gapY, float gapSize, float xPos)
    {
        CreatePipe(gapY - gapSize / 2, xPos, true);
        CreatePipe(CAMERA_ORTO_SIZE * 2f - gapY - gapSize / 2, xPos, false);
        pipesSwapnCount++;
        SetDifficulty(GetDifficulty());
    }

    private Difficult GetDifficulty()
    {
        difficultyPipeSpawnCount--;
        if (difficultyPipeSpawnCount == 0 && !(currentDifficulty + 1 >= difficulties.GetLenghth()))
        {
            currentDifficulty++;
            difficultyPipeSpawnCount = difficulties[currentDifficulty].pipeSpawnedCapacity;
        }

        return difficulties[currentDifficulty];
    }

    private void CreatePipe(float height, float xPos, bool createOnBottom)
    {
        Transform pipe = Instantiate(pipePref);
        float _pipeBodyYPos;
        if (createOnBottom)
        {
            _pipeBodyYPos = -CAMERA_ORTO_SIZE;
        }
        else
        {
            _pipeBodyYPos = CAMERA_ORTO_SIZE;
            pipe.localScale = new Vector3(1, -1, 1);
        }
        pipe.position = new Vector3(xPos, _pipeBodyYPos);
        pipeList.Add(pipe);
        pipe.GetComponentInChildren<SpriteRenderer>().size = new Vector2(WIDTH_PIPE, height);
        pipe.GetComponentInChildren<BoxCollider2D>().size = new Vector2(WIDTH_PIPE, height);
        pipe.GetComponentInChildren<BoxCollider2D>().offset = new Vector2(0, height * .5f);

    }
}
