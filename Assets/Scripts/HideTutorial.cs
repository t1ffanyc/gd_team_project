using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panelMove;
    [SerializeField] private GameObject panelBanana;
    [SerializeField] private GameObject panelDoor;
    [SerializeField] private GameObject panelCongrats;

    [Header("Next Level")]
    public UnityEvent nextLevel;

    [Header("Movement Settings")]
    [SerializeField] private int pressesToHideMovePanel = 1;
    [SerializeField] private int pressesToShowBananaPanel = 4;

    private int movePressCount = 0;

    private bool moveDone = false;
    private bool bananaDone = false;
    private bool doorDone = false;

    void Start()
    {
        panelMove.SetActive(true);
        panelBanana.SetActive(false);
        panelDoor.SetActive(false);
        panelCongrats.SetActive(false);
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        bool moved = kb.leftArrowKey.wasPressedThisFrame || 
                     kb.rightArrowKey.wasPressedThisFrame;

        if (!moved) return;

        movePressCount++;

        // Hide first panel after X presses
        if (!moveDone && movePressCount >= pressesToHideMovePanel)
        {
            moveDone = true;
            panelMove.SetActive(false);
        }

        // Show banana panel after more presses
        if (!bananaDone && movePressCount >= pressesToShowBananaPanel)
        {
            panelBanana.SetActive(true);
        }
    }

    // Call this when banana is collected
    public void OnBananaCollected()
    {
        if (bananaDone) return;
        bananaDone = true;

        panelBanana.SetActive(false);
        panelDoor.SetActive(true);

        Debug.Log("TutorialManager: panelBanana OFF, panelDoor ON");
    }

    // Call this when player reaches door
    public void OnDoorReached()
    {
        if (!bananaDone) return;
        if (doorDone) return;

        doorDone = true;

        panelDoor.SetActive(false);
        panelCongrats.SetActive(true);
    }

    public void OnNextLevelButton()
    {
        nextLevel.Invoke();
    }
}