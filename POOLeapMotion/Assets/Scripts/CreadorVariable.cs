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

    bool modify = false;

    IntVariable intVarToModify;
    FloatVariable floatVarToModify;
    BoolVariable boolVarToModify;

    int indiceLinea = 0;

    #region Inicializacion
    private new void Start()
    {
        base.Start();
        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "variables"));
        nombreInput.inputValidator = InputValidationAlphaOnly.CreateInstance<InputValidationAlphaOnly>();
        botonesProteccion[0].OnPress += (() => { NivelDeProteccion = ProteccionVar.PUBLIC; });
        botonesProteccion[1].OnPress += (() => { NivelDeProteccion = ProteccionVar.PRIVATE; });
        botonesProteccion[2].OnPress += (() => { NivelDeProteccion = ProteccionVar.PROTECTED; });
        botonesTipo[0].OnPress += (() => { Tipo = TipoVar.INT; });
        botonesTipo[1].OnPress += (() => { Tipo = TipoVar.FLOAT; });
        botonesTipo[2].OnPress += (() => { Tipo = TipoVar.BOOLEAN; });
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
        buttons[7].gameObject.SetActive(false);
        tipo = TipoVar.INT;
        nivelDeProteccion = ProteccionVar.PUBLIC;
        cabecera.text = "";
        nombreInput.text = "";
        proteccionString = "public";
        tipoString = "int";
        intVarToModify = null;
        floatVarToModify = null;
        boolVarToModify = null;
        TrimString();
        modify = false;
    }
    #endregion

    #region MetodosTween
    public new void Open(){
        nombreInput.gameObject.SetActive(true);
        nombreInput.Select();
        base.Open();
    }

    public void OpenNew()
    {
        Open();
        Restart();
    }

    public void OpenModifyInt(IntVariable var, int i)
    {
        Open();
        Restart();
        intVarToModify = var;
        tipo = TipoVar.INT;
        nivelDeProteccion = var.proteccion;
        nombreInput.text = var.nombre;
        indiceLinea = i;
        modify = true;
        TrimString();
    }

    public void OpenModifyBool(BoolVariable var, int i)
    {
        Open();
        Restart();
        boolVarToModify = var;
        tipo = TipoVar.BOOLEAN;
        nivelDeProteccion = var.proteccion;
        nombreInput.text = var.nombre;
        indiceLinea = i;
        modify = true;
        TrimString();
    }

    public void OpenModifyFloat(FloatVariable var, int i)
    {
        Open();
        Restart();
        floatVarToModify = var;
        tipo = TipoVar.FLOAT;
        nivelDeProteccion = var.proteccion;
        nombreInput.text = var.nombre;
        indiceLinea = i;
        modify = true;
        TrimString();
    }

    public new void Close(){
        base.Close();
        nombreInput.gameObject.SetActive(false);
    }

    public void End()
    {
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

        if (!repeat)
        {
            textoError.text = "Este nombre esta en uso por otra variable";
        }

        for (int i = 0; i < CreadorObjetos.Instance.variablesInt.Count && !repeat; i++)
        {
            repeat = nombreInput.text.Equals(CreadorObjetos.Instance.variablesInt[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for (int i = 0; i < CreadorObjetos.Instance.variablesFloat.Count && !repeat; i++)
        {
            repeat = nombreInput.text.Equals(CreadorObjetos.Instance.variablesFloat[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for (int i = 0; i < CreadorObjetos.Instance.variablesBoolean.Count && !repeat; i++)
        {
            repeat = nombreInput.text.Equals(CreadorObjetos.Instance.variablesBoolean[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if(modify && repeat && intVarToModify != null){
            repeat = !(nombreInput.text == intVarToModify.nombre);
        }

        if(modify && repeat && floatVarToModify != null){
            repeat = !(nombreInput.text == floatVarToModify.nombre);
        }

        if(modify && repeat && boolVarToModify != null){
            repeat = !(nombreInput.text == boolVarToModify.nombre);
        }

        if (!repeat)
        {
            textoError.text = "Este nombre esta en uso por la clase";
        }

        if (!repeat)
        {
            repeat = nombreInput.text.Equals(CreadorObjetos.Instance.nombreInput.text, StringComparison.InvariantCultureIgnoreCase);
        }

        if (repeat)
        {
            buttons[7].gameObject.SetActive(false);
            textoError.gameObject.SetActive(true);
        }

        if (nombreInput.text.Length > nombreInput.characterLimit)
        {
            nombreInput.text = nombreInput.text.Remove(nombreInput.characterLimit, 1);
        }

        if (nombreInput.text.Length == 0)
        {
            buttons[7].gameObject.SetActive(false);
        }

    }

    public void Create()
    {
        if (!modify)
        {
            CreateNew();
        }
        else
        {
            CreateModify();
        }
    }

    public void CreateNew()
    {
        switch (tipo)
        {
            case TipoVar.INT:
                GameObject intObject = Instantiate(bundle.LoadAsset<GameObject>("IntVariable"), Vector3.zero, Quaternion.identity);
                IntVariable intVar = intObject.GetComponent<IntVariable>();
                intVar.nombre = nombreInput.text;
                intVar.proteccion = NivelDeProteccion;
                CreadorObjetos.Instance.variablesInt.Add(intVar);
                PanelIzquierdo.Instance.AddVariable("int");
                break;
            case TipoVar.FLOAT:
                GameObject floatObject = Instantiate(bundle.LoadAsset<GameObject>("FloatVariable"), Vector3.zero, Quaternion.identity);
                FloatVariable floatVar = floatObject.GetComponent<FloatVariable>();
                floatVar.nombre = nombreInput.text;
                floatVar.proteccion = NivelDeProteccion;
                CreadorObjetos.Instance.variablesFloat.Add(floatVar);
                PanelIzquierdo.Instance.AddVariable("float");
                break;
            case TipoVar.BOOLEAN:
                GameObject booleanObject = Instantiate(bundle.LoadAsset<GameObject>("BoolVariable"), Vector3.zero, Quaternion.identity);
                BoolVariable booleanVar = booleanObject.GetComponent<BoolVariable>();
                booleanVar.nombre = nombreInput.text;
                booleanVar.proteccion = NivelDeProteccion;
                CreadorObjetos.Instance.variablesBoolean.Add(booleanVar);
                PanelIzquierdo.Instance.AddVariable("bool");
                break;
        }
        CreadorObjetos.Instance.NumberVariables ++;
    }

    public void CreateModify()
    {

        if (intVarToModify != null)
        {
            CreadorObjetos.Instance.variablesInt.Remove(intVarToModify);
            Destroy(intVarToModify.gameObject);
        }

        if (floatVarToModify != null)
        {
            CreadorObjetos.Instance.variablesFloat.Remove(floatVarToModify);
            Destroy(floatVarToModify.gameObject);
        }

        if (boolVarToModify != null)
        {
            CreadorObjetos.Instance.variablesBoolean.Remove(boolVarToModify);
            Destroy(boolVarToModify.gameObject);
        }

        switch (tipo)
        {
            case TipoVar.INT:
                GameObject intObject = Instantiate(bundle.LoadAsset<GameObject>("IntVariable"), Vector3.zero, Quaternion.identity);
                IntVariable intVar = intObject.GetComponent<IntVariable>();
                intVar.nombre = nombreInput.text;
                intVar.proteccion = NivelDeProteccion;
                CreadorObjetos.Instance.variablesInt.Add(intVar);
                PanelIzquierdo.Instance.AddVariable(indiceLinea, "int");
                break;
            case TipoVar.FLOAT:
                GameObject floatObject = Instantiate(bundle.LoadAsset<GameObject>("FloatVariable"), Vector3.zero, Quaternion.identity);
                FloatVariable floatVar = floatObject.GetComponent<FloatVariable>();
                floatVar.nombre = nombreInput.text;
                floatVar.proteccion = NivelDeProteccion;
                CreadorObjetos.Instance.variablesFloat.Add(floatVar);
                PanelIzquierdo.Instance.AddVariable(indiceLinea, "float");
                break;
            case TipoVar.BOOLEAN:
                GameObject booleanObject = Instantiate(bundle.LoadAsset<GameObject>("BoolVariable"), Vector3.zero, Quaternion.identity);
                BoolVariable booleanVar = booleanObject.GetComponent<BoolVariable>();
                booleanVar.nombre = nombreInput.text;
                booleanVar.proteccion = NivelDeProteccion;
                CreadorObjetos.Instance.variablesBoolean.Add(booleanVar);
                PanelIzquierdo.Instance.AddVariable(indiceLinea, "bool");
                break;
        }
    }
    #endregion
}
