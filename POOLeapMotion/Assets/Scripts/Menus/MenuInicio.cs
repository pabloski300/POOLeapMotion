using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInicio : CustomMenu
{
    // Start is called before the first frame update
    public override void Init()
    {
        base.Init();
        GetButton("Cargar").OnPress += (()=>Manager.Instance.Load());
    }

}
