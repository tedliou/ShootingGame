using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    public GameObject hurt;
    public Image bloodBar;
    float barWidth;
    float widthRate;
    RectTransform rectTransform;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        rectTransform = bloodBar.GetComponent<RectTransform>();
        barWidth = rectTransform.rect.width;
        widthRate = barWidth / PlayerManager.Instance.hp;
    }
    public void GetHurt(int hhhhh)
    {
        hurt.GetComponent<Animator>().Play("Hurt");

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - hhhhh * widthRate, rectTransform.sizeDelta.y);
    }
}
