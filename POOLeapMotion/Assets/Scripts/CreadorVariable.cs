using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Leap.Unity.Interaction;
using System;

public class CreadorVariable : MonoBehaviour
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

    public TMP_InputField nombreInput;
    public TextMeshPro cabecera;
    public GameObject textoError;
    public InteractionButton[] botonesProteccion;
    public InteractionButton[] botonesTipo;

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
    }

    public void TrimString()
    {

        nombreInput.text = nombreInput.text.Trim();
        Debug.Log(CreadorObjetos.Instance.cabecera.text);
        if (nombreInput.text.Equals(CreadorObjetos.Instance.nombreInput.text, StringComparison.InvariantCultureIgnoreCase))
        {
            Debug.Log("nombre mal");
            textoError.SetActive(true);
            return;
        }

        if (nombreInput.text.Length > nombreInput.characterLimit)
        {
            nombreInput.text = nombreInput.text.Remove(nombreInput.characterLimit,1);
            return;
        }

        Debug.Log("nombre bien");
        textoError.SetActive(false);
        cabecera.text = proteccionString + " " + tipoString + " " + nombreInput.text;

    }

    public void Restart()
    {
        tipo = TipoVar.INT;
        nivelDeProteccion = ProteccionVar.PUBLIC;
        cabecera.text = "";
        nombreInput.text = "";
        proteccionString = "public";
        tipoString = "int";
        TrimString();
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
}
