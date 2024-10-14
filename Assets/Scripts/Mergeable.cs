using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mergeable : MonoBehaviour, IBall
{
    public int level;

    private bool canMerge;

    private List<Collider2D> collidersInContact = new List<Collider2D>();

    public int row;
    private Rigidbody2D rb;

    private void Start()
    {
        if (level == 10) GamejoltManager.instance.UnlockLastBallAchivement();
        canMerge = true;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CreationAnimation(0.2f));
    }

    private void LateUpdate()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -15, 15));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collidersInContact.Contains(collision.collider))
        {
            collidersInContact.Add(collision.collider);
            if (canMerge && collision.gameObject.TryGetComponent<Mergeable>(out var component) && component.level == level && !Manager.Instance.ballDestroyedThisFrame)
            {
                Vector2 pos = Vector3.Lerp(transform.position, collision.transform.position, 0.5f);
                Manager.Instance.CreateNewBallOnCollision(pos, level, gameObject, collision.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collidersInContact.Remove(collision.collider);
    }

    public void Destball()
    {
        if (Manager.Instance.allBalls.Contains(gameObject)) Manager.Instance.allBalls.Remove(gameObject);
        Destroy(gameObject);
    }

    public void OnOut()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;
        transform.SetParent(Manager.Instance.ballParent);
        Manager.Instance.allBalls.Add(gameObject);
    }

    public void OnCreate()
    {
        transform.SetParent(Manager.Instance.spawnPoint.transform);
        transform.position = Manager.Instance.spawnPoint.transform.position;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
    }

    public void OnCollisionCreate()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        transform.SetParent(Manager.Instance.ballParent);
        if (row > 4) GamejoltManager.instance.UnlockRowAchivement();
        Manager.Instance.allBalls.Add(gameObject);
        Manager.Instance.ballsMerged++;
        StartCoroutine(DisableRow());
    }

    private IEnumerator DisableRow()
    {
        yield return new WaitForSeconds(1.5f);
        row = 0;
    }

    private IEnumerator CreationAnimation(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float scaleProgress = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, scaleProgress);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
}
