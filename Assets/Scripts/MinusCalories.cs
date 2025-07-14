using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
public class MinusCalories : MonoBehaviour
{
    private int weight;
    private float[] calloriesBurn = {0.18f, 0.14f,0.13f,0.12f};
    public List<GameObject> picks;
    private int picIndex = 4;
    public List<Vector2> picksPoses;

    public Transform centerPoint;
    public float picsMoveSpeed;
    public GameObject title;
    public GameObject panelInput;
    public InputField weightOrMinutesInput;
    public InputField caloriesInput;
    public Text minutes;
    public Text inputText;
    public Text userCalsCountText;

    public float userCalCount;
    public string todayDate;

     private void Start()
    {
        todayDate = DateTime.Today.ToString("dd.MM.yyyy");

        if (!PlayerPrefs.HasKey("userWeight"))
        {
            weight = 0;
        }
        else
        {
            weight = PlayerPrefs.GetInt("userWeight");
        }
        foreach (GameObject item in picks)
        {
            picksPoses.Add(item.transform.localPosition);
        }

        if(PlayerPrefs.HasKey("Calories" + todayDate))
        {
            userCalCount = PlayerPrefs.GetInt("Calories" + todayDate);
        }
        else
        {
            PlayerPrefs.SetInt("Calories" + todayDate, 0);
            userCalCount = 0;
        }
        userCalsCountText.text = userCalCount.ToString();
    }

    public void PictureButton(int index)
    {
        if (picIndex == 4)
        {
            picIndex = index;
            picks[index].transform.DOLocalMove(centerPoint.transform.localPosition, picsMoveSpeed);
            picks[index].transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), picsMoveSpeed);
            picks[index].transform.SetAsLastSibling();
            ColorChanger(picks[index].GetComponent<Image>(), picks[index].GetComponentsInChildren<Image>()[1]);

            title.SetActive(false);
            panelInput.SetActive(true);
            float burningCalories = weight * calloriesBurn[picIndex] * 60;
            minutes.text = $"{(int)burningCalories}kcal/hour";
        }
        else
        {
            PicPoseAndScalereturn();
        }
        if(weight == 0)
        {
            inputText.text = "kg...";
            minutes.text = "Input your weight";
        }
        else
        {
            float burningCalories = weight * calloriesBurn[picIndex] * 60;
            minutes.text = $"{burningCalories}kcal/hour";
        }
    }
    public void EnterButton()
    {
        if (weightOrMinutesInput.text != "")
        {
            if (weight == 0)
            {
                weight = int.Parse(weightOrMinutesInput.text);
                PlayerPrefs.SetInt("userWeight", weight);
                inputText.text = "minutes";
                float burningCalories = weight * calloriesBurn[picIndex] * 60;
                minutes.text = $"{(int)burningCalories}kcal/hour";
            }
            else
            {
                userCalCount = PlayerPrefs.GetInt("Calories" + todayDate);
                float kcal = weight * calloriesBurn[picIndex] * int.Parse(weightOrMinutesInput.text);
                userCalCount -= kcal;
                if (userCalCount > 0)
                {
                    PlayerPrefs.SetInt("Calories" + todayDate, (int)userCalCount);
                }
                else
                {
                    userCalCount = 0;
                    PlayerPrefs.SetInt("Calories" + todayDate, (int)userCalCount);
                }
                PicPoseAndScalereturn();
                userCalsCountText.text = ((int)userCalCount).ToString();
            }
        }
    }
    public void PicPoseAndScalereturn()
    {
        if (picIndex != 4)
        {
            weightOrMinutesInput.text = "";
            picks[picIndex].transform.DOLocalMove(picksPoses[picIndex], picsMoveSpeed);
            picks[picIndex].transform.DOScale(new Vector3(1, 1, 1), picsMoveSpeed).onComplete = SetIndexInParent;

            foreach (GameObject image in picks)
            {
                image.GetComponent<Image>().DOColor(Color.white, picsMoveSpeed);
            }
        }
    }
    private void SetIndexInParent()
    {
        picks[picIndex].transform.SetSiblingIndex(picIndex);
        picIndex = 4;
        title.SetActive(true);
        panelInput.SetActive(false);
    }
    public void SetCustomMinus()
    {
        if (caloriesInput.text != "") 
        {
            userCalCount = PlayerPrefs.GetInt("Calories" + todayDate);
            userCalCount -= int.Parse(caloriesInput.text);
            if (userCalCount > 0)
            {
                PlayerPrefs.SetInt("Calories" + todayDate, (int)userCalCount);
            }
            else
            {
                userCalCount = 0;
                PlayerPrefs.SetInt("Calories" + todayDate, (int)userCalCount);
            }
            caloriesInput.text = "";
            userCalsCountText.text = ((int)userCalCount).ToString();
        }

    }
    private void ColorChanger(Image img, Image img2)
    {
        foreach (GameObject image in picks)
        {
            if (img != image.GetComponent<Image>() & img2 != image.GetComponent<Image>())
                image.GetComponent<Image>().DOColor(new Color32(100, 100, 100, 255), picsMoveSpeed/2);
            else
                image.GetComponent<Image>().DOColor(Color.white, picsMoveSpeed / 2);
        }
    }
}
