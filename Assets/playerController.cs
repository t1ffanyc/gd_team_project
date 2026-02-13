using UnityEngine;
using UnityEngine.Events;

public class playerController : MonoBehaviour
{
    public UnityEvent nextLevel;
    bool hasKey = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            hasKey = true;
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Door")
        {
            if (hasKey)
            {
                nextLevel.Invoke();
            }
        }
    }
}
