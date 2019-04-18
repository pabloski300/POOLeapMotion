using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploracionMetodo : CustomMenu
{
    public List<SubmenuMetodo> menus;
    public MetodoBase metodoActual;
    public SubmenuMetodo menuActual;

    public static ExploracionMetodo Instance;
    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OpenNew(MetodoBase metodo)
    {
        base.Open();
        buttons[0].gameObject.SetActive(true);
        foreach (SubmenuMetodo s in menus)
        {
            s.gameObject.SetActive(false);
            s.Clear();
        }
        menuActual = menus.Find(x => x.nombre == metodo.nombre);
        menuActual.gameObject.SetActive(true);
        metodoActual = metodo;

    }

    public new void Close(){
        foreach (SubmenuMetodo s in menus)
        {
            s.gameObject.SetActive(false);
            s.Clear();
        }
        base.Close();
    }

    public void Execute()
    {
        buttons[0].gameObject.SetActive(false);
        metodoActual.Execute(menuActual.inputs,menuActual.output);
    }
}
