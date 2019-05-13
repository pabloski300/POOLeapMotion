using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploracionMetodo : CustomMenu
{
    public List<SubmenuMetodo> menus;
    public MetodoBase metodoActual;
    public SubmenuMetodo menuActual;

    Consola c;
    ExploracionObjeto e;

    // Start is called before the first frame update
    public override void Init()
    {
        base.Init();

        c = (Consola)Manager.Instance.GetMenu("Consola");
        e = (ExploracionObjeto)Manager.Instance.GetMenu("ExploracionObjeto");

        GetButton("Ejecutar").OnPress += (() => Execute());

        foreach (SubmenuMetodo s in menus)
        {
            s.gameObject.SetActive(false);
            s.Init();
        }
    }

    public void OpenNew(MetodoBase metodo)
    {
        base.Open();
        GetButton("Ejecutar").gameObject.SetActive(true);
        menuActual = menus.Find(x => x.nombre == metodo.nombre);
        menuActual.gameObject.SetActive(true);
        metodoActual = metodo;

    }

    public new void Close()
    {
        foreach (SubmenuMetodo s in menus)
        {
            s.gameObject.SetActive(false);
            s.Clear();
        }
        base.Close();
    }

    public void Execute()
    {
        GetButton("Ejecutar").gameObject.SetActive(false);
        metodoActual.Execute(menuActual.inputs, menuActual.output);
        c.Write(e.variable.nombre + "." + metodoActual.nombre + "();");
    }
}
