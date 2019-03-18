using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClases : CustomMenu
{
    public LineaClase[] lines;


    // Start is called before the first frame update
    void Start(){
        lines[0].crear.OnPress += (()=>Create(0));
        lines[0].eliminar.OnPress += (()=>DeleteClass(0));
        lines[1].crear.OnPress += (()=>Create(1));
        lines[1].eliminar.OnPress += (()=>DeleteClass(1));
        lines[2].crear.OnPress += (()=>Create(2));
        lines[2].eliminar.OnPress += (()=>DeleteClass(2));
    }
    
    public void Init()
    {
        int n = Manager.Instance.anchorablePrefs.Count;

        for(int i=0; i<lines.Length; i++){
            bool active = i < n;
            if(active){
                lines[i].nombre.text = Manager.Instance.anchorablePrefs[i].nombre;
            }
            lines[i].gameObject.SetActive(active);
        }
    }

    public void OpenNew(){
        Open();
        Init();
    }

    public void DeleteClass(int i){
        Manager.Instance.RemoveAll();
        Destroy(Manager.Instance.anchorablePrefs[i].gameObject);
        Manager.Instance.anchorablePrefs.RemoveAt(i);
        Init();
    }

    public void Create(int i){
        Manager.Instance.SpawnObject(i);
    }
}
