using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploracionObjeto : CustomMenu
{

    public static ExploracionObjeto Instance;

    string nombreVariable;

    ObjetoBase objeto;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    public void Open(ObjetoBase objeto){
        
    }
}
