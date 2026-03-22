using UnityEngine;
using TMPro;

public class TimerUISetter : MonoBehaviour
{
    public TextMeshProUGUI timerText; 

    void Start()
    {
        if (UniversalTimer.Instance != null)
        {
            UniversalTimer.Instance.timerText = timerText;
        }
    }
}