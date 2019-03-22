using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploracionObjeto : CustomMenu
{

    public static ExploracionObjeto Instance;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }
}
