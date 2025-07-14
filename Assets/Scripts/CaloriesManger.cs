using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CaloriesManger : MonoBehaviour
{
    public InputField gramsInput;
    public InputField caloriesInput;
    public PicturesManager picturesManager;
    public Text calsCountText;
    public Text userCalsCountText;

    private float kcal;
    public float userCalCount;
    public string todayDate;
    private int maxCalories;

    private void Start()
    {
        Application.targetFrameRate = 90;
        todayDate = DateTime.Today.ToString("dd.MM.yyyy");
        if (!PlayerPrefs.HasKey("FirstDate"))
        {
            PlayerPrefs.SetString("FirstDate", todayDate);
        }
        if (!PlayerPrefs.HasKey("MaxCalories"))
        {
            PlayerPrefs.SetInt("MaxCalories", 0);
            maxCalories = 0;
        }
        else
        {
            maxCalories = PlayerPrefs.GetInt("MaxCalories");
        }

        if (PlayerPrefs.HasKey("Calories" + todayDate))
        {
            userCalCount = PlayerPrefs.GetInt("Calories" + todayDate);
        }
        else
        {
            PlayerPrefs.SetInt("Calories" + todayDate,0);
            userCalCount = 0;
        }
        userCalsCountText.text = userCalCount.ToString();
    }
    private void CheckForMax(int kcal)
    {
        if(kcal > maxCalories)
        {
            PlayerPrefs.SetInt("MaxCalories", kcal);
            maxCalories = kcal;
        }
    }
    public void SetCalories()
    {
        if (gramsInput.text != "")
        {
            kcal = picturesManager.kcal / 100;
            userCalCount += int.Parse(gramsInput.text) * kcal;
            CheckForMax((int)userCalCount);
            PlayerPrefs.SetInt("Calories" + todayDate, (int)userCalCount);
            userCalsCountText.text = ((int)userCalCount).ToString();
            picturesManager.PicPoseAndScalereturn();
            gramsInput.text = "";
            
        }
    }
    public void SetCustomCalories()
    {
        if(caloriesInput.text != "")
        {
            userCalCount += int.Parse(caloriesInput.text);
            CheckForMax((int)userCalCount);
            PlayerPrefs.SetInt("Calories" + todayDate, (int)userCalCount);
            userCalsCountText.text = ((int)userCalCount).ToString();
            caloriesInput.text = "";
        }
    }
}
