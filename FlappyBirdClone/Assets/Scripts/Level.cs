using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    private static Level instance;

    public static Level GetInstance { get => instance; }
    public GameMode CurrentGameMode { get => currentGameMode; }

    private const float CAMERA_ORTO_SIZE = 50f;
    private const float WIDTH_PIPE = 15;
    private const int PIPE_DESTROY_X_POS = -140;
    private const int PIPE_SPAWN_X_POS = 120;
    public const int MAX_PIPES = 600; // спавн 2 пайпов это 1 очко для игрока, так что нужно пройти 600 пайпов что бы получить 300 очков

    [SerializeField]
    private SpriteRenderer backGround;

    [SerializeField]
    public GameMods gameMods;

    private List<GameMode> gameModeForPlay = new List<GameMode>();

    [SerializeField]
    private GameMode currentGameMode;

    private int currentGameModeId = 0;
    private int spawnedPipesCount = 0;

    private Difficult currentDifficulty;

    [SerializeField]
    private Transform pipePref;
    [SerializeField]
    private Transform pipeHeadPref;

    [SerializeField]
    private int pipeMoveSpeed = 3;

    [SerializeField]
    private float pipeSpawnTimerMax = .5f;

    private float pipeSpawnTimer;

    [Range(10f, 80f)]
    [SerializeField]
    private float gapSize = 50f;

    [SerializeField]
    private Transform allPipeParent;
    private Queue<Transform> pipePool = new Queue<Transform>();
    [SerializeField]
    private List<Transform> pipeList = new List<Transform>();
    private Coroutine changeColorCoroutine;
    private float waitTimeBetweenChangeMod;
    private bool gameFinished = false;
    private bool lastPipePassed = false;
    public bool GameFinished { get => gameFinished; }

    [SerializeField]
    private ChangeModeEvent onGameModeChanged;
    public ChangeModeEvent OnGameModeChanged { get => onGameModeChanged; set => onGameModeChanged = value; }

    [SerializeField]
    private UnityEvent onGameFinished;
    public UnityEvent OnGameFinished { get => onGameFinished; set => onGameFinished = value; }

    [SerializeField]
    private UnityEvent onPipePassedBird;
    public UnityEvent OnPipePassedBird { get => onPipePassedBird; set => onPipePassedBird = value; }
    private void Awake()
    {
        gameModeForPlay.AddRange(gameMods.GameModes);
        instance = this;
    }

    private void Start()
    {
        SoundManager.Init();
        ChangeGameMode();
        SetDifficulty(currentDifficulty);
    }

    private void Update()
    {
        if (Bird.GetInstance.objRigidbody2D.bodyType == RigidbodyType2D.Dynamic)
        {
            if(!gameFinished)
                HandlePipeSpawner();
            HandlePipeMovement();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ForceChangeMode();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < 200; i++)
            {
                spawnedPipesCount++;
                OnPipePassedBird?.Invoke();
            }
        }
    }

    private void ForceChangeMode()
    {
        SoundManager.StopAllSounds();
    }

    private void ChangeGameMode()
    {
        if (gameFinished)
            return;

        DeleteAllCurrentPipes();
        if (gameModeForPlay.Count == 0)
            gameModeForPlay.AddRange(gameMods.GameModes);

        do
        {
            currentGameModeId = UnityEngine.Random.Range(0, gameModeForPlay.Count);
        } while (currentGameMode.gameModeTitle == gameModeForPlay[currentGameModeId].gameModeTitle);

        Debug.Log("Prev game mode" + currentGameMode.gameModeTitle);
        currentGameMode = gameModeForPlay[currentGameModeId];
        Debug.Log("Current game mode" + currentGameMode.gameModeTitle);
        currentDifficulty = currentGameMode.difficult;
        gameModeForPlay.Remove(currentGameMode);
        backGround.sprite = currentGameMode.backGround;

        pipePref = currentGameMode.pipePref;
        pipeHeadPref = currentGameMode.pipeHeadPref;

        waitTimeBetweenChangeMod = 2f;

        OnGameModeChanged?.Invoke(currentGameMode);
        StartCoroutine(SoundManager.PlaySound(Enums.AudioSounds.BackGroundMusic, ChangeGameMode));
        backGround.sprite = currentGameMode.backGround;

        if (changeColorCoroutine != null)
            StopCoroutine(changeColorCoroutine);

        changeColorCoroutine = StartCoroutine(Coroutins.SpriteColor(backGround, currentGameMode.backGroundColor, 1f));
        SetDifficulty(currentDifficulty);
    }

    private void HandlePipeSpawner()
    {
        if (spawnedPipesCount >= MAX_PIPES)
        {
            gameFinished = true;
            SoundManager.StopAllSounds();
            return;
        }

        waitTimeBetweenChangeMod -= Time.deltaTime;
        pipeSpawnTimer -= waitTimeBetweenChangeMod < 0 ? Time.deltaTime : 0;
        if (pipeSpawnTimer < 0 && waitTimeBetweenChangeMod < 0)
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
        pipeSpawnTimerMax = difficulty.pipeSpawnTimerMax;
    }

    private void HandlePipeMovement()
    {
        if (Bird.GetInstance.objRigidbody2D == null)
            return;

        for (int i = 0; i < pipeList.Count; i++)
        {
            if (pipeList[i] == null)
                continue;

            Transform pipe = pipeList[i];
            bool isRightFromPlrBird = pipe.position.x - .3f > Bird.GetInstance.objRigidbody2D.transform.position.x;

            pipe.position += new Vector3(-1, 0, 0) * pipeMoveSpeed * Time.deltaTime;

            if (isRightFromPlrBird && pipe.position.x - .3f < Bird.GetInstance.objRigidbody2D.transform.position.x)
                OnPipePassedBird?.Invoke();

            if (!lastPipePassed && ScoreController.GetInstance.PipePassedCount >= MAX_PIPES/2)
            {
                onGameFinished?.Invoke();
                lastPipePassed = true;
            }

            if (pipe.position.x < PIPE_DESTROY_X_POS)
            {
                pipe.gameObject.SetActive(false);
                pipePool.Enqueue(pipe);
                pipeList.Remove(pipe);
                i--;
            }
        }
    }
    private void DeleteAllCurrentPipes()
    {
        pipeList.ForEach(x => Destroy(x.gameObject));
        for (int i = 0; i < pipePool.Count; i++)
            Destroy(pipePool.Dequeue().gameObject);

        pipePool.Clear();
        pipePool.TrimExcess();
        pipeList.Clear();
        pipeList.TrimExcess();
    }
    private void CreateGapPipes(float gapY, float gapSize, float xPos)
    {
        CreatePipe(gapY - gapSize / 2, xPos, true);
        CreatePipe(CAMERA_ORTO_SIZE * 2f - gapY - gapSize / 2, xPos, false);
    }

    private void CreatePipe(float height, float xPos, bool createOnBottom)
    {
        if (pipePref == null)
            return;

        Transform pipe;
        bool fromPool;
        spawnedPipesCount++;
        if (pipePool.Count == 0)
        {
            Debug.Log("Pipe instantiated");
            pipe = Instantiate(pipePref, allPipeParent);
            fromPool = false;
        }
        else
        {
            Debug.Log("Pipe from pool");
            pipe = pipePool.Dequeue();
            pipe.gameObject.SetActive(true);
            fromPool = true;
        }

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
        pipe.GetComponentInChildren<SpriteRenderer>().size = new Vector2(WIDTH_PIPE, height);
        pipe.GetComponentInChildren<BoxCollider2D>().size = new Vector2(WIDTH_PIPE, height);
        pipe.GetComponentInChildren<BoxCollider2D>().offset = new Vector2(0, height * .5f);

        if (pipeHeadPref != null)
            CreatePipeHead(pipe, xPos, createOnBottom, fromPool);

        pipeList.Add(pipe);
    }

    private void CreatePipeHead(Transform pipe, float xPos, bool createOnBottom, bool fromPool = false)
    {
        if (pipeHeadPref == null)
            return;

        Transform pipeHead;
        if (fromPool)
            pipeHead = pipe.GetChild(pipe.childCount - 1);
        else
            pipeHead = Instantiate(pipeHeadPref, pipe);

        float _pipeBodyYPos;

        if (createOnBottom)
        {
            _pipeBodyYPos = pipe.GetComponent<SpriteRenderer>().size.y;
            pipeHead.localScale = new Vector3(12, 10, 1);
        }
        else
        {
            _pipeBodyYPos = pipe.GetComponent<SpriteRenderer>().size.y;
            pipeHead.localScale = new Vector3(12, -10, 1);
        }

        pipeHead.localPosition = new Vector3(0, _pipeBodyYPos);
    }

    [Serializable]
    public class ChangeModeEvent : UnityEvent<GameMode> { }
}