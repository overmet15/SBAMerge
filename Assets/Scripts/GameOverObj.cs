using System.Collections.Generic;
using UnityEngine;

public class GameOverObj : MonoBehaviour
{
    private List<Mergeable> ballsInside = new();
    [SerializeField] private CanvasGroup group;
    [SerializeField] private float timer;
    [SerializeField] private GameOverManager gameOverManager;
    private float time;
    private bool done;
    void Update()
    {
        group.alpha = time / timer;
        if (ballsInside.Count > 0) time += Time.deltaTime;
        else time = 0;
        if (time > timer && !done) { done = true; gameOverManager.GameOver(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Mergeable>() != null)
        {
            Mergeable mergeable = collision.GetComponent<Mergeable>();
            if (!ballsInside.Contains(mergeable)) ballsInside.Add(mergeable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Mergeable>() != null)
        {
            Mergeable mergeable = collision.GetComponent<Mergeable>();
            if (ballsInside.Contains(mergeable)) ballsInside.Remove(mergeable);
        }
    } 
}
