using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public int timeRemaining = 45;
    public bool timerIsRunning = true;

    public TextMeshProUGUI timerText;     

    private float timerAccumulator = 0f;    

    void Update()
    {
        if (!timerIsRunning) return;

        timerAccumulator += Time.deltaTime;

        if (timerAccumulator >= 1f)
        {
            timeRemaining -= 1;              
            timerAccumulator = 0f;         

            timerText.text = timeRemaining.ToString("00"); 

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerIsRunning = false;

                //Reset puzzle or trigger failure
            }
        }
    }
}