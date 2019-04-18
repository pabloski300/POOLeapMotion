using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : InteractionButton, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    bool locked = false;
    public bool Locked { get { return locked; } set { locked = value; ignoreContact = value; } }

    public new void Start() {
        base.Start();
        OnPress += (()=>LockPress());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!locked){
            _isPressed = true;
        }
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
        if(!locked){
            OnPress();
        }
    }

    private new void OnDisable() {
        base.OnDisable();
        _hoveringMouse = false;
        this.locked = false;
    }

    public new void Update()
    {
        if (!locked)
        {
            base.Update();
        }
    }

    void LockPress(){
        if(gameObject.activeSelf){
            StartCoroutine(LockAfterPress());
        }
    }

    IEnumerator LockAfterPress(){
        this.locked = true;
        yield return new WaitForSeconds(0.1f);
        this.locked = false;
    }

}
