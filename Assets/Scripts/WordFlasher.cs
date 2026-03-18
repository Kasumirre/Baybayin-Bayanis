using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class WordFlasher : MonoBehaviour
{
    public TextMeshProUGUI wordText;
    public string[] words;

    private List<string> shuffledWords;
    private int currentWordIndex = 0;

    public float displayTime = 45f; 
    private float timer = 0f;

    private Timer timerScript;

    public PointsManager pointsManager;

    void Start()
    {
        timerScript = FindObjectOfType<Timer>();

        if (words.Length > 0)
        {
            ShuffleAndStart();
        }
    }

    void ShuffleAndStart()
    {
        shuffledWords = new List<string>(words);

        for (int i = 0; i < shuffledWords.Count; i++)
        {
            string temp = shuffledWords[i];
            int randomIndex = Random.Range(i, shuffledWords.Count);
            shuffledWords[i] = shuffledWords[randomIndex];
            shuffledWords[randomIndex] = temp;
        }

        currentWordIndex = 0;
        DisplayCurrentWord();
    }

    void Update()
    {
        if (shuffledWords == null || shuffledWords.Count == 0) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            NextWord();
        }

        if (Input.GetMouseButtonDown(0))
        {
            pointsManager.AddPoint();
            NextWord();
        }
    }

    void NextWord()
    {
        currentWordIndex++;

        DisplayCurrentWord();
    }

    void DisplayCurrentWord()
    {
        wordText.text = shuffledWords[currentWordIndex];
        timer = displayTime;

        if (timerScript != null)
        {
            timerScript.ResetTimer();
        }
    }
}