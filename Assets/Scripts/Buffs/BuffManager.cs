using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
	public enum buffType
	{
		none, bomb, shake, timeFreeze
	}

	public static BuffManager instance;

	private Manager m;

	[SerializeField]
	private GameObject bombPrefab;

	public bool bombActive;

	private bool canUseBuffs;

	public float cooldown;
	public float cooldownMax;
	public float slowMoCooldown;
	public float slowMoCooldownMax;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		m = GetComponent<Manager>();
		canUseBuffs = true;
	}

	public void ActivateBuff(buffType type)
	{
		if (canUseBuffs)
		{
			switch (type)
			{
			case buffType.bomb:
				ActivateBomb();
				break;
			case buffType.timeFreeze:
				ActiveSlowMo();
				break;
			case buffType.shake:
				ActiveShake();
				break;
			}
		}
	}

	public void TryDeactivateBuff(buffType type)
	{
		if (type == buffType.bomb)
		{
			bombActive = false;
			if (m.curBall != null)
			{
				m.curBall.GetComponent<DaBomb>().DestroyWithoutBoom();
			}
			SaveValues.instance.gameData.bombBuffCount++;
			m.CreateNewBall();
		}
	}

	public void ResetBuff(buffType type)
	{
		if (type == buffType.bomb)
		{
			bombActive = false;
			m.CreateNewBall(false);
		}
	}

	private IEnumerator StartCooldown(float timer)
	{
		canUseBuffs = false;
		float elapsedTimer = timer;
		cooldownMax = timer;
		while (elapsedTimer > 0f)
		{
			elapsedTimer -= Time.unscaledDeltaTime;
			cooldown = elapsedTimer;
			yield return null;
		}
		canUseBuffs = true;
	}

	public void StartCooldownButVoidForRealLolIDunnoWhatToTypeHere()
	{
		StartCoroutine(StartCooldown(5f));
	}

	private void ActivateBomb()
	{
		if (!bombActive)
		{
			if (SaveValues.instance.gameData.bombBuffCount != 0)
			{
				SaveValues.instance.gameData.bombBuffCount--;
				bombActive = true;
				m.CreateNewBall(bombPrefab);
			}
			else
			{
				Debug.LogWarning("No");
			}
		}
		else
		{
			TryDeactivateBuff(buffType.bomb);
		}
	}

	private void ActiveSlowMo()
	{
		if (!bombActive)
		{
			if (SaveValues.instance.gameData.slowMoBuffCount != 0)
			{
				StartCoroutine(timeFreeze(15f));
				StartCooldownButVoidForRealLolIDunnoWhatToTypeHere();
			}
			else
			{
				Debug.LogWarning("No");
			}
		}
		else
		{
			Debug.LogWarning("bomb is active");
		}
	}

	private void ActiveShake()
	{
		if (!bombActive)
		{
			if (SaveValues.instance.gameData.shakeBuffCount != 0)
			{
                foreach (Shake s in FindObjectsByType<Shake>(FindObjectsSortMode.None)) s.GoShake(false);
                SaveValues.instance.gameData.shakeBuffCount--;
				StartCooldownButVoidForRealLolIDunnoWhatToTypeHere();
			}
			else
			{
				Debug.LogWarning("No");
			}
		}
		else
		{
			Debug.LogWarning("bomb is active");
		}
	}

	private IEnumerator timeFreeze(float timer)
	{
		StartCoroutine(SoundManager.instance.SlowlyDeAddPitch(0.8f));
		SaveValues.instance.gameData.slowMoBuffCount--;
		Time.timeScale = 0.5f;
		float elapsedTimer = timer;
		slowMoCooldownMax = timer;
		while (elapsedTimer > 0f)
		{
			elapsedTimer -= Time.unscaledDeltaTime;
			slowMoCooldown = elapsedTimer;
			yield return null;
		}
		StartCoroutine(SoundManager.instance.SlowlyAddPitch());
		Time.timeScale = 1f;
	}

	public int ReturnHowMuchValue(buffType type)
	{
		if (type == buffType.bomb) return SaveValues.instance.gameData.bombBuffCount;
		else if (type == buffType.shake) return SaveValues.instance.gameData.shakeBuffCount;
		else if (type == buffType.timeFreeze) return SaveValues.instance.gameData.slowMoBuffCount;
		else return 0;
    }
}
