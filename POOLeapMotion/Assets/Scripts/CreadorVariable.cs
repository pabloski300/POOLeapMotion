using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Leap.Unity.Interaction;
using System;

public class CreadorVariable : CustomMenu
{
    public enum TipoVar
    {
        INT,
        FLOAT,
        BOOLEAN,
    }

    public enum ProteccionVar
    {
        PUBLIC,
        PRIVATE,
        PROTECTED
    }

    [Header("Strings")]
    public TMP_InputField nombreInput;
    public TextMeshPro cabecera;
    public TextMeshPro textoError;

    [Header("Buttons2")]
    public CustomButton[] botonesProteccion;
    public CustomButton[] botonesTipo;

    ProteccionVar nivelDeProteccion = ProteccionVar.PUBLIC;
    string proteccionString = "";
    ProteccionVar NivelDeProteccion
    {
        get { return nivelDeProteccion; }
        set
        {
            nivelDeProteccion = value;
            switch (value)
            {
                case ProteccionVar.PUBLIC:
                    proteccionString = "public";
                    break;
                case ProteccionVar.PRIVATE:
                    proteccionString = "private";
                    break;
                case ProteccionVar.PROTECTED:
                    proteccionString = "protected";
                    break;
            }
            TrimString();
        }
    }

    TipoVar tipo = TipoVar.INT;
    string tipoString;
    TipoVar Tipo
    {
        get { return tipo; }
        set
        {
            tipo = value;
            switch (value)
            {
                case TipoVar.INT:
                    tipoString = "int";
                    break;
                case TipoVar.FLOAT:
                    tipoString = "float";
                    break;
                case TipoVar.BOOLEAN:
                    tipoString = "boolean";
                    break;
            }
            TrimString();
        }
    }
    AssetBundle bundle;

    public static CreadorVariable Instance;

#region Inicializacion
    private void Start()
    {
        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "variables"));
        nombreInput.inputValidator = InputValidationAlphaOnly.CreateInstance<InputValidationAlphaOnly>();
        botonesProteccion[0].OnPress += (() => { NivelDeProteccion = ProteccionVar.PUBLIC; });
        botonesProteccion[1].OnPress += (() => { NivelDeProteccion = ProteccionVar.PRIVATE; });
        botonesProteccion[2].OnPress += (() => { NivelDeProteccion = ProteccionVar.PROTECTED; });
        botonesTipo[0].OnPress += (() => { Tipo = TipoVar.INT; });
        botonesTipo[1].OnPress += (() => { Tipo = TipoVar.FLOAT; });
        botonesTipo[2].OnPress += (() => { Tipo = TipoVar.BOOLEAN; });
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    public void Restart()
    {
        buttons[7].gameObject.SetActive(false);
        tipo = TipoVar.INT;
        nivelDeProteccion = ProteccionVar.PUBLIC;
        cabecera.text = "";
        nombreInput.text = "";
        proteccionString = "public";
        tipoString = "int";
        TrimString();
    }
#endregion

#region MetodosTween
    public void OpenNew(){
        Open();
        Restart();
    }

    public void End(){
        Create();
        Close();
    }
#endregion

#region Metodos
    public void TrimString()
    {

        nombreInput.text = nombreInput.text.Trim();
        buttons[7].gameObject.SetActive(true);
        textoError.gameObject.SetActive(false);
        cabecera.text = proteccionString + " " + tipoString + " " + nombreInput.text;

        bool repeat = false;

        if(!repeat){
            textoError.text = "Este nombre esta reservado";
        }

        foreach(string i in CreadorMetodos.Instance.metodos.Keys){
            repeat = nombreInput.text.Equals(i, StringComparison.InvariantCultureIgnoreCase);
        }

        if(!repeat){
            textoError.text = "Este nombre esta en uso por otra variable";
        }

        Debug.Log(CreadorObjetos.Instance.variablesInt.Count);
        for(int i=0; i<CreadorObjetos.Instance.variablesInt.Count && !repeat; i++){
            repeat = nombreInput.text.Equals(CreadorObjetos.Instance.variablesInt[i].Nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for(int i=0; i<CreadorObjetos.Instance.variablesFloat.Count && !repeat; i++){
            repeat = nombreInput.text.Equals(CreadorObjetos.Instance.variablesFloat[i].Nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for(int i=0; i<CreadorObjetos.Instance.variablesBoolean.Count && !repeat; i++){
            repeat = nombreInput.text.Equals(CreadorObjetos.Instance.variablesBoolean[i].Nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if(!repeat){
            textoError.text = "Este nombre esta en uso por la clase";
        }

        if(!repeat){
            repeat = nombreInput.text.Equals(CreadorObjetos.Instance.nombreInput.text, StringComparison.InvariantCultureIgnoreCase);
        }

        if (repeat)
        {
            buttons[7].gameObject.SetActive(false);
            textoError.gameObject.SetActive(true);
        }

        if (nombreInput.text.Length > nombreInput.characterLimit)
        {
            nombreInput.text = nombreInput.text.Remove(nombreInput.characterLimit,1);
        }

        if(nombreInput.text.Length == 0){
            buttons[7].gameObject.SetActive(false); 
        }

    }

    public void Create()
    {
        switch (tipo)
        {
            case TipoVar.INT:
                GameObject intObject = Instantiate(bundle.LoadAsset<GameObject>("IntVariable"), Vector3.zero, Quaternion.identity);
                IntVariable intVar = intObject.GetComponent<IntVariable>();
                intVar.Nombre = nombreInput.text;
                intVar.Proteccion = NivelDeProteccion;
                CreadorObjetos.Instance.variablesInt.Add(intVar);
                break;
            case TipoVar.FLOAT:
                GameObject floatObject = Instantiate(bundle.LoadAsset<GameObject>("FloatVariable"), Vector3.zero, Quaternion.identity);
                FloatVariable floatVar = floatObject.GetComponent<FloatVariable>();
                floatVar.Nombre = nombreInput.text;
                floatVar.Proteccion = NivelDeProteccion;
                CreadorObjetos.Instance.variablesFloat.Add(floatVar);
                break;
            case TipoVar.BOOLEAN:
                GameObject booleanObject = Instantiate(bundle.LoadAsset<GameObject>("BoolVariable"), Vector3.zero, Quaternion.identity);
                BoolVariable booleanVar = booleanObject.GetComponent<BoolVariable>();
                booleanVar.Nombre = nombreInput.text;
                booleanVar.Proteccion = NivelDeProteccion;
                CreadorObjetos.Instance.variablesBoolean.Add(booleanVar);
                break;
        }
    }
#endregion
}
