using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : InteractionButton, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    bool locked = false;
    public bool Locked
    {
        get
        {
            return locked;
        }
        set
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
            if (!blocked)
            {
                locked = value; /* ignoreContact = value;*/ rb.isKinematic = value;
            }
            else
            {
                locked = true; rb.isKinematic = true;
            }
        }
    }

    [HideInInspector]
    public bool toggleButton;
    bool blocked;
    public bool Blocked
    {
        get
        {
            return blocked;
        }
        set
        {
            blocked = value; /* ignoreContact = value;*/
            Locked = value;
        }
    }

    Rigidbody rb;

    public bool lockUpdate;

    public void Init()
    {
        rb = GetComponent<Rigidbody>();
        base.Start();
        OnPress += (() => LockPress());
        OnPress += (() => Manager.Instance.PlaySound(1));
        ButtonScaler b = GetComponent<ButtonScaler>();
        if (b != null)
        {
            b.enabled = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!locked)
        {
            Manager.Instance.PlaySound(0);
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
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!locked && _hoveringMouse)
        {
            OnPress();
        }
        _isPressed = false;
    }

    private new void OnDisable()
    {
        base.OnDisable();
        _hoveringMouse = false;
        this.Locked = false;
    }

    private new void OnEnable()
    {
        base.OnEnable();
        _hoveringMouse = false;
        if (rb != null)
            this.Locked = false;
    }

    public new void Update()
    {
        if (!lockUpdate)
        {
            base.Update();
        }
    }

    void LockPress()
    {

        this.Locked = true;
        if (gameObject.activeSelf && !toggleButton)
        {
            StartCoroutine(LockAfterPress());
        }
    }

    IEnumerator LockAfterPress()
    {
        this.Locked = true;
        yield return new WaitForSeconds(0.2f);
        this.Locked = false;
    }

}
