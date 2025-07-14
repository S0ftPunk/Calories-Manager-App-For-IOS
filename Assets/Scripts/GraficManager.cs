using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Globalization;

public class GraficManager : MonoBehaviour
{
    public DayPanel dayPanel;

    public Transform viewPort;

    public float speedDot = 0.35f;
    private float k;

    private string firstDate;
    private void Start()
    {
        firstDate = PlayerPrefs.GetString("FirstDate");
        k = dayPanel.oneDayPanel.GetComponent<RectTransform>().sizeDelta.y / PlayerPrefs.GetInt("MaxCalories");
        if(PlayerPrefs.GetInt("MaxCalories") > 0)
            GraficBuilder(firstDate);
    }
    private void GraficBuilder(string date)
    {
        if(PlayerPrefs.HasKey("Calories" + date))
        {
            DateTime startDate = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            int calories = PlayerPrefs.GetInt("Calories" + date);
            if (calories >= PlayerPrefs.GetInt("MaxCalories")/27f)
            {
                DayPanel day = Instantiate(dayPanel, viewPort);
                day.caloriesCount.text = calories.ToString();
                day.date.text = startDate.ToString("dd.MM");
                day.oneDayPanel.GetComponent<RectTransform>().DOSizeDelta(new Vector2(day.oneDayPanel.GetComponent<RectTransform>().sizeDelta.x, calories * k), speedDot);               
            }
            DateTime newDate = startDate.AddDays(1);
            GraficBuilder(newDate.ToString("dd.MM.yyyy"));
        }

    }
    private void OnEnable()
    {
        foreach (DayPanel item in viewPort.GetComponentsInChildren<DayPanel>())
        {
            Debug.Log(item.name);
            Destroy(item.gameObject);
        }
        GraficBuilder(firstDate);
    }
}

