using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuExplorar : CustomMenu
{
    public TextMeshPro text;

    public void Open(ObjetoBase objeto){
        base.Open();
        text.text = objeto.codigo;
    }
}
