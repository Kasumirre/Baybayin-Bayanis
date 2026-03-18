using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PointsManager : MonoBehaviour
{
    public Slider pointsBar;
    public int maxPoints = 5;
    private int currentPoints = 0;

    public AudioSource pointSound;

    void Start()
    {
        if (pointsBar != null)
        {
            pointsBar.maxValue = maxPoints;
            pointsBar.value = currentPoints;
        }
    }

    public void AddPoint()
    {
        currentPoints++;

        if (currentPoints > maxPoints)
            currentPoints = maxPoints;

        if (pointsBar != null)
            pointsBar.value = currentPoints;

        if (pointSound != null)
        {
            pointSound.Play();
        }

        if (currentPoints == maxPoints)
        {
            Debug.Log("Points bar is full!");
            StartCoroutine(LoadSceneAfterDelay());
        }
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("VictoryScene");
    }
}