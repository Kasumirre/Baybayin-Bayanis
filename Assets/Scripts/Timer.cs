using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public int timeRemaining = 45;
    public bool timerIsRunning = true;

    public TextMeshProUGUI timerText;

    private float timerAccumulator = 0f;

    // Audio sources
    public AudioSource secondTickSound;  
    public AudioSource last10SecondsSound;

    public WordFlasher wordFlasher;
    private bool isPaused = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            isPaused = !isPaused;
        }

        if (!timerIsRunning || isPaused) return;

        timerAccumulator += Time.deltaTime;

        if (timerAccumulator >= 1f)
        {
            timeRemaining -= 1;
            timerAccumulator = 0f;

            timerText.text = timeRemaining.ToString("00");

            if (timeRemaining > 10)
            {
                secondTickSound.Play();
            }
            else if (timeRemaining > 0.1)
            {
                last10SecondsSound.Play();
            }

            if (timeRemaining <= 0)
            {
                if (wordFlasher != null)
                {
                    wordFlasher.OnTimeUp();
                }

                ResetTimer();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            ResetTimer();
        }
    }

    public void ResetTimer()
    {
        timeRemaining = 45;
        timerAccumulator = 0f;
        timerIsRunning = true;
        timerText.text = timeRemaining.ToString("00");
    }
}