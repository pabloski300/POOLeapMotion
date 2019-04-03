using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuExplorar : CustomMenu
{
    public TextMeshPro text;

    public static MenuExplorar Instance;

    private new void Start() {
        base.Start();
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    public void Open(ObjetoBase objeto){
        base.Open();
        text.text = objeto.codigo;
    }
}
