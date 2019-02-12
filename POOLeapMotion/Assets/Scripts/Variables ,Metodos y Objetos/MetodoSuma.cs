using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetodoSuma : MetodoBase
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

    public override void WriteFile()
    {
        throw new System.NotImplementedException();
    }
}
