using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ThreeDigitSceneManager : MonoBehaviour
{
    public TextMeshProUGUI wordText;
    public RawImage imageDisplay;
    public AudioSource clickSound;

    private bool isTextVisible = true;

    void Start()
    {
        if (wordText != null) wordText.gameObject.SetActive(true);
        if (imageDisplay != null) imageDisplay.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTextVisible)
            {
                if (wordText != null) wordText.gameObject.SetActive(false);
                if (imageDisplay != null) imageDisplay.gameObject.SetActive(true);

                isTextVisible = false;
                clickSound.Play();
            }
            else
            {
                SceneManager.LoadScene("RiddleScene");
            }
        }
    }
}