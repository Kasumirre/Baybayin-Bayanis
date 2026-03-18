using UnityEngine;
using UnityEngine.UI; 

public class PointsManager : MonoBehaviour
{
    public Slider pointsBar;   
    public int maxPoints = 5; 
    private int currentPoints = 0;

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

        if (currentPoints == maxPoints)
        {
            Debug.Log("Points bar is full!");
            // To do: Trigger Successs
        }
    }

}