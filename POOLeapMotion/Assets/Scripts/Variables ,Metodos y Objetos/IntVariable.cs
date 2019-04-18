using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntVariable : Variable<int>
{
    public override void Init(ObjetoBase objeto, CustomAnchor main){
        base.Init(objeto,main);
        textoPanelSuperior.text = proteccion.ToString()+" int "+nombre;
    }

    public override string WriteFile()
    {
        return "    int "+ nombre +";\n";
        
    }
}
