using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float time = 0f;
    private bool running = true;

    void Update()
    {
        if (!running) return;

        time += Time.deltaTime;
        timerText.text = "Time: " + time.ToString("F2");
    }

    public float GetTime()
    {
        return time;
    }

    public void StopTimer()
    {
        running = false;
    }
}
