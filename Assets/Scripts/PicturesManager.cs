using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PicturesManager : MonoBehaviour
{
    public GameObject leftButton, rightButton;
    static public int puckIndex;
    public List<GameObject> pucks;
    public List<GameObject> picks;
    public List<Vector2> picksPoses;
    public Transform leftPoint, rightPoint,centerPoint;
    public float swipeSpeed;
    public float picsMoveSpeed;
    public int picIndex = 4;
    public Text calsCountText;
    public int prevIndex;

    private float[,] callories = { {130,165,35,68 },{ 206, 160, 120, 176 },{ 380, 165, 450, 50 } } ;
    public GameObject panelButtons, panelInput;

    public float kcal;
    private void Start()
    {
        CheckForButtons();
        FillPicsList();
        foreach(GameObject item in picks)
        {
            picksPoses.Add(item.transform.localPosition);
        }
    }

    private void FillPicsList()
    {
        picks.Clear();
        foreach (Button image in pucks[puckIndex].GetComponentsInChildren<Button>())
        {
            picks.Add(image.gameObject);
        }
    }
    private void CheckForButtons()
    {
        if (puckIndex == 0)
        {
            leftButton.SetActive(false);
            rightButton.SetActive(true);
        }
        else if (puckIndex == 2)
        {
            leftButton.SetActive(true);
            rightButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }
    }
    private void CardSwiper()
    {
        pucks[puckIndex].transform.DOLocalMove(Vector3.zero, swipeSpeed);

        if (puckIndex > prevIndex)
        {
            pucks[prevIndex].transform.DOLocalMove(leftPoint.transform.localPosition, swipeSpeed);
        }
        else
        {
            pucks[prevIndex].transform.DOLocalMove(rightPoint.transform.localPosition, swipeSpeed);
        }
        FillPicsList();
    }

    public void PuckChanger(int k)
    {
        prevIndex = puckIndex;
        puckIndex += k;
        CheckForButtons();
        CardSwiper();
    }
    public void PictureButton(int index)
    {
        if (picIndex == 4)
        {
            picIndex = index;
            kcal = callories[puckIndex, index];
            picks[index].transform.DOLocalMove(centerPoint.transform.localPosition, picsMoveSpeed);
            picks[index].transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), picsMoveSpeed);
            picks[index].transform.SetAsLastSibling();
            ColorChanger(picks[index].GetComponent<Image>(), picks[index].GetComponentsInChildren<Image>()[1]);
            panelButtons.SetActive(false);
            panelInput.SetActive(true);
            calsCountText.text = $"{kcal}kcal/100g";
        }
        else
        {
            PicPoseAndScalereturn();
        }
    }
    public void PicPoseAndScalereturn()
    {
        if (picIndex != 4)
        {
            picks[picIndex].transform.DOLocalMove(picksPoses[picIndex], picsMoveSpeed);
            picks[picIndex].transform.DOScale(new Vector3(1, 1, 1), picsMoveSpeed).onComplete = SetIndexInParent;

            foreach (Image image in pucks[puckIndex].GetComponentsInChildren<Image>())
            {
                image.DOColor(Color.white, swipeSpeed);
            }
        }
    }

    private void SetIndexInParent()
    {
        picks[picIndex].transform.SetSiblingIndex(picIndex);
        picIndex = 4;
        panelButtons.SetActive(true);
        panelInput.SetActive(false);
    }
    private void ColorChanger(Image img, Image img2)
    {
        foreach (Image image in pucks[puckIndex].GetComponentsInChildren<Image>())
        {
            if (img != image & img2 != image)
                image.DOColor(new Color32(100, 100, 100, 255), swipeSpeed/2);
            else
                image.DOColor(Color.white, swipeSpeed/2);
        }
    }

   
}
