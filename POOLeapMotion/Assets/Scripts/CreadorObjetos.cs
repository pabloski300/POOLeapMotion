using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Leap.Unity.Animation;
using System;

public class CreadorObjetos : CustomMenu
{
    [Header("Strings")]
    public TMP_InputField nombreInput;
    public TextMeshPro cabecera;
    public TextMeshPro textoError;

    [HideInInspector]
    public List<IntVariable> variablesInt;
    [HideInInspector]
    public List<FloatVariable> variablesFloat;
    [HideInInspector]
    public List<BoolVariable> variablesBoolean;

    public Dictionary<string, MetodoBase> metodos = new Dictionary<string, MetodoBase>();

    public static CreadorObjetos Instance;

    AssetBundle bundle;

#region Inicializacion
    private void Start() {
        nombreInput.inputValidator = InputValidationAlphaOnly.CreateInstance<InputValidationAlphaOnly>();
        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "objeto"));

        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    public void Restart(){
        variablesInt.Clear();
        variablesFloat.Clear();
        variablesBoolean.Clear();
        metodos.Clear();
        nombreInput.text = "";
        buttons[0].gameObject.SetActive(false);
        buttons[1].gameObject.SetActive(false);
        buttons[3].gameObject.SetActive(false);
    }
#endregion
    
#region MetodosTween

    public void OpenNew(){
        Restart();
        Open();
    }

    public void End(){
        Create();
        Close();
    }
    
#endregion

#region Metodos
    public void TrimString(){

        
        textoError.gameObject.SetActive(false);
        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(true);
        buttons[3].gameObject.SetActive(true);
        nombreInput.text = nombreInput.text.Trim();
        cabecera.text = "public class " + nombreInput.text;

        bool repeat = false;

        if(!repeat){
            textoError.text = "Este nombre esta reservado";
        }

        foreach(string i in CreadorMetodos.Instance.metodos.Keys){
            repeat = nombreInput.text.Equals(i, StringComparison.InvariantCultureIgnoreCase);
        }

        if(!repeat){
            textoError.text = "Este nombre esta en uso por una variable";
        }

        for(int i=0; i<variablesInt.Count && !repeat; i++){
            repeat = nombreInput.text.Equals(variablesInt[i].Nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for(int i=0; i<variablesFloat.Count && !repeat; i++){
            repeat = nombreInput.text.Equals(variablesFloat[i].Nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for(int i=0; i<variablesBoolean.Count && !repeat; i++){
            repeat = nombreInput.text.Equals(variablesBoolean[i].Nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if(!repeat){
            textoError.text = "Este nombre esta en uso por otra clase";
        }

        for(int i=0; i<Manager.Instance.anchorablePrefs.Count && !repeat; i++){
            repeat = nombreInput.text.Equals(Manager.Instance.anchorablePrefs[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if (repeat)
        {
            buttons[3].gameObject.SetActive(false);
            textoError.gameObject.SetActive(true);
        }

        if (nombreInput.text.Length > nombreInput.characterLimit)
        {
            nombreInput.text = nombreInput.text.Remove(nombreInput.characterLimit, 1);
        }

        if(nombreInput.text.Length == 0){
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(false); 
        }  
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
#endregion

}
