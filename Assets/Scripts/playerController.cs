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
        
    }
    
    [SerializeField] private TutorialManager tutorialManager;

    private bool hasKey = false;

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
            hasKey = true;

            collision.transform.parent.gameObject.SetActive(false);
            source.PlayOneShot(eatClip);

            Debug.Log("key collected!");

            if (tutorialManager != null)
            {
                Debug.Log("Calling TutorialManager.OnBananaCollected()");
                tutorialManager.OnBananaCollected();
            }

            // Disable the whole key (parent). If your trigger is nested deeper, use root.
            if (collision.transform.parent != null)
                collision.transform.parent.gameObject.SetActive(false);
            else
                collision.transform.root.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Door"))
        {
            if (hasKey)
            {
                Debug.Log("door reached!");
                if (tutorialManager != null)
                    tutorialManager.OnDoorReached();

                // Only invoke nextLevel if not on the tutorial level
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Tutuorial Level")
                {
                    nextLevel.Invoke();
                }
            }
        }
    }
}