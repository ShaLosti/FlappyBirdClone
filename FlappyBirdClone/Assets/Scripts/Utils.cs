using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utils
{
	public static void GoToScene(string name)
	{
		if (name == "Globals") return;
		var previousScene = SceneManager.GetActiveScene();
		void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			SceneManager.SetActiveScene(scene);
			if (previousScene.name != "Globals")
			{
				SceneManager.UnloadSceneAsync(previousScene.name);
				Resources.UnloadUnusedAssets();
			}
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.LoadScene(name, LoadSceneMode.Additive);
	}
	public static bool Clamp(float value, float min, float max)
	{
		return value == Mathf.Clamp(value, min, max);
	}

	public static bool Clamp(int value, int min, int max)
	{
		return value == Mathf.Clamp(value, min, max);
	}

	public static void Log(string objectName, params object[] args)
	{
		Debug.Log(objectName + ": " + args[0]);
	}
	static public void LogError(string objectName, params object[] args)
	{
		Debug.LogError(objectName + ": " + args[0]);
	}
	public static void PerformWithDelay(MonoBehaviour obj, float delay, Action func)
	{
		obj.StartCoroutine(Perform(delay, func));
	}
	public static void WaitUntilObjAlive(MonoBehaviour obj, GameObject _obj, Action func)
	{
		obj.StartCoroutine(WaitUntilObjAlive(_obj, func));
	}
	static IEnumerator Perform(float seconds, Action func)
	{
		yield return new WaitForSeconds(seconds);
		func?.Invoke();
	}
	static IEnumerator WaitUntilObjAlive(GameObject _obj, Action func)
	{
		yield return new WaitWhile(() => _obj.activeSelf);
		func?.Invoke();
	}

}
