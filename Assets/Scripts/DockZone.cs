using UnityEngine;
using TMPro;

public class DockZone : MonoBehaviour
{
    public float requiredTime = 3f;

    private float timer = 0f;
    private bool inZone = false;
    private bool completed = false;

    [Header("UI References")]
    public GameObject dockTimerUI;
    public TextMeshProUGUI dockTimerText;

    public GameObject endPanel;
    public TextMeshProUGUI endText;

    public GameTimer gameTimer;
    public BoatController boatController;

    void Update()
    {
        if (inZone && !completed)
        {
            timer += Time.deltaTime;

            dockTimerUI.SetActive(true);
            dockTimerText.text = "Docking: " + (requiredTime - timer).ToString("F1");

            if (timer >= requiredTime)
            {
                CompleteDocking();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTERED: " + other.name + " | TAG: " + other.tag);

        if (other.CompareTag("Player"))
        {
            Debug.Log("CORRECT TAG - DOCKING");
            CompleteDocking();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = false;
            timer = 0f;
            dockTimerUI.SetActive(false);
        }
    }

    void CompleteDocking()
    {
        Debug.Log("DOCK COMPLETE!");

        boatController.hasDocked = true;
        gameTimer.StopTimer();
        endPanel.SetActive(true);

        float finalTime = gameTimer.GetTime();
        endText.text = "Docking Complete!\nTime: " + finalTime.ToString("F2");
    }
}
