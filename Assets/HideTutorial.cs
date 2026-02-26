using UnityEngine;

public class HideTutorialOnArrow : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;  // drag your panel here
    private bool hidden = false;

    void Update()
    {
        if (hidden) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            tutorialPanel.SetActive(false);   // hides it
            hidden = true;
        }
    }
}