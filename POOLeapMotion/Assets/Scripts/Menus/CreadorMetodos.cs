using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;

public class CreadorMetodos : CustomMenu
{
    [Header("UI")]
    public TextMeshPro cabecera;

    public TextMeshPro textoError;
    public ToggleGroup metodosGroup;
    string metodosToggle;

    string method;

    bool valid;

    MetodoBase methodToModify;

    int indiceLinea;

    bool modify;

    CreadorObjetos c;
    PanelIzquierdo p;

    #region Inicializacion
    public override void Init()
    {
        base.Init();

        c = (CreadorObjetos)Manager.Instance.GetMenu("CreadorObjetos");
        p = (PanelIzquierdo)Manager.Instance.GetMenu("PanelIzquierdo");

        GetButton("Finalizar").OnPress += (() => End());
    }

    public void Restart()
    {
        ChangeMethod("Print");
        indiceLinea = 0;
        methodToModify = null;
        modify = false;
    }
    #endregion

    #region MetodosTween
    public void OpenNew()
    {
        Open();
        Restart();
    }

    public void OpenModify(MetodoBase var, int i)
    {
        Open();
        Restart();
        methodToModify = var;
        indiceLinea = i;
        modify = true;
        ChangeMethod(var.nombre);
    }

    public override void Callback()
    {
        metodosGroup.Reset(GetButton(metodosToggle));
    }

    public void End()
    {
        Create();
        Close();
    }
    #endregion

    #region Metodos
    public void ChangeMethod(string name)
    {
        method = name;
        cabecera.text = Manager.Instance.metodosPrefab[method].cabecera;
        valid = !c.metodos.ContainsKey(method);
        if (modify && !valid)
        {
            valid = method == methodToModify.nombre;
        }
        if (Manager.Instance.english)
        {
            textoError.text = "This method is already in the class";
        }
        else
        {
            textoError.text = "Este metodo ya existe en la clase";
        }
        textoError.gameObject.SetActive(!valid);
        GetButton("Finalizar").gameObject.SetActive(valid);
        metodosToggle = name;
    }

    public void Create()
    {

        MetodoBase metodo = Instantiate(Manager.Instance.metodosPrefab[method], new Vector3(999, 999, 999), Quaternion.identity);
        if (!modify)
        {
            c.metodos.Add(metodo.nombre, metodo);
            p.AddMetodo(metodo.nombre);
            c.NumberMethods++;
        }
        else
        {
            c.metodos.Remove(methodToModify.nombre);
            Destroy(methodToModify.gameObject);

            c.metodos.Add(metodo.nombre, metodo);
            p.AddMetodo(indiceLinea, metodo.nombre);
        }
    }

    public void Load(string nombre)
    {
        MetodoBase metodo = Instantiate(Manager.Instance.metodosPrefab[nombre], new Vector3(999, 999, 999), Quaternion.identity);
        c.metodos.Add(metodo.nombre, metodo);
    }
    #endregion

}
