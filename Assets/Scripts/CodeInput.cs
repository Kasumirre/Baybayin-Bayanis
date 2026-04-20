using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class CodeInput : MonoBehaviour
{
    public TextMeshProUGUI codeText;

    public TextMeshProUGUI correctText;
    public TextMeshProUGUI wrongText;

    public string correctCode = "";

    private string currentInput = "";

    public AudioSource audioSource;
    public AudioClip inputSound;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip backspaceSound;

    private Vector2 originalPosition;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 10f;

    void Start()
    {
        correctText.gameObject.SetActive(false);
        wrongText.gameObject.SetActive(false);

        originalPosition = codeText.rectTransform.anchoredPosition;

        UpdateDisplay(); 
    }

    void Update()
    {
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()) || Input.GetKeyDown(KeyCode.Keypad0 + i))
            {
                AddDigit(i.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace) && currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);

            if (audioSource != null && backspaceSound != null)
            {
                audioSource.PlayOneShot(backspaceSound);
            }

            UpdateDisplay();
        }
    }

    void AddDigit(string digit)
    {
        if (currentInput.Length >= 6) return;

        currentInput += digit;

        if (audioSource != null && inputSound != null)
        {
            audioSource.PlayOneShot(inputSound);
        }

        UpdateDisplay();

        if (currentInput.Length == 6)
        {
            CheckCode();
        }
    }

    void UpdateDisplay()
    {
        string display = "";

        for (int i = 0; i < currentInput.Length; i++)
        {
            display += currentInput[i] + " ";
        }

        for (int i = currentInput.Length; i < 6; i++)
        {
            display += "_ ";
        }

        codeText.text = display.Trim();
    }

    void CheckCode()
    {
        if (currentInput == correctCode)
        {
            Debug.Log("Correct Code!");

            correctText.gameObject.SetActive(true);
            wrongText.gameObject.SetActive(false);

            if (audioSource != null && correctSound != null)
            {
                audioSource.PlayOneShot(correctSound);
            }

            StartCoroutine(LoadSceneAfterDelay());
        }
        else
        {
            Debug.Log("Wrong Code!");

            wrongText.gameObject.SetActive(true);
            correctText.gameObject.SetActive(false);

            if (audioSource != null && wrongSound != null)
            {
                audioSource.PlayOneShot(wrongSound);
            }

            StartCoroutine(Shake());

            Invoke(nameof(ResetInput), 1f);
        }
    }

    IEnumerator Shake()
    {
        float elapsed = 0f;
        RectTransform rect = codeText.rectTransform;

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

    void ResetInput()
    {
        currentInput = "";
        UpdateDisplay();

        wrongText.gameObject.SetActive(false);
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "StoneDiskTerminalCode")
        {
            SceneManager.LoadScene("3DigitScene");
        }
        else if (currentScene == "RiddleScene")
        {
            SceneManager.LoadScene("FinalPuzzle");
        }
    }
}