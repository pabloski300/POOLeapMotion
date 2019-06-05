using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatVariable : Variable<float>
{
    public override void Init(ObjetoBase objeto, CustomAnchor main){
        base.Init(objeto,main);
        textoPanelSuperior.text = proteccion.ToString().ToLower()+" float "+nombre;
    }

    public override string WriteFile()
    {
        return "    "+proteccion.ToString().ToLower()+" float "+ nombre +";\n";
    }
}
