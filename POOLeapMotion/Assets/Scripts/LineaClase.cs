using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using TMPro;

public class LineaClase : MonoBehaviour
{
    public InteractionButton modificar;
    public InteractionButton crearVariable;
    public InteractionButton crearObjeto;
    public InteractionButton eliminar;
    public InteractionButton explorar;
    public TextMeshPro nombre;

    [HideInInspector]
    public ObjetoBase objeto;

    public MeshRenderer color;


    [HideInInspector]
    public int indice;

    public void Start(){
        modificar.OnPress += (()=>Modificar());
        crearObjeto.OnPress += (()=>CrearObjeto());
        crearVariable.OnPress += (()=>CrearVariable());
        eliminar.OnPress += (()=>Eliminar());
        explorar.OnPress += (()=>Explorar());
        this.gameObject.SetActive(false);

    }

    public void Eliminar()
    {
        MenuGrid.Instance.RemoveOfTypeObject(objeto);
        MenuGrid.Instance.RemoveOfTypeVariable(objeto);
        MenuGrid.Instance.anchorablePrefs.Remove(objeto);
        Destroy(objeto.gameObject);
        objeto = null;
        MenuClases.Instance.Init();
        MenuClases.Instance.NumberClases--;
    }

    public void Modificar(){
        CreadorObjetos.Instance.OpenModify(objeto);
    }

    public void CrearObjeto(){
        MenuGrid.Instance.SpawnObject(indice);
        MenuClases.Instance.NumberObjetos++;
    }

    public void Explorar(){
        MenuExplorar.Instance.Open(objeto);
    }

    public void CrearVariable(){
        MenuVariable.Instance.OpenNew(nombre.text, objeto.Material);
    }
}
