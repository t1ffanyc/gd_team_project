using UnityEngine;
using UnityEngine.Events;

public class playerController : MonoBehaviour
{
    public UnityEvent nextLevel;
    public UnityEvent playerDeath;


    [SerializeField] AudioSource source;
    [SerializeField] AudioClip eatClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // reset level‑9 counters
        keysCollected = 0;
        if (IsLevel9())
        {
            var keys = GameObject.FindGameObjectsWithTag("Key");
            var uniqueRoots = new System.Collections.Generic.HashSet<GameObject>();
            foreach (var key in keys)
            {
                uniqueRoots.Add(key.transform.root.gameObject);
            }
            keysNeeded = uniqueRoots.Count;
            Debug.Log($"Level 9 detected {keysNeeded} keys needed");
        }
    }
    
    [SerializeField] private TutorialManager tutorialManager;

    private bool hasKey = false;
    // level-9 multi-key support
    private int keysNeeded = 0; // auto-detected for level 9
    private int keysCollected = 0;

    private bool IsLevel9()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 9";
    }

    void Awake()
    {
        // Auto-find if you forgot to drag it in Inspector
        if (tutorialManager == null)
            tutorialManager = FindFirstObjectByType<TutorialManager>();

        Debug.Log("TutorialManager found? " + (tutorialManager != null));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            playerDeath.Invoke();
            Debug.Log("dead");
        }
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Trigger hit: {collision.name} tag={collision.tag}");

        if (collision.CompareTag("Key"))
        {
            if (IsLevel9())
            {
                // count keys for level 9
                keysCollected++;
                source.PlayOneShot(eatClip);

                GameObject toDisable = collision.gameObject;
                if (collision.transform.parent != null)
                    toDisable = collision.transform.parent.gameObject;
                else if (collision.transform.root != collision.transform)
                    toDisable = collision.transform.root.gameObject;
                toDisable.SetActive(false);

                Debug.Log($"key collected {keysCollected}/{keysNeeded}");
            }
            else
            {
                hasKey = true;
                collision.transform.parent.gameObject.SetActive(false);
                source.PlayOneShot(eatClip);

                Debug.Log("key collected!");
            }

            if (tutorialManager != null)
            {
                Debug.Log("Calling TutorialManager.OnBananaCollected()");
                tutorialManager.OnBananaCollected();
            }
        }
        else if (collision.CompareTag("Door"))
        {
            if (IsLevel9())
            {
                if (keysNeeded > 0 && keysCollected < keysNeeded)
                {
                    Debug.Log("need more keys before entering");
                    return;
                }
            }
            else
            {
                if (!hasKey)
                    return;
            }

            Debug.Log("door reached!");
            if (tutorialManager != null)
                tutorialManager.OnDoorReached();

            // Only invoke nextLevel if not on the tutorial level
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Tutorial Level")
            {
                nextLevel.Invoke();
            }
        }
    }
}