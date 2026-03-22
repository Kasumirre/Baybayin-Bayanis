using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UniversalTimer : MonoBehaviour
{
    public static UniversalTimer Instance;

    public float timeRemaining = 2700f; // 45 minutes
    public bool timerIsRunning = true;

    public TextMeshProUGUI timerText; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop timer if VictoryScene
        if (scene.name == "VictoryScene")
        {
            timerIsRunning = false;
        }
    }

    private void Update()
    {
        if (!timerIsRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerIsRunning = false;
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void ResetTimer()
    {
        timeRemaining = 2700f;
        timerIsRunning = true;
    }

    public void SetTimerText(TextMeshProUGUI newText)
    {
        timerText = newText;
        UpdateTimerUI(); 
    }
}