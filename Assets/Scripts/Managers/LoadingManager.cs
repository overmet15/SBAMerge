using GameJolt.API;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
	public static LoadingManager instance;

	[SerializeField]
	private Animator animator;

	void Awake()
	{
		instance = this;
	}
	
	void Start()
    {
		if (SaveValues.instance.gameData.inGameScore != 0)
		{
			if (SaveValues.instance.gameData.inGameScore > SaveValues.instance.gameData.mainModeScore) SaveValues.instance.gameData.mainModeScore = SaveValues.instance.gameData.inGameScore;
			int ass = Mathf.RoundToInt(SaveValues.instance.gameData.inGameScore / 10);
			SaveValues.instance.gameData.coinCount += ass;
			SaveValues.instance.SaveGData();
		}
		//else Debug.Log("nah");
    }

	public IEnumerator LoadScene(int sceneId = 1)
	{
		StartCoroutine(MusicManager.instance.SlowlyDeAddPitch());
		animator.SetTrigger("Go");
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(sceneId);
	}

	public async void RestartAsync()
    {
        if (Manager.Instance.score > SaveValues.instance.gameData.mainModeScore)
        {
            SaveValues.instance.gameData.mainModeScore = Manager.Instance.score;
			if (Core.instance.signedIn) Scores.Add(Manager.Instance.score, Manager.Instance.score.ToString(), 886999, "");
        }

		if (Manager.Instance.ballsMerged > SaveValues.instance.gameData.MostMergedBalls)
			SaveValues.instance.gameData.MostMergedBalls = Manager.Instance.ballsMerged;

        int CoinsToAdd = Mathf.RoundToInt(Manager.Instance.score / 10);
		SaveValues.instance.gameData.coinCount += CoinsToAdd;
		SaveValues.instance.gameData.inGameScore = 0;
        await SaveValues.instance.SaveGDataAsync();
        StartCoroutine(LoadScene());
	}
}
