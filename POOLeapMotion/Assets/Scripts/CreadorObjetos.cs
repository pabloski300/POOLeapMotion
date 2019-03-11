using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class CreadorObjetos : MonoBehaviour
{
    public TMP_InputField nombreInput;
    public TextMeshPro cabecera;

    public List<IntVariable> variablesInt;
    public List<FloatVariable> variablesFloat;
    public List<BoolVariable> variablesBoolean;

    public Dictionary<string, MetodoBase> metodos = new Dictionary<string, MetodoBase>();

    public static CreadorObjetos Instance;

    AssetBundle bundle;

    private void Start() {
        nombreInput.inputValidator = InputValidationAlphaOnly.CreateInstance<InputValidationAlphaOnly>();
        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "objeto"));
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    public void TrimString(){

        if (nombreInput.text.Length > nombreInput.characterLimit)
        {
            nombreInput.text = nombreInput.text.Remove(nombreInput.characterLimit,1);
            return;
        }

        nombreInput.text = nombreInput.text.Trim();
        cabecera.text = "public class " + nombreInput.text;
    }

    public void Restart(){
        variablesInt.Clear();
        variablesFloat.Clear();
        variablesBoolean.Clear();
        metodos.Clear();
        nombreInput.text = "";
    }

    public void Create()
    {
        
        GameObject objeto = Instantiate(bundle.LoadAsset<GameObject>("ObjetoBasico"), Vector3.zero, Quaternion.identity);
        ObjetoBase objetoScript = objeto.GetComponent<ObjetoBase>();

        objetoScript.nombre = nombreInput.text;

        string s = cabecera.text +" {\n";

        foreach(IntVariable var in variablesInt){
            Debug.Log(var.Nombre);
            Debug.Log(objetoScript.variablesInt.Count);
            objetoScript.variablesInt.Add(var.Nombre,var);
            var.gameObject.transform.parent = objetoScript.variablesParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }
        foreach(FloatVariable var in variablesFloat){
            objetoScript.variablesFloat.Add(var.Nombre,var);
            var.gameObject.transform.parent = objetoScript.variablesParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }
        foreach(BoolVariable var in variablesBoolean){
            objetoScript.variablesBool.Add(var.Nombre,var);
            var.gameObject.transform.parent = objetoScript.variablesParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }
        foreach(MetodoBase var in metodos.Values){
            objetoScript.metodos.Add(var.Nombre,var);
            var.gameObject.transform.parent = objetoScript.metodoParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }

        s += "}";

        //Debug.Log(s);
        
        objetoScript.codigo += s;

        Manager.Instance.anchorablePrefs.Add(objeto.GetComponent<ObjetoBase>());
    }
}
