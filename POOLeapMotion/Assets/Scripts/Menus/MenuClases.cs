using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuClases : CustomMenu
{
    public TextMeshPro info;

    public LineaClase[] lines;

    int numberClases;

    public int NumberClases
    {
        get { return numberClases; }
        set
        {
            numberClases = value;
            if (numberClases > 2)
            {
                GetButton("Crear").gameObject.SetActive(false);
            }
            else
            {
                GetButton("Crear").gameObject.SetActive(true);
                GetButton("Crear").transform.parent.position = new Vector3(GetButton("Crear").transform.parent.position.x,
                                                                            lines[numberClases].transform.position.y,
                                                                            GetButton("Crear").transform.parent.position.z);
            }
        }
    }

    int numberObjetos;

    public int NumberObjetos
    {
        get { return numberObjetos; }
        set
        {
            numberObjetos = value;
            if (numberObjetos > m.gridObjeto.Count - 1)
            {
                GetButton("CrearObjeto1").gameObject.SetActive(false);
                GetButton("CrearObjeto1").Blocked = true;
                GetButton("CrearObjeto1").gameObject.SetActive(true);
                GetButton("CrearObjeto2").gameObject.SetActive(false);
                GetButton("CrearObjeto2").Blocked = true;
                GetButton("CrearObjeto2").gameObject.SetActive(true);
                GetButton("CrearObjeto3").gameObject.SetActive(false);
                GetButton("CrearObjeto3").Blocked = true;
                GetButton("CrearObjeto3").gameObject.SetActive(true);
            }
            else
            {
                GetButton("CrearObjeto1").Blocked = false;
                GetButton("CrearObjeto2").Blocked = false;
                GetButton("CrearObjeto3").Blocked = false;
            }
        }
    }

    int numberVariables;

    public int NumberVariables
    {
        get { return numberVariables; }
        set
        {
            numberVariables = value;
            if (numberVariables > m.gridVariable.Count - 1)
            {
                GetButton("CrearVariable1").Blocked = true;
                GetButton("CrearVariable2").Blocked = true;
                GetButton("CrearVariable3").Blocked = true;
            }
            else
            {
                GetButton("CrearVariable1").Blocked = false;
                GetButton("CrearVariable2").Blocked = false;
                GetButton("CrearVariable3").Blocked = false;
            }
        }
    }

    MenuGrid m;

    // Start is called before the first frame update
    public override void Init()
    {
        base.Init();

        m = (MenuGrid)Manager.Instance.GetMenu("MenuGrid");

        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].indice = i;
            lines[i].Init();
        }

        GetButton("Volver").OnPress+=(()=>Manager.Instance.Save());
    }

    public void ReOrder(){
        int n = m.anchorablePrefs.Count;
        for (int i = 0; i < lines.Length; i++)
        {
            bool active = i < n;
            if (active)
            {
                lines[i].objeto = m.anchorablePrefs[i];
                lines[i].nombre.text = lines[i].objeto.nombre;
                lines[i].color.material = lines[i].objeto.Material;
                lines[i].cube.textoPanelSuperior.text = "class " + lines[i].objeto.nombre;
            }
            lines[i].gameObject.SetActive(active);
        }
    }

    public new void Open()
    {
        base.Open();
        ReOrder();
    }

    public override void Callback()
    {
        NumberObjetos += 0;
        NumberVariables += 0;
    }

    public void ShowText(string text)
    {
        info.gameObject.SetActive(false);
        info.text = text;
        info.gameObject.SetActive(true);
    }
}
