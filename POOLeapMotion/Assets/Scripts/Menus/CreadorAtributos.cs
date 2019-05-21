using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Leap.Unity.Interaction;
using System;

public class CreadorAtributos : CustomMenu
{
    public enum TipoVar
    {
        Int,
        Float,
        Boolean,
    }

    public enum ProteccionVar
    {
        Public,
        Private,
        Protected
    }

    [Header("UI")]
    public TMP_InputField nombreInput;
    public TextMeshPro cabecera;
    public TextMeshPro textoError;
    public ToggleGroup tipoGroup;
    public ToggleGroup proteccionGroup;

    ProteccionVar nivelDeProteccion = ProteccionVar.Public;
    string proteccionString = "";
    ProteccionVar NivelDeProteccion
    {
        get { return nivelDeProteccion; }
        set
        {
            nivelDeProteccion = value;
            switch (value)
            {
                case ProteccionVar.Public:
                    proteccionString = "public";
                    break;
                case ProteccionVar.Private:
                    proteccionString = "private";
                    break;
                case ProteccionVar.Protected:
                    proteccionString = "protected";
                    break;
            }
            TrimString();
        }
    }

    TipoVar tipo = TipoVar.Int;
    string tipoString;
    TipoVar Tipo
    {
        get { return tipo; }
        set
        {
            tipo = value;
            switch (value)
            {
                case TipoVar.Int:
                    tipoString = "int";
                    break;
                case TipoVar.Float:
                    tipoString = "float";
                    break;
                case TipoVar.Boolean:
                    tipoString = "boolean";
                    break;
            }
            TrimString();
        }
    }

    bool modify = false;

    [HideInInspector]
    public IntVariable intVarToModify;
    [HideInInspector]
    public FloatVariable floatVarToModify;
    [HideInInspector]
    public BoolVariable boolVarToModify;

    int indiceLinea = 0;

    string tipoToggle;
    string proteccionToggle;

    CreadorObjetos c;
    PanelIzquierdo p;

    #region Inicializacion
    public override void Init()
    {
        base.Init();

        c = (CreadorObjetos)Manager.Instance.GetMenu("CreadorObjetos");
        p = (PanelIzquierdo)Manager.Instance.GetMenu("PanelIzquierdo");

        GetButton("Public").OnPress += (() => { NivelDeProteccion = ProteccionVar.Public; });
        GetButton("Private").OnPress += (() => { NivelDeProteccion = ProteccionVar.Private; });
        GetButton("Protected").OnPress += (() => { NivelDeProteccion = ProteccionVar.Protected; });
        GetButton("Int").OnPress += (() => { Tipo = TipoVar.Int; });
        GetButton("Float").OnPress += (() => { Tipo = TipoVar.Float; });
        GetButton("Bool").OnPress += (() => { Tipo = TipoVar.Boolean; });

        GetButton("Finalizar").OnPress += (() => End());

        nombreInput.gameObject.SetActive(false);
    }

    public void Restart()
    {
        tipo = TipoVar.Int;
        nivelDeProteccion = ProteccionVar.Public;
        cabecera.text = "";
        nombreInput.text = "";
        proteccionString = "public";
        tipoString = "int";
        intVarToModify = null;
        floatVarToModify = null;
        boolVarToModify = null;
        modify = false;
    }
    #endregion

    #region MetodosTween
    public new void Open()
    {
        nombreInput.gameObject.SetActive(true);
        base.Open();
    }

    public void OpenNew()
    {
        Open();
        Restart();
        proteccionToggle = "Public";
        tipoToggle = "Int";
    }

    public void OpenModifyInt(IntVariable var, int i)
    {
        Open();
        Restart();
        intVarToModify = var;
        tipo = TipoVar.Int;
        nivelDeProteccion = var.proteccion;
        proteccionToggle = nivelDeProteccion.ToString();
        tipoToggle = "Int";
        nombreInput.text = var.nombre;
        nombreInput.stringPosition = nombreInput.text.Length;
        indiceLinea = i;
        modify = true;
    }

    public void OpenModifyBool(BoolVariable var, int i)
    {
        Open();
        Restart();
        boolVarToModify = var;
        tipo = TipoVar.Boolean;
        nivelDeProteccion = var.proteccion;
        proteccionToggle = nivelDeProteccion.ToString();
        tipoToggle = "Bool";
        nombreInput.text = var.nombre;
        nombreInput.stringPosition = nombreInput.text.Length;
        indiceLinea = i;
        modify = true;
    }

    public void OpenModifyFloat(FloatVariable var, int i)
    {
        Open();
        Restart();
        floatVarToModify = var;
        tipo = TipoVar.Float;
        nivelDeProteccion = var.proteccion;
        proteccionToggle = nivelDeProteccion.ToString();
        tipoToggle = "Float";
        nombreInput.text = var.nombre;
        nombreInput.stringPosition = nombreInput.text.Length;
        indiceLinea = i;
        modify = true;
    }

    public override void Callback()
    {
        proteccionGroup.Reset(GetButton(proteccionToggle));
        tipoGroup.Reset(GetButton(tipoToggle));
        TrimString();
    }

