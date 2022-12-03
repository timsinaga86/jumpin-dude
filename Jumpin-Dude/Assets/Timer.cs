using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Singleton;


    public int t;
    private TMP_Text timeDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Singleton = this;
        timeDisplay = GetComponent<TMP_Text>();
        // Initialize the display
        SetTime(0);
    }
    private void Update()
    {
        SetTime((int)Time.time);
    }

    private void SetTime(int currTime)
    {
        timeDisplay.text = $"Time: {currTime}";
        if (currTime > 90)
        {
            Time.timeScale = 0;
        }
    }
}
