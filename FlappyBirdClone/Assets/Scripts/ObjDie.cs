using System;
using UnityEngine;

public class ObjDie : MonoBehaviour, ITakeDmg
{
    public Rigidbody2D objRigidbody2D;

    public Action onDie;
    public Action OnDie { get => onDie; set => onDie = value; }
    public IPlrBird Plr { get => plr; }
    public bool CanDie { get => canDie; set => canDie = value; }

    private IPlrBird plr;
    private float immortalTime = 0;
    private bool canDie = true;
    [SerializeField]
    private float immortalTimeAfterGameModeChange = 2f;

    private void Start()
    {
        plr = GetComponent<IPlrBird>();
    }
    private void OnEnable()
    {
        Level.GetInstance.OnGameModeChanged.AddListener(SetImmortalTime);
    }

    private void OnDisable()
    {
        Level.GetInstance.OnGameModeChanged.RemoveListener(SetImmortalTime);
    }

    private void Die()
    {
        StartCoroutine(SoundManager.PlaySound(Enums.AudioSounds.Die, () => { }));
        plr.AllowMove = false;
        PlayerPrefs.SetInt("CurrentPoints", 0);
        OnDie?.Invoke();
    }
    public void SetImmortalTime(GameMode _gamoMode) => immortalTime = immortalTimeAfterGameModeChange;
    private void Update()
    {
        if (immortalTime > 0)
        {
            immortalTime -= Time.deltaTime;
            canDie = false;
        }
        if (!canDie)
        {
            canDie = true;
            return;
        }
        if (plr.AllowMove && (transform.position.y < -50 || transform.position.y > 50))
            Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDoDmg>() == null)
            return;

        if(canDie)
            Die();
    }
}