using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClases : CustomMenu
{
    public LineaClase[] lines;

    public static MenuClases Instance;

    int numberClases;

    public int NumberClases{get{return numberClases;}set{numberClases = value;
        if(numberClases > 2){
            buttons[16].gameObject.SetActive(false);
        }else{
            buttons[16].gameObject.SetActive(true);
        }
    }}

    int numberObjetos;

    public int NumberObjetos{get{return numberObjetos;}set{numberObjetos = value;
        if(numberObjetos > MenuGrid.Instance.gridObjeto.Count-1){
            buttons[1].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false);
        }else{
            buttons[1].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(true);
            buttons[3].gameObject.SetActive(true);
        }
    }}

    int numberVariables;

    public int NumberVariables{get{return numberVariables;}set{numberVariables = value;
        if(numberVariables > MenuGrid.Instance.gridVariable.Count-1){
            buttons[7].gameObject.SetActive(false);
            buttons[8].gameObject.SetActive(false);
            buttons[9].gameObject.SetActive(false);
        }else{
            buttons[7].gameObject.SetActive(true);
            buttons[8].gameObject.SetActive(true);
            buttons[9].gameObject.SetActive(true);
        }
    }}


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        for(int i=0; i<lines.Length; i++){
            lines[i].indice = i;
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Init()
    {
        int n = MenuGrid.Instance.anchorablePrefs.Count;
        for (int i = 0; i < lines.Length; i++)
        {
            bool active = i < n;
            if (active)
            {
                lines[i].objeto = MenuGrid.Instance.anchorablePrefs[i];
                lines[i].nombre.text = lines[i].objeto.nombre;
            }
            lines[i].gameObject.SetActive(active);
        }
    }

    public new void Open()
    {
        base.Open();
        Init();
    }
}
