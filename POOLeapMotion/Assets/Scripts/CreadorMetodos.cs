using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Leap.Unity.Interaction;
using TMPro;
using UnityEngine;

public class CreadorMetodos : MonoBehaviour
{
    public TextMeshPro cabecera;

    public GameObject textoError;

    public string method;
    
    AssetBundle bundle;

    Dictionary<string, MetodoBase> metodos = new Dictionary<string, MetodoBase>();

    bool valid;

    private void Start()
    {
        Debug.Log("Metodos");
        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "metodos"));
        List<GameObject> m = bundle.LoadAllAssets<GameObject>().ToList();
        Debug.Log(m.Count);
        foreach(GameObject x in m) {
            MetodoBase mb = x.GetComponent<MetodoBase>();
            Debug.Log(mb.Nombre);
            metodos.Add(mb.Nombre, mb);
        }
        bundle.Unload(false);
    }

    public void ChangeMethod(string name){
        method = name;
        cabecera.text = metodos[method].cabecera;
        valid = !CreadorObjetos.Instance.metodos.ContainsKey(method);
        textoError.SetActive(!valid);
    }

    public void Restart()
    {
        method = "MetodoPrint";
        cabecera.text = metodos[method].cabecera;
    }

    public void Create()
    {
        if(valid){
            MetodoBase metodo = Instantiate(metodos[method], Vector3.zero, Quaternion.identity);
            CreadorObjetos.Instance.metodos.Add(metodo.Nombre, metodo);
        }
    }
}
