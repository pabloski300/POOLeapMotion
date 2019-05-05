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

    public GameObject textoError;
    public ToggleGroup metodosGroup;
    string metodosToggle;

    string method;

    bool valid;

    MetodoBase methodToModify;

    int indiceLinea;

    bool modify;

    public static CreadorMetodos Instance;

    #region Inicializacion
    private new void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        base.Awake();

        GetButton("Finalizar").OnPress += (()=>End());
    }

    public void Restart()
    {
        ChangeMethod("MetodoPrint");
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

    public override void Callback(){
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
        valid = !CreadorObjetos.Instance.metodos.ContainsKey(method);
        if(modify && !valid){
            valid = method == methodToModify.nombre;
        }
        textoError.SetActive(!valid);
        GetButton("Finalizar").gameObject.SetActive(valid);
        metodosToggle = name;
    }

    public void Create()
    {
        if (!modify)
        {
            MetodoBase metodo = Instantiate(Manager.Instance.metodosPrefab[method], Vector3.zero, Quaternion.identity);
            CreadorObjetos.Instance.metodos.Add(metodo.nombre, metodo);
            PanelIzquierdo.Instance.AddMetodo(metodo.nombre);
            CreadorObjetos.Instance.NumberMethods ++;
        }
        else
        {
            CreadorObjetos.Instance.metodos.Remove(methodToModify.nombre);
            
            Destroy(methodToModify.gameObject);
            MetodoBase metodo = Instantiate(Manager.Instance.metodosPrefab[method], Vector3.zero, Quaternion.identity);
            CreadorObjetos.Instance.metodos.Add(metodo.nombre, metodo);
            PanelIzquierdo.Instance.AddMetodo(indiceLinea, metodo.nombre);
        }
    }
    #endregion

}
