using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetodoMul : MetodoBase
{
    ObjetoBase objeto;
    int param1;
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
        s += "    public int Mul(int x, int y){\n";
        s += "        return x * y;\n";
        s += "    }\n";

        return s;
    }
}