    public new void Close()
    {
        base.Close();
        nombreInput.DeactivateInputField();
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
        if (nombreInput.text.Length == 0)
        {
            GetButton("Finalizar").Locked = true;
            textoError.gameObject.SetActive(true);
            if (Manager.Instance.english)
            {
                textoError.text = "Please, write a name";
            }
            else
            {
                textoError.text = "Por favor, introduce un nombre";
            }
            return;
        }

        cabecera.text = proteccionString + " " + tipoString + " " + nombreInput.text;

        bool repeat = nombreInput.text.Compare(this, modify);

        if (repeat)
        {
            GetButton("Finalizar").Blocked = true;
            textoError.gameObject.SetActive(true);
            return;
        }

        if (nombreInput.text.Length > nombreInput.characterLimit)
        {
            nombreInput.text = nombreInput.text.Remove(nombreInput.characterLimit, 1);
        }

        GetButton("Finalizar").Blocked = false;
        textoError.gameObject.SetActive(false);
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
            case TipoVar.Int:
                IntVariable intVar = Instantiate(Manager.Instance.intVariablePrefab, new Vector3(999, 999, 999), Quaternion.identity);
                intVar.nombre = nombreInput.text;
                intVar.proteccion = NivelDeProteccion;
                c.variablesInt.Add(intVar);
                p.AddVariable("int");
                break;
            case TipoVar.Float:
                FloatVariable floatVar = Instantiate(Manager.Instance.floatVariablePrefab, new Vector3(999, 999, 999), Quaternion.identity);
                floatVar.nombre = nombreInput.text;
                floatVar.proteccion = NivelDeProteccion;
                c.variablesFloat.Add(floatVar);
                p.AddVariable("float");
                break;
            case TipoVar.Boolean:
                BoolVariable booleanVar = Instantiate(Manager.Instance.boolVariablePrefab, new Vector3(999, 999, 999), Quaternion.identity);
                booleanVar.nombre = nombreInput.text;
                booleanVar.proteccion = NivelDeProteccion;
                c.variablesBoolean.Add(booleanVar);
                p.AddVariable("bool");
                break;
        }
        c.NumberVariables++;
    }

    public void CreateModify()
    {

        if (intVarToModify != null)
        {
            c.variablesInt.Remove(intVarToModify);
            Destroy(intVarToModify.gameObject);
        }

        if (floatVarToModify != null)
        {
            c.variablesFloat.Remove(floatVarToModify);
            Destroy(floatVarToModify.gameObject);
        }

        if (boolVarToModify != null)
        {
            c.variablesBoolean.Remove(boolVarToModify);
            Destroy(boolVarToModify.gameObject);
        }

        switch (tipo)
        {
            case TipoVar.Int:
                IntVariable intVar = Instantiate(Manager.Instance.intVariablePrefab, new Vector3(999, 999, 999), Quaternion.identity);
                intVar.nombre = nombreInput.text;
                intVar.proteccion = NivelDeProteccion;
                c.variablesInt.Add(intVar);
                p.AddVariable(indiceLinea, "int");
                break;
            case TipoVar.Float:
                FloatVariable floatVar = Instantiate(Manager.Instance.floatVariablePrefab, new Vector3(999, 999, 999), Quaternion.identity);
                floatVar.nombre = nombreInput.text;
                floatVar.proteccion = NivelDeProteccion;
                c.variablesFloat.Add(floatVar);
                p.AddVariable(indiceLinea, "float");
                break;
            case TipoVar.Boolean:
                BoolVariable booleanVar = Instantiate(Manager.Instance.boolVariablePrefab, new Vector3(999, 999, 999), Quaternion.identity);
                booleanVar.nombre = nombreInput.text;
                booleanVar.proteccion = NivelDeProteccion;
                c.variablesBoolean.Add(booleanVar);
                p.AddVariable(indiceLinea, "bool");
                break;
        }
    }

    public void Load(string type, string proteccion, string nombre)
    {
        ProteccionVar p = ProteccionVar.Public;

        switch (proteccion)
        {
            case "Public":
                p = ProteccionVar.Public;
                break;
            case "Private":
                p = ProteccionVar.Public;
                break;
            case "Protected":
                p = ProteccionVar.Public;
                break;
        }

        switch (type)
        {
            case "int":
                IntVariable intVar = Instantiate(Manager.Instance.intVariablePrefab, new Vector3(999, 999, 999), Quaternion.identity);
                intVar.nombre = nombre;
                intVar.proteccion = p;
                c.variablesInt.Add(intVar);
                break;
            case "float":
                FloatVariable floatVar = Instantiate(Manager.Instance.floatVariablePrefab, new Vector3(999, 999, 999), Quaternion.identity);
                floatVar.nombre = nombre;
                floatVar.proteccion = p;
                c.variablesFloat.Add(floatVar);
                break;
            case "bool":
                BoolVariable booleanVar = Instantiate(Manager.Instance.boolVariablePrefab, new Vector3(999, 999, 999), Quaternion.identity);
                booleanVar.nombre = nombre;
                booleanVar.proteccion = p;
                c.variablesBoolean.Add(booleanVar);
                break;
        }
    }
    #endregion
}
