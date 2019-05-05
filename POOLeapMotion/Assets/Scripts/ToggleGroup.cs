using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToggleGroup : MonoBehaviour
{
    List<CustomButton> buttons = new List<CustomButton>();
    // Start is called before the first frame update
    void Awake()
    {
        buttons = GetComponentsInChildren<CustomButton>().ToList();
        foreach (CustomButton b in buttons)
        {
            b.toggleButton = true;
            b.OnPress += (() => StartCoroutine(Toggle(b)));
        }
    }

    // Update is called once per frame
    IEnumerator Toggle(CustomButton buttonPressed)
    {
        foreach (CustomButton b in buttons)
        {
            if (b != buttonPressed)
            {
                b.Locked = false;
            }
        }
        yield return null;
        buttonPressed.Locked = true;
        yield return null;
        Debug.Log("pulsado");
        buttonPressed.ClearContactTracking();
    }

    public void Reset(CustomButton buttonPressed)
    {
        buttonPressed.Locked = true;
    }
}
