using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;

public class CreadorMetodos : CustomMenu
{
    [Header("Strings")]
    public TextMeshPro cabecera;

    public GameObject textoError;

    string method;

    AssetBundle bundle;

    public Dictionary<string, MetodoBase> metodos = new Dictionary<string, MetodoBase>();

    bool valid;

    MetodoBase methodToModify;

    int indiceLinea;

    bool modify;

    public static CreadorMetodos Instance;

    #region Inicializacion
    private void Start()
    {
        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "metodos"));
        List<GameObject> m = bundle.LoadAllAssets<GameObject>().ToList();
        foreach (GameObject x in m)
        {
            MetodoBase mb = x.GetComponent<MetodoBase>();
            metodos.Add(mb.nombre, mb);
        }
        bundle.Unload(false);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
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
        cabecera.text = metodos[method].cabecera;
        valid = !CreadorObjetos.Instance.metodos.ContainsKey(method);
        if(modify && !valid){
            valid = method == methodToModify.nombre;
        }
        textoError.SetActive(!valid);
        buttons[7].gameObject.SetActive(valid);
    }

    public void Create()
    {
        if (!modify)
        {
            MetodoBase metodo = Instantiate(metodos[method], Vector3.zero, Quaternion.identity);
            CreadorObjetos.Instance.metodos.Add(metodo.nombre, metodo);
            PanelIzquierdo.Instance.AddMetodo(metodo.nombre);
            CreadorObjetos.Instance.NumberMethods ++;
        }
        else
        {
            CreadorObjetos.Instance.metodos.Remove(methodToModify.nombre);
            
            Destroy(methodToModify.gameObject);
            MetodoBase metodo = Instantiate(metodos[method], Vector3.zero, Quaternion.identity);
            CreadorObjetos.Instance.metodos.Add(metodo.nombre, metodo);
            PanelIzquierdo.Instance.AddMetodo(indiceLinea, metodo.nombre);
        }
    }
    #endregion

}
