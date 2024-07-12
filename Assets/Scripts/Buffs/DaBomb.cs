using System.Collections;
using UnityEngine;

public class DaBomb : MonoBehaviour, IBall
{
	[SerializeField]
	private float forceRadius;

	[SerializeField]
	private float destRadius;

	[SerializeField]
	private float force;

	[SerializeField]
	private float upForce;

	[SerializeField]
	private GameObject particlesPrefab;

	[SerializeField]
	private float timer;

	[SerializeField]
	private Transform explodePos;

	private void Start()
	{
		Manager.instance.canCreateNewBall = false;
	}

	private void Explode()
	{
		Collider2D[] forceCols = Physics2D.OverlapCircleAll(explodePos.position, forceRadius, LayerMask.GetMask("Balls"));

		foreach (Collider2D collider2D in forceCols)
		{
			Vector2 vector = explodePos.position - transform.position;
			vector.Normalize();
			collider2D.GetComponent<Rigidbody2D>().AddForce(vector * force + Vector2.up * upForce);
		}

		Collider2D[] destroyCols = Physics2D.OverlapCircleAll(explodePos.position, destRadius, LayerMask.GetMask("Balls"));
		foreach (Collider2D collider2D2 in destroyCols) collider2D2.GetComponent<Mergeable>().Destball();

		Instantiate(particlesPrefab).transform.position = transform.position;
		Manager.instance.canCreateNewBall = true;
        Manager.instance.CreateNewBall(false);
		foreach (Shake s in FindObjectsOfType<Shake>()) s.GoShake(true);
		Destroy(gameObject);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawWireSphere(explodePos.position, destRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(explodePos.position, forceRadius);
	}

	public IEnumerator WaitExplode()
	{
		yield return new WaitForSeconds(timer);
		Explode();
	}

	public void DestroyWithoutBoom()
	{
		FindObjectOfType<Manager>().canCreateNewBall = true;
		Destroy(gameObject);
	}

	public void OnCreate()
	{
		transform.SetParent(Manager.instance.spawnPoint.transform);
		transform.position = Manager.instance.spawnPoint.transform.position;
		GetComponent<CircleCollider2D>().enabled = false;
		GetComponent<Rigidbody2D>().simulated = false;
	}

	public void OnOut()
	{
		transform.SetParent(null);
		GetComponent<CircleCollider2D>().enabled = true;
		GetComponent<Rigidbody2D>().simulated = true;
		BuffManager.instance.ResetBuff(BuffManager.buffType.bomb);
		BuffManager.instance.StartCooldownButVoidForRealLolIDunnoWhatToTypeHere();
		StartCoroutine(WaitExplode());
	}
}
