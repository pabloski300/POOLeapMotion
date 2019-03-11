using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetodoAdd : MetodoBase
{
    ObjetoBase objeto;
    int param1;
    int param2;

    public TMP_InputField input2;

    public override void Init(ObjetoBase objeto) {
        this.objeto = objeto;
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator WaitForParams()
    {
        throw new System.NotImplementedException();
    }

    public override string WriteFile()
    {
        string s = " \n";
        s += "    public int Add(int x, int y){\n";
        s += "        return x + y;\n";
        s += "    }\n";

        return s;
    }
}
