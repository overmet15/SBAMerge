using GameJolt.API;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
	public static LoadingManager instance;

	[SerializeField]
	private Animator animator;

	private void Awake()
	{
		instance = this;
	}

	public IEnumerator LoadScene(int sceneId = 1)
	{
		StartCoroutine(SoundManager.instance.SlowlyDeAddPitch());
		animator.SetTrigger("Go");
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(sceneId);
	}

	public async void RestartAsync()
    {
        if (Manager.instance.score > SaveValues.instance.gameData.mainModeScore)
        {
            SaveValues.instance.gameData.mainModeScore = Manager.instance.score;
			if (Core.instance.signedIn) Scores.Add(Manager.instance.score, Manager.instance.score.ToString(), 886999, "");
        }
        int ass = Mathf.RoundToInt(Manager.instance.score / 10);
		SaveValues.instance.gameData.coinCount += ass;
        await SaveData.SaveGameDataAsync(SaveValues.instance.gameData);
        StartCoroutine(LoadScene());
	}
}
