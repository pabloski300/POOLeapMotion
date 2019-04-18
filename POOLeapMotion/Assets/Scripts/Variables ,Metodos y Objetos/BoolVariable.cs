using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolVariable : Variable<bool>
{
    public override void Init(ObjetoBase objeto, CustomAnchor main){
        base.Init(objeto,main);
        textoPanelSuperior.text = proteccion.ToString()+" bool "+nombre;
    }

    public override string WriteFile()
    {
        return "    boolean "+ nombre +";\n";
    }
}
