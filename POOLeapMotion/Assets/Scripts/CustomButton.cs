using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : InteractionButton, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    bool locked = false;
    public bool Locked { get { return locked; } set { locked = value; ignoreContact = value; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoveringMouse = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoveringMouse = false;
        _isPressed = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPress();
    }

    private new void OnDisable() {
        base.OnDisable();
        _hoveringMouse = false;
    }

    public new void Update()
    {
        if (!locked)
        {
            base.Update();
        }
    }

}
