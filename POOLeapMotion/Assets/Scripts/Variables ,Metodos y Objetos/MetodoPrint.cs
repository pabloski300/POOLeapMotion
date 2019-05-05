using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetodoPrint : MetodoBase
{
    public override void Execute(List<TMP_InputField> inputs, TextMeshPro output)
    {
        string x = inputs[0].text;
        output.text = x;
        ExploracionMetodo.Instance.GetButton("Ejecutar").gameObject.SetActive(true);
    }

    public override string WriteFile()
    {
        string s = " \n";
        s += "    public void Print(string x){\n";
        s += "        System.out.println(x);\n";
        s += "    }\n";

        return s;
    }
}
