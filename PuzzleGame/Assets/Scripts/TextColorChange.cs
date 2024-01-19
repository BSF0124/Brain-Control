using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextColorChange : MonoBehaviour
{
    public TextMeshProUGUI text;

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     text.color = Color.white;
    // }

    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     text.color = new Color32(161,161,161,255);
    // }

    public void ChangeWhite()
    {
        text.color = Color.white;
    }

    public void ChangeGray()
    {
        text.color = new Color32(161,161,161,255);
    }

}
