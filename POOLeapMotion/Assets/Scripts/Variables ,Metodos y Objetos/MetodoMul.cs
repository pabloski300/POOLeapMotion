using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetodoMul : MetodoBase
{

    public override void Execute(List<TMP_InputField> inputs, TextMeshPro output)
    {
        int x = int.Parse(inputs[0].text);
        int y = int.Parse(inputs[1].text);
        output.text = (x*y).ToString();
        em.GetButton("Ejecutar").gameObject.SetActive(true);
    }

    public override string WriteFile()
    {
        string s = " \n";
        s += "    public int Mul(int x, int y){\n";
        s += "        return x * y;\n";
        s += "    }\n";

        return s;
    }
}
