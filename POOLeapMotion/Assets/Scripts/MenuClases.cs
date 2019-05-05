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
            GetButton("Crear").gameObject.SetActive(false);
        }else{
            GetButton("Crear").gameObject.SetActive(true);
        }
    }}

    int numberObjetos;

    public int NumberObjetos{get{return numberObjetos;}set{numberObjetos = value;
        if(numberObjetos > MenuGrid.Instance.gridObjeto.Count-1){
            GetButton("CrearObjeto1").gameObject.SetActive(false);
            GetButton("CrearObjeto2").gameObject.SetActive(false);
            GetButton("CrearObjeto3").gameObject.SetActive(false);
        }else{
            GetButton("CrearObjeto1").gameObject.SetActive(true);
            GetButton("CrearObjeto2").gameObject.SetActive(true);
            GetButton("CrearObjeto3").gameObject.SetActive(true);
        }
    }}

    int numberVariables;

    public int NumberVariables{get{return numberVariables;}set{numberVariables = value;
        if(numberVariables > MenuGrid.Instance.gridVariable.Count-1){
            GetButton("CrearVariable1").gameObject.SetActive(false);
            GetButton("CrearVariable2").gameObject.SetActive(false);
            GetButton("CrearVariable3").gameObject.SetActive(false);
        }else{
            GetButton("CrearVariable1").gameObject.SetActive(true);
            GetButton("CrearVariable2").gameObject.SetActive(true);
            GetButton("CrearVariable3").gameObject.SetActive(true);
        }
    }}


    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
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
                lines[i].color.material = lines[i].objeto.Material;
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
