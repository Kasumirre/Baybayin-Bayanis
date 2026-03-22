using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    public AudioSource wrongSound;

    //Shake Effect
    private Vector2 originalPosition;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 10f;

    void Start()
    {
        timerScript = FindObjectOfType<Timer>();

        if (words.Length > 0)
        {
            ShuffleAndStart();
        }

        originalPosition = wordText.rectTransform.anchoredPosition;
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
            wrongSound.Play();
            NextWord();
        }
        if (Input.GetMouseButtonDown(1))
        {
            wrongSound.Play();
            StartCoroutine(Shake());
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

    IEnumerator Shake()
    {
        float elapsed = 0f;
        RectTransform rect = wordText.rectTransform;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;

            rect.anchoredPosition = new Vector2(
                originalPosition.x + x,
                originalPosition.y
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = originalPosition;
    }
}