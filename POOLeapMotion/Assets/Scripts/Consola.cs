using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Consola : CustomMenu
{
    int maxLineas = 16;

    int currentLines = 0;

    public TextMeshPro text;

    List<string> lineas = new List<string>();

    public static Consola Instance;

    private new void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Write(string s)
    {
        if (currentLines < maxLineas)
        {
            lineas.Add(s);
            text.text = "";
            for (int i = 0; i < lineas.Count; i++)
            {
                text.text += lineas[i] + "\n";
            }

            currentLines++;
        }
        else
        {
            lineas.RemoveAt(0);
            lineas.Add(s);
            text.text = "";
            for (int i = 0; i < lineas.Count; i++)
            {
                text.text += lineas[i] + "\n";
            }
        }
    }

    public void Clear()
    {
        currentLines = 0;
        text.text = "";
    }
}
