using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class btnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNTYPE          currentType;
    public Transform        buttonScale;
    Vector3                 defaultScale;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNTYPE.Start:
                break;
            case BTNTYPE.Help:
                break;
            case BTNTYPE.Option:
                break;
            case BTNTYPE.Yes:
                break;
            case BTNTYPE.No:
                break;
            case BTNTYPE.Sound:
                break;
            case BTNTYPE.Back:
                break;
            case BTNTYPE.Quit:
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
