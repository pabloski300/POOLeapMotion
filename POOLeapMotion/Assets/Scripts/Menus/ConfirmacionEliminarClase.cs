using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmacionEliminarClase : CustomMenu
{
    LineaClase linea;
    public static ConfirmacionEliminarClase Instance;
    // Start is called before the first frame update
    public new void Init()
    {
        base.Init();
        Instance = this;
        GetButton("Confirmar").OnPress += (() => Confirmar());
    }

    public void Open(LineaClase c)
    {
        base.Open();
        linea = c;
    }

    public void Confirmar()
    {
        if (linea != null)
        {
            linea.Eliminar();
        }
    }
}
