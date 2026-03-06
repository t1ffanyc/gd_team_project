using UnityEngine;
using System.Collections;

public class TimedSpike : MonoBehaviour
{
    public float startDelay = 2f;
    public float upTime = 1f;
    public float downTime = 1f;

    private Collider2D col;
    private SpriteRenderer sr;

    void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(SpikeLoop());
    }

    IEnumerator SpikeLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            // Spike out
            sr.enabled = true;
            col.enabled = true;
            yield return new WaitForSeconds(upTime);

            // Spike hidden
            sr.enabled = false;
            col.enabled = false;
            yield return new WaitForSeconds(downTime);
        }
    }
}