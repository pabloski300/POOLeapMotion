using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInicio : CustomMenu
{
    // Start is called before the first frame update
    MenuClases mc;
    public override void Init()
    {
        base.Init();
        mc = (MenuClases)Manager.Instance.GetMenu("MenuClases");
        GetButton("Cargar").OnPress += (()=>{Manager.Instance.Load(); mc.Open();});
        GetButton("Language").OnPress += (()=>Manager.Instance.ChangeLanguage());
    }

}
