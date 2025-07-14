using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OnEnabledDataSet : MonoBehaviour
{
    public Text calorieUserText;
    private int caloriesCount;
    private string todayDate;
    void Start()
    {
        todayDate = DateTime.Today.ToString("dd.MM.yyyy");
    }
    private void OnEnable()
    {
        caloriesCount = PlayerPrefs.GetInt("Calories" + todayDate);
        calorieUserText.text = caloriesCount.ToString();
    }
}
