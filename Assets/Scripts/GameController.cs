using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class GameController : MonoBehaviour
{

    private int gravityState = 0;
    private bool isRotating = false;
    [SerializeField] private float rotationDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics2D.gravity= new Vector2(0,-9.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnRotateLeft(InputValue value)
    {
        if(isRotating) return;
        Debug.Log("rotate left");
        gravityState = (gravityState + 1) % 4;
        RotateWorld();
    }

    void OnRotateRight(InputValue value)
    {
        if(isRotating) return;
        Debug.Log("rotate right");
        gravityState = (gravityState + 3) % 4;
        RotateWorld();
    }

    void RotateWorld()
    {
        float targetAngle = gravityState * 90f;

        Vector2 baseGravity = new Vector2(0, -9.81f);
        Vector2 newGravity = Quaternion.Euler(0, 0, targetAngle) * baseGravity;
        Physics2D.gravity = newGravity;

        StartCoroutine(SmoothRotate(targetAngle));
    }

    IEnumerator SmoothRotate(float targetAngle)
    {
        isRotating = true;

        Transform cam = Camera.main.transform;

        Quaternion startRotation = cam.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        float time = 0f;

        while (time < rotationDuration)
        {
            time += Time.deltaTime;
            float t = time / rotationDuration;

            cam.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        cam.rotation = targetRotation;
        StartCoroutine(WaitUntilSettled());
    }

    IEnumerator WaitUntilSettled()
    {
        Rigidbody2D[] bodies = FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);

        bool allSleeping = false;
        float waitTime = 0f;
        float maxWaitTime = 5f; // Adjust this value as needed

        while (!allSleeping && waitTime < maxWaitTime)
        {
            allSleeping = true;

            foreach (Rigidbody2D rb in bodies)
            {
                if (rb.bodyType != RigidbodyType2D.Dynamic)
                    continue;

                if (!rb.IsSleeping())
                {
                    allSleeping = false;
                    break;
                }
            }

            waitTime += Time.deltaTime;
            yield return null;
        }

        isRotating = false;
    }
}
