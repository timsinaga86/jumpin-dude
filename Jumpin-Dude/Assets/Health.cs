using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static Health Singleton;

    private static TMP_Text HealthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Singleton = this;
        HealthDisplay = GetComponent<TMP_Text>();
        // Initialize the display
        SetHealth(3);
    }
    private void Update()
    {
    }

    public static void SetHealth(int currHealth)
    {
        HealthDisplay.text = $"Health: {currHealth}";
    }
}
