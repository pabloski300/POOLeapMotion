using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolVariable : Variable<bool>
{
    public override void Init(ObjetoBase objeto, CustomAnchor main){
        base.Init(objeto,main);
        textoPanelSuperior.text = proteccion.ToString().ToLower()+" bool "+nombre;
    }

    public override string WriteFile()
    {
        return "    "+proteccion.ToString().ToLower()+" boolean "+ nombre +";\n";
    }
}
