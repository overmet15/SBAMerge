using System;
using System.Collections;
using System.Collections.Generic;
using GameJolt.API;
using TMPro;
using UnityEngine;
public interface IBall
{
    public void OnOut();
    public void OnCreate();
}
public class Manager : MonoBehaviour
{
	public static Manager instance;
	public Transform ballParent;

	[Header("Spawn Point Stuff")]
	public GameObject spawnPoint;

	private float leftLimit;
	private float rightLimit;
	private Vector2 targetPosition;
	[SerializeField] private float[] leftBallBorderLimits;
	[SerializeField] private float[] rightBallBorderLimits;
	[SerializeField] private float speed;

	[Header("Balls")]
    	public GameObject[] ballList;
	private bool ballCreatedThisFrame;
	public bool ballDestroyedThisFrame;
	public GameObject curBall;
	[HideInInspector] 
	public List<GameObject> allBalls;
	public bool canCreateNewBall;
	[SerializeField]
	private GameObject creationParticles;
    	[SerializeField] private ParticleSystem spawnParticles;

    	[SerializeField] private SpriteRenderer nextBallSRender;
	[SerializeField] private Sprite[] nextBallSprites;

	[Header("Score")]
	public GameObject row;
	public int score;
	private bool hasCreated;
	private bool down;
	public bool canPlay;
	private int nextBallLv = 0;
	private float forKeyboard;
	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		Time.timeScale = 1f;
		canCreateNewBall = true;
		canPlay = true;
		CreateNewBall();
        	StartCoroutine(UpdateActivity());
    	}

	private void LateUpdate()
	{
		nextBallSRender.sprite = nextBallSprites[nextBallLv];
		ballCreatedThisFrame = false;
	}

	private void Update()
	{
		if (!SaveValues.instance.optionsData.keyboardControls)
		{
			HandleInputMouse();
		}
		else
		{
			HandleInputKeyboard();
		}
	}

	private void HandleInputMouse()
	{
		if (down && canPlay)
		{
			targetPosition = new Vector2(Mathf.Clamp(Camera.main.ScreenPointToRay(Input.mousePosition).origin.x, leftLimit, rightLimit), 4f);
			spawnPoint.transform.position = Vector2.Lerp(spawnPoint.transform.position, targetPosition, speed * Time.deltaTime);
		}
	}

	private void HandleInputKeyboard()
	{
		if (canPlay)
		{
			float axis = Input.GetAxis("Horizontal") / 4;
			forKeyboard += axis / 15f;
			forKeyboard = Mathf.Clamp(forKeyboard, leftLimit, rightLimit);
			spawnPoint.transform.position = new Vector2(forKeyboard, 4f);
			if (Input.GetKeyDown(KeyCode.Space))
			{
				MouseUp();
			}
		}
	}

	public void CreateNewBallOnCollision(Vector2 pos, int lvl, GameObject first, GameObject second)
	{
		if (!ballCreatedThisFrame)
		{
			int num = lvl + 1;
			if (num > ballList.Length - 1)
			{
				num = 0;
			}
			GameObject ball = Instantiate(ballList[num]);
            ball.transform.position = pos;
			Mergeable mergable = ball.GetComponent<Mergeable>();
            mergable.row = first.GetComponent<Mergeable>().row + 1;
			if (mergable.row >= 2)
			{
				GameObject rowObj = Instantiate(row);
                rowObj.transform.position = pos + Vector2.up * 1.5f;
				score += 15 * mergable.row;
                rowObj.GetComponent<RowText>().CreateRow(mergable.row, 15 * mergable.row);
			}
            mergable.OnCollisionCreate();
			Instantiate(creationParticles).transform.position = pos;
			score += 5 * num;
			first.GetComponent<Mergeable>().Destball();
			second.GetComponent<Mergeable>().Destball();
			ballCreatedThisFrame = true;
		}
	}

	public void CreateNewBall(bool randomizeNext = true)
	{
		if (canPlay && canCreateNewBall)
		{
			curBall = Instantiate(ballList[nextBallLv]);
			spawnParticles.Play();
			leftLimit = leftBallBorderLimits[nextBallLv];
			rightLimit = rightBallBorderLimits[nextBallLv];
			if (randomizeNext) nextBallLv = RaandomBallChose();
			curBall.TryGetComponent<IBall>(out var component);
			component.OnCreate();
			hasCreated = true;
		}
	}

	public void CreateNewBall(GameObject prefab, float leftBorderLimit = -3f, float rightBorderLimit = 3f)
	{
		if (canPlay && canCreateNewBall)
		{
			if (curBall != null)
			{
				curBall.GetComponent<Mergeable>().Destball();
			}
			leftLimit = leftBorderLimit;
			rightLimit = rightBorderLimit;
            curBall = Instantiate(prefab);
			curBall.TryGetComponent<IBall>(out var component);
			component.OnCreate();
			hasCreated = true;
		}
	}

	private int RaandomBallChose()
	{
		int num = UnityEngine.Random.Range(0, 100);
		if (num < 45) return 0;
		else if (num < 75) return 1;
		else if (num < 95) return 2;
		else return 3;
	}

	public void MouseDown() { down = true; }

	public void MouseUp()
	{
		if (!SaveValues.instance.optionsData.keyboardControls) down = false;
		if (hasCreated && canPlay) OutBall();
	}

	private void OutBall()
	{
		StartCoroutine(WaitASecond());
		curBall.TryGetComponent<IBall>(out var component);
		component.OnOut();
		hasCreated = false;
		curBall = null;
	}

	private IEnumerator WaitASecond()
	{
		yield return new WaitForSecondsRealtime(0.5f);
		if (!hasCreated)
		{
			CreateNewBall();
		}
		else
		{
			StartCoroutine(WaitASecond());
		}
	}
	IEnumerator UpdateActivity()
	{
#if !UNITY_ANDROID
		DiscordManager.UpdateActivity($"score: {score}, $"Hi-Score: {SaveValues.instance.gameData.mainModeScore}");
#endif
        yield return new WaitForSeconds(15);
		StartCoroutine(UpdateActivity());
	}
}
