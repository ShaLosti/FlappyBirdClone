using UnityEngine;

public class FastGachi : MonoBehaviour
{
    private Coroutine coroutine;

    [SerializeField]
    private Rigidbody2D rigidbody;

    private void OnEnable()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(Coroutins.ChangeGravityBetweenTwoValues(rigidbody, 35f, 45f, 1f));
    }

    private void OnDisable()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}