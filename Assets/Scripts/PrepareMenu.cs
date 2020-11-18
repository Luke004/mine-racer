using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrepareMenu : MonoBehaviour
{
    public TextMeshProUGUI selectedLapsText;

    public Image car1, car2;
    int car1Idx, car2Idx;
    Sprite[] carSprites;

    void Start()
    {
        carSprites = Resources.LoadAll<Sprite>("Cars");
        for (int i = 0; i < carSprites.Length; ++i)
        {
            if (car1.sprite.name == carSprites[i].name)
            {
                car1Idx = i;
                continue;
            }
            if (car2.sprite.name == carSprites[i].name)
            {
                car2Idx = i;
                continue;
            }
        }
    }

    public void changeText(float value)
    {
        selectedLapsText.text = value.ToString();
    }

    public void nextCarP1()
    {
        car1Idx++;
        if (car1Idx > carSprites.Length - 1)
        {
            car1Idx = 0;
        }
        if (car1Idx == car2Idx) nextCarP1();
        car1.sprite = carSprites[car1Idx];
    }

    public void prevCarP1()
    {
        car1Idx--;
        if (car1Idx < 0)
        {
            car1Idx = carSprites.Length - 1;
        }
        if (car1Idx == car2Idx) prevCarP1();
        car1.sprite = carSprites[car1Idx];
    }

    public void nextCarP2()
    {
        car2Idx++;
        if (car2Idx > carSprites.Length - 1)
        {
            car2Idx = 0;
        }
        if (car2Idx == car1Idx) nextCarP2();
        car2.sprite = carSprites[car2Idx];
    }

    public void prevCarP2()
    {
        car2Idx--;
        if (car2Idx < 0)
        {
            car2Idx = carSprites.Length - 1;
        }
        if (car2Idx == car1Idx) prevCarP2();
        car2.sprite = carSprites[car2Idx];
    }

}
