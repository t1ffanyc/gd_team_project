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
        Physics2D.gravity = Quaternion.Euler(0, 0, targetAngle) * baseGravity;

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
        isRotating = false;
    }
}
