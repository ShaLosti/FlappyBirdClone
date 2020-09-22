using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Coroutins
{
    public static Action eventCompleted;
    public static float timeStep = .01f;

    public static IEnumerator PerfomrmWithDelay(Action func, float time = 0)
    {
        if (time == 0)
            yield return null;
        else
            yield return new WaitForSeconds(time);

        func?.Invoke();
        Completed();
    }

    private static void Completed()
    {
        eventCompleted?.Invoke();
    }

    public static IEnumerator Move(Transform transform, Vector3 newPos, float time)
    {
        Vector3 initPos = transform.position;
        float realTime = Time.time;
        float elapsedTime = 0.0f;
        while (transform != null && elapsedTime <= time)
        {
            transform.position = Vector3.Lerp(initPos, newPos, elapsedTime / time);
            elapsedTime += Time.time - realTime;
            realTime = Time.time;
            yield return new WaitForSeconds(timeStep);
        }
        transform.position = newPos;
        Completed();
    }

    public static IEnumerator MoveRect(RectTransform transform, Vector2 newPos, float time)
    {
        Vector2 initPos = transform.anchoredPosition;
        float realTime = Time.time;
        float elapsedTime = 0.0f;
        while (transform != null && elapsedTime <= time)
        {
            transform.anchoredPosition = Vector2.Lerp(initPos, newPos, elapsedTime / time);
            elapsedTime += Time.time - realTime;
            realTime = Time.time;
            yield return new WaitForSeconds(timeStep);
        }
        transform.anchoredPosition = newPos;
        Completed();
    }

    public static IEnumerator Rotate(Transform transform, Vector3 newRotation, float time)
    {
        Vector3 initRot = transform.eulerAngles;
        transform.eulerAngles = newRotation;
        Vector3 newEulerAngles = transform.eulerAngles;
        transform.eulerAngles = initRot;
        float realTime = Time.time;
        float elapsedTime = 0.0f;
        while (transform != null && elapsedTime <= time)
        {
            transform.eulerAngles = Vector3.Lerp(initRot, newEulerAngles, elapsedTime / time);
            elapsedTime += Time.time - realTime;
            realTime = Time.time;
            yield return new WaitForSeconds(timeStep);
        }
        transform.eulerAngles = newEulerAngles;
        Completed();
    }

    public static IEnumerator FadeIn(CanvasGroup canvas, float time, float alphaValue)
    {
        float initAlpha = canvas.alpha;
        float realTime = Time.time;
        float elapsedTime = 0.0f;
        if (alphaValue > 1.0f) alphaValue = 1.0f;

        while (canvas != null && canvas.alpha < alphaValue)
        {
            canvas.alpha = Mathf.Lerp(initAlpha, alphaValue, elapsedTime / time);
            elapsedTime += Time.time - realTime;
            realTime = Time.time;
            yield return new WaitForSeconds(timeStep);
        }
        Completed();
    }

    public static IEnumerator FadeOut(CanvasGroup canvas, float time)
    {
        float initAlpha = canvas.alpha;
        float realTime = Time.time;
        float elapsedTime = 0.0f;
        while (canvas != null && canvas.alpha > 0.0f)
        {
            canvas.alpha = Mathf.Lerp(initAlpha, 0.0f, elapsedTime / time);
            elapsedTime += Time.time - realTime;
            realTime = Time.time;
            yield return new WaitForSeconds(timeStep);
        }
        Completed();
    }

    public static IEnumerator Color(Image image, Color color, float time)
    {
        var initColor = image.color;
        float realTime = Time.time;
        float elapsedTime = 0.0f;
        while (image != null && image.color != color)
        {
            image.color = UnityEngine.Color.Lerp(initColor, color, elapsedTime / time);
            elapsedTime += Time.time - realTime;
            realTime = Time.time;
            yield return new WaitForSeconds(timeStep);
        }
        Completed();
    }

    public static IEnumerator SpriteColor(SpriteRenderer spriteRenderer, Color color, float time)
    {
        var initColor = spriteRenderer.color;
        float realTime = Time.time;
        float elapsedTime = 0.0f;
        while (spriteRenderer != null && spriteRenderer.color != color)
        {
            spriteRenderer.color = UnityEngine.Color.Lerp(initColor, color, elapsedTime / time);
            elapsedTime += Time.time - realTime;
            realTime = Time.time;
            yield return new WaitForSeconds(timeStep);
        }
        Completed();
    }

    public static IEnumerator Shake(Transform transform, float time, float xRange = 0f, float yRange = 0f)
    {
        var initPos = transform.position;
        var targetPos = new Vector3(initPos.x + xRange * 0.5f, initPos.y + yRange * 0.5f, initPos.z);
        int direct = -1;
        float realTime = Time.time;
        float elapsedTime = 0.0f;
        while (transform != null)
        {
            transform.position = Vector3.Lerp(initPos, targetPos, elapsedTime / time);
            if (transform.position == targetPos)
            {
                initPos = transform.position;
                targetPos = new Vector3(initPos.x + xRange * direct, initPos.y + yRange * direct, initPos.z);
                direct = direct > 0 ? -1 : 1;
                elapsedTime = 0.0f;
            }
            elapsedTime += Time.time - realTime;
            realTime = Time.time;
            yield return new WaitForSeconds(timeStep);
        }
        Completed();
    }

    public static IEnumerator Pulse(Transform transform, float time, float xRange = 0f, float yRange = 0f)
    {
        var initScale = transform.localScale;
        var targetScale = new Vector3(initScale.x + xRange * 0.5f, initScale.y + yRange * 0.5f, initScale.z);
        int direct = -1;
        float realTime = Time.time;
        float elapsedTime = 0.0f;
        while (transform != null)
        {
            transform.localScale = Vector3.Lerp(initScale, targetScale, elapsedTime / time);
            if (transform.localScale == targetScale)
            {
                initScale = transform.localScale;
                targetScale = new Vector3(initScale.x + xRange * direct, initScale.y + yRange * direct, initScale.z);
                direct = direct > 0 ? -1 : 1;
                elapsedTime = 0.0f;
            }
            elapsedTime += Time.time - realTime;
            realTime = Time.time;
            yield return new WaitForSeconds(timeStep);
        }
        Completed();
    }

    public static IEnumerator Bounce(Transform transform, float deltaScale, float time, float damping)
    {
        var defScale = transform.localScale;
        var initScale = transform.localScale;
        var targetScale = new Vector3(initScale.x + deltaScale, initScale.y + deltaScale, initScale.z);
        float realTime = Time.time;
        float elapsedTime = 0.0f;
        int iterations = 0;
        while (transform != null && deltaScale > 0.005f)
        {
            transform.localScale = Vector3.Lerp(initScale, targetScale, elapsedTime / time);
            if (transform.localScale == targetScale)
            {
                iterations++;
                // уменьшаем
                if (iterations % 2 != 0)
                {
                    initScale = targetScale;
                    targetScale = defScale;
                    // демпенгуем и увеличиваем
                }
                else
                {
                    deltaScale = deltaScale * (1 - damping);
                    initScale = targetScale;
                    targetScale = new Vector3(defScale.x + deltaScale, defScale.y + deltaScale, defScale.z);
                }
                elapsedTime = 0.0f;
            }
            elapsedTime += Time.time - realTime;
            realTime = Time.time;
            yield return new WaitForSeconds(timeStep);
        }
        transform.localScale = defScale;
        Completed();
    }

    public static IEnumerator ChangeGravityBetweenTwoValues(Rigidbody2D rigidbody, float minGravity, float maxGravity, float addToGravity)
    {
        float time = 0;
        rigidbody.gravityScale = minGravity;
        bool getDown = true;
        while (true)
        {
            if (rigidbody.bodyType == RigidbodyType2D.Static)
            {
                time += Time.deltaTime;
                yield return new WaitForFixedUpdate();
                continue;
            }
            if (time > .5f)
            {
                time = 0f;

                if (rigidbody.gravityScale <= minGravity)
                    getDown = false;
                if (rigidbody.gravityScale >= maxGravity)
                    getDown = true;

                rigidbody.gravityScale += getDown ? -1 * addToGravity : addToGravity;
            }
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public static IEnumerator FadeOutVolume(AudioSource audioSource, float time)
    {
        float initVolume = audioSource.volume;
        float realTime = Time.time;
        float elapsedTime = 0.0f;
        while (audioSource != null && audioSource.volume > 0.0f)
        {
            audioSource.volume = Mathf.Lerp(initVolume, 0.0f, elapsedTime / time);
            elapsedTime += Time.time - realTime;
            realTime = Time.time;
            yield return new WaitForSeconds(timeStep);
        }
        Completed();
    }
}