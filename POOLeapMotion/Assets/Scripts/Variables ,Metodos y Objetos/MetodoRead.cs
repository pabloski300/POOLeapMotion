using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetodoRead : MetodoBase
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
        s += "    public string Read(){\n";
        s += "        Scanner s = new Scanner(System.in);\n";
        s += "        string x = s.next();\n";
        s += "        return s;\n";
        s += "    }\n";

        return s;
    }
}
