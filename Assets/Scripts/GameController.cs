using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{

    private int gravityState = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnRotateLeft(InputValue value)
    {
        Debug.Log("rotate left");
        gravityState = (gravityState + 1) % 4;
        RotateGravity();
    }

    void OnRotateRight(InputValue value)
    {
        Debug.Log("rotate right");
        gravityState = (gravityState + 3) % 4;
        RotateGravity();
    }

    void RotateGravity()
    {
        switch (gravityState)
        {
            case 0: Physics2D.gravity = new Vector2(0, -9.81f); break; // down
            case 1: Physics2D.gravity = new Vector2(9.81f, 0); break;  // right
            case 2: Physics2D.gravity = new Vector2(0, 9.81f); break;  // up
            case 3: Physics2D.gravity = new Vector2(-9.81f, 0); break; // left
        }
    }
}
