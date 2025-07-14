using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class NavBarManager : MonoBehaviour
{
    public List<GameObject> buttons;
    public List<GameObject> panels;

    public float speedButton = 0.3f;

    private int chosedIndex = 0;

    private float newYPos;
    private float oldYPos;
    private void Start()
    {
        newYPos = buttons[chosedIndex].transform.localPosition.y;
        oldYPos = buttons[chosedIndex + 1].transform.localPosition.y;
    }
    public void ShowPanel(int index)
    {
        if (index != chosedIndex) {
            buttons[index].transform.DOLocalMoveY(newYPos, speedButton);
            buttons[index].GetComponent<Image>().DOColor(Color.white, speedButton);

            buttons[chosedIndex].transform.DOLocalMoveY(oldYPos, speedButton);
            buttons[chosedIndex].GetComponent<Image>().DOColor(new Color32(225,225,225,255), speedButton);

            panels[index].SetActive(true);
            panels[chosedIndex].SetActive(false);
            chosedIndex = index;
        }
    }
}
