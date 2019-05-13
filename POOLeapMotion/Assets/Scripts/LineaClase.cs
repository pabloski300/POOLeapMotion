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

    MenuGrid mg;
    MenuClases mc;
    CreadorObjetos co;
    MenuExplorar me;
    CreadorVariables cv;
    ConfirmacionEliminarClase ce;

    public RepresentacionClase cube;
    public CustomAnchor mainAnchor;

    public void Init()
    {
        mg = (MenuGrid)Manager.Instance.GetMenu("MenuGrid");
        mc = (MenuClases)Manager.Instance.GetMenu("MenuClases");
        co = (CreadorObjetos)Manager.Instance.GetMenu("CreadorObjetos");
        me = (MenuExplorar)Manager.Instance.GetMenu("MenuExplorar");
        cv = (CreadorVariables)Manager.Instance.GetMenu("CreadorVariables");
        ce = (ConfirmacionEliminarClase)Manager.Instance.GetMenu("ConfirmacionEliminarClase");

        modificar.OnPress += (() => Modificar());
        crearObjeto.OnPress += (() => CrearObjeto());
        crearVariable.OnPress += (() => CrearVariable());
        explorar.OnPress += (() => Explorar());
        this.gameObject.SetActive(false);

        cube.Init(mainAnchor, this);
    }

    public void ConfirmarEliminar()
    {
        ce.Open();
        mc.Close();
    }

    public void Eliminar()
    {
        mg.RemoveOfTypeObject(objeto);
        mg.RemoveOfTypeVariable(objeto);
        mg.anchorablePrefs.Remove(objeto);
        Destroy(objeto.gameObject);
        objeto = null;
        mc.ReOrder();
        mc.NumberClases--;
    }

    public void Modificar()
    {
        co.OpenModify(objeto);
    }

    public void CrearObjeto()
    {
        mg.SpawnObject(indice);
        mc.NumberObjetos++;
        mc.ShowText("ObjetoCreado");
    }

    public void Explorar()
    {
        me.Open(objeto);
    }

    public void CrearVariable()
    {
        cv.OpenNew(nombre.text, objeto.Material);
    }

    public void AbrirConfirmacion()
    {
        ConfirmacionEliminarClase.Instance.Open(this);
        mc.Close();
    }
}
