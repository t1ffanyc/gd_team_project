using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.Events;

public class Spike : MonoBehaviour
{
    public UnityEvent playerDeath;
    
    void Start()
    {
        
    }
    
    
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerDeath.Invoke();
        }
    }
}
