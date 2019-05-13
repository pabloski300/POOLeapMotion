using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity.Animation;
using UnityEngine;

public class CustomMenu : MonoBehaviour
{
    Dictionary<string, CustomButton> buttons = new Dictionary<string,CustomButton>();
    public Dictionary<string, CustomButton> Buttons { get { return buttons; } }

    [Header("Tweens")]
    public TransformTweenBehaviour tween;

    public virtual void Init()
    {
        List<CustomButton> buttonsL = GetComponentsInChildren<CustomButton>().ToList();
        foreach (CustomButton b in buttonsL)
        {
            b.Init();
            buttons.Add(b.name, b);
        }
    }

    public void Open()
    {
        tween.PlayForward();
    }

    public void Close()
    {
        tween.PlayBackward();
    }

    public void LockButtons()
    {
        int i = 0;
        foreach (string k in buttons.Keys)
        {
            buttons[k].Locked = true;
            buttons[k].lockUpdate = true;
            i++;
        }
    }

    public void UnlockButtons()
    {
        int i = 0;
        foreach (string k in buttons.Keys)
        {
            buttons[k].Locked = false;
            i++;
        }
    }

    public void UnlockButtonsDelayed(float time)
    {
        StartCoroutine(ChangeButtonState(time, false));
    }

    IEnumerator ChangeButtonState(float time, bool locked)
    {
        LockButtons();
        yield return new WaitForSeconds(time);
        foreach (string k in buttons.Keys)
        {
            buttons[k].Locked = locked;
            buttons[k].lockUpdate = locked;
        }
        Callback();
    }

    public CustomButton GetButton(string name)
    {
        try{
            return buttons[name];
        }catch{
            Debug.Log(name);
            return null;
        }
    }

    public virtual void Callback(){

    }
}
