using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntVariable : Variable<int>
{
    ObjetoBase objeto;

    public override void Init(ObjetoBase objeto)
    {
        this.objeto = objeto;
    }

    public override void WriteFile()
    {
        throw new System.NotImplementedException();
    }
}
