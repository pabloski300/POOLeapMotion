using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetodoPrint : MetodoBase
{
    ObjetoBase objeto;
    string param1;
    public string param1string;

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
        s += "    public void Print(string x){\n";
        s += "        System.out.println(x);\n";
        s += "    }\n";

        return s;
    }
}
