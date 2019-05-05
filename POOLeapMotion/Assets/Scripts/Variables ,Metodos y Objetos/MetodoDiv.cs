using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetodoDiv : MetodoBase
{

    public override void Execute(List<TMP_InputField> inputs, TextMeshPro output)
    {
        float x = float.Parse(inputs[0].text);
        float y = float.Parse(inputs[1].text);
        float z = x/y;
        output.text = string.Format("{0}",z);
        ExploracionMetodo.Instance.GetButton("Ejecutar").gameObject.SetActive(true);
    }

    public override string WriteFile()
    {
        string s = " \n";
        s += "    public int Div(float x, float y){\n";
        s += "        return x / y;\n";
        s += "    }\n";

        return s;
    }
}
