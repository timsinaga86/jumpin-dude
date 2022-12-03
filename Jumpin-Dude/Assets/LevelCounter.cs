using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    public static LevelCounter Singleton;

    private static TMP_Text levelDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Singleton = this;
        levelDisplay = GetComponent<TMP_Text>();
        // Initialize the display
        SetLevel(1);
    }
    private void Update()
    {
    }

    public static void SetLevel(int currLevel)
    {
        levelDisplay.text = $"Level: {currLevel}";
    }
}