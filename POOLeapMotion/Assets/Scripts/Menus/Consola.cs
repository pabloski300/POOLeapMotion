using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Consola : CustomMenu
{
    public int maxLineas = 16;

    int currentLines = 0;

    public TextMeshPro text;
    
    List<string> lineas = new List<string>();

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
        lineas.Clear();
    }
}
