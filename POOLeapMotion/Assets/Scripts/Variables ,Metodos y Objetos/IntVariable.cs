using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntVariable : Variable<int>
{
    public override void Init(ObjetoBase objeto, CustomAnchor main){
        base.Init(objeto,main);
        textoPanelSuperior.text = proteccion.ToString().ToLower()+" int "+nombre;
    }

    public override string WriteFile()
    {
        return "    "+proteccion.ToString().ToLower()+" int "+ nombre +";\n";
        
    }
}
