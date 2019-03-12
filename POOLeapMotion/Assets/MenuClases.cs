using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClases : MonoBehaviour
{
    public LineaClase[] buttons;


    // Start is called before the first frame update
    void Start(){
        buttons[0].crear.OnPress += (()=>Create(0));
        buttons[0].eliminar.OnPress += (()=>DeleteClass(0));
        buttons[1].crear.OnPress += (()=>Create(1));
        buttons[1].eliminar.OnPress += (()=>DeleteClass(1));
        buttons[2].crear.OnPress += (()=>Create(2));
        buttons[2].eliminar.OnPress += (()=>DeleteClass(2));
    }
    
    public void Init()
    {
        int n = Manager.Instance.anchorablePrefs.Count;

        for(int i=0; i<buttons.Length; i++){
            bool active = i < n;
            if(active){
                buttons[i].nombre.text = Manager.Instance.anchorablePrefs[i].nombre;
            }
            buttons[i].gameObject.SetActive(active);
        }
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
