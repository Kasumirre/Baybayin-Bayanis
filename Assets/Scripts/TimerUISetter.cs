using UnityEngine;
using TMPro;

public class TimerUISetter : MonoBehaviour
{
    public TextMeshProUGUI sceneTimerText; 

    void Start()
    {
        if (UniversalTimer.Instance != null && sceneTimerText != null)
        {
            UniversalTimer.Instance.SetTimerText(sceneTimerText);
        }
    }
}