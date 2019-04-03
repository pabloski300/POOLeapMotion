using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableObjeto : CustomAnchorable
{

    public string nombre;

    public string clase;

    public ObjetoBase objetoReferenciado;

    public LineRenderer referencia;

    // Start is called before the first frame update
    public void Init(string nombre, string clase)
    {
        this.nombre = nombre;
        this.clase = clase;
        objetoReferenciado = null; 
        textoPanelSuperior.text = clase +" "+ nombre+";";
    }

    // Update is called once per frame
    void Update()
    {
        if(objetoReferenciado != null){
            referencia.SetPosition(0,transform.position);
            referencia.SetPosition(1,objetoReferenciado.transform.position);
        }else{
            referencia.SetPosition(1,transform.position);
            referencia.SetPosition(0,transform.position);
        }
    }

    private void OnTriggerEnter(Collider other) {
        ObjetoBase o  = other.gameObject.GetComponent<ObjetoBase>();

        if(o != null && o == objetoReferenciado){
            return;
        }

        if(o != null && o.nombre == clase){
            objetoReferenciado = o;
        }
    }
}
