using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal destinationPortal;
    [SerializeField] private float teleportCooldown = 0.2f;
    private bool canTeleport = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canTeleport && destinationPortal != null)
        {
            StartCoroutine(Teleport(other));
        }
    }

    private IEnumerator Teleport(Collider2D player)
    {
        canTeleport = false;
        destinationPortal.canTeleport = false;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.position = destinationPortal.transform.position;

        yield return new WaitForSeconds(teleportCooldown);

        canTeleport = true;
        destinationPortal.canTeleport = true;
    }

    public void DisableTeleportTemporarily(float time)
    {
        StartCoroutine(DisableRoutine(time));
    }

    private IEnumerator DisableRoutine(float time)
    {
        canTeleport = false;
        yield return new WaitForSeconds(time);
        canTeleport = true;
    }
}
