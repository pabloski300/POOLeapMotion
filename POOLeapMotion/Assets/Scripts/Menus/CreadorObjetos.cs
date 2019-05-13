
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
    [HideInInspector]
    public Dictionary<string, MetodoBase> metodos = new Dictionary<string, MetodoBase>();
    Vector3 localPosAM;
    Vector3 localPosAA;

    int numberVariables;
    public int NumberVariables
    {
        get { return numberVariables; }
        set
        {
            numberVariables = value;

            if (numberVariables > 2)
            {
                GetButton("AñadirVariable").gameObject.SetActive(false);
            }
            else
            {
                UnlockButtonsDelayed(0.5f);
                GetButton("AñadirVariable").transform.parent.localPosition = new Vector3(GetButton("AñadirVariable").transform.parent.localPosition.x,
                                                                            p.lineasVariables[numberVariables].transform.localPosition.y,
                                                                            GetButton("AñadirVariable").transform.parent.localPosition.z);
                GetButton("AñadirVariable").gameObject.SetActive(true);
            }
        }
    }

    int numberMethods;
    public int NumberMethods
    {
        get { return numberMethods; }
        set
        {
            numberMethods = value;

            if (numberMethods > 2)
            {
                GetButton("AñadirMetodo").gameObject.SetActive(false);
            }
            else
            {
                UnlockButtonsDelayed(0.5f);
                GetButton("AñadirMetodo").transform.parent.localPosition = new Vector3(GetButton("AñadirMetodo").transform.parent.localPosition.x,
                                                                            p.lineasMetodos[numberMethods].transform.localPosition.y,
                                                                            GetButton("AñadirMetodo").transform.parent.localPosition.z);
                GetButton("AñadirMetodo").gameObject.SetActive(true);
            }

        }
    }

    public ObjetoBase objectToModify;
    int indiceLinea;

    Material matClase;

    Color SetColor
    {
        set
        {
        matClase = new Material(Shader.Find("Standard"));
        matClase.color = value;
        }
    }

    bool modify;

    PanelIzquierdo p;
    MenuGrid m;
    MenuClases mc;

    public GameObject b;

    #region Inicializacion
    public override void Init()
    {
        base.Init();

        p = (PanelIzquierdo)Manager.Instance.GetMenu("PanelIzquierdo");
        m = (MenuGrid)Manager.Instance.GetMenu("MenuGrid");
        mc = (MenuClases)Manager.Instance.GetMenu("MenuClases");

        numberVariables = 0;
        numberMethods = 0;

        nombreInput.gameObject.SetActive(true);

        GetButton("Finalizar").OnPress += (()=>End());

        //localPosAM = GetButton("AñadirMetodo").transform.localPosition;
        //localPosAA = GetButton("AñadirVariable").transform.localPosition;
    }

    public void Restart()
    {
        variablesInt.Clear();
        variablesFloat.Clear();
        variablesBoolean.Clear();
        metodos.Clear();
        nombreInput.text = "";
        numberVariables = 0;
        numberMethods = 0;
        objectToModify = null;
        indiceLinea = 0;
        modify = false;
    }
    #endregion

    #region MetodosTween

    public void OpenNew()
    {
        Open();
        Restart();
        p.title.text = nombreInput.text;
        SetColor = Manager.Instance.GenerateColor();
        NumberVariables = 0;
        NumberMethods = 0;
    }

    public void OpenModify(ObjetoBase objeto)
    {
        Restart();
        Open();
        p.OpenNew();
        objectToModify = objeto;
        for (int i = 0; i < objectToModify.variablesInt.Count; i++)
        {
            variablesInt.Add(objectToModify.variablesInt[i]);
            p.AddVariable("int");
            numberVariables++;
        }
        for (int i = 0; i < objectToModify.variablesFloat.Count; i++)
        {
            variablesFloat.Add(objectToModify.variablesFloat[i]);
            p.AddVariable("float");
            numberVariables++;
        }
        for (int i = 0; i < objectToModify.variablesBool.Count; i++)
        {
            variablesBoolean.Add(objectToModify.variablesBool[i]);
            p.AddVariable("bool");
            numberVariables++;
        }
        for (int i = 0; i < objectToModify.metodos.Count; i++)
        {
            metodos.Add(objectToModify.metodos[i].nombre, objectToModify.metodos[i]);
            p.AddMetodo(objectToModify.metodos[i].nombre);
            numberMethods++;
        }
        nombreInput.text = objectToModify.nombre;
        nombreInput.Select();
        nombreInput.stringPosition = nombreInput.text.Length;
        modify = true;
        SetColor = objectToModify.Material.color;
        TrimString();
    }

    public new void Open()
    {
        nombreInput.gameObject.SetActive(true);
        base.Open();
    }

    public override void Callback(){
        TrimString();
        //b.transform.position = Vector3.zero;
    }

    public void End()
    {
        Create();
        Close();
    }

    public new void Close()
    {
        base.Close();
        nombreInput.DeactivateInputField();
        nombreInput.gameObject.SetActive(false);
    }

    #endregion

    #region Metodos
    public void TrimString()
    {
        textoError.gameObject.SetActive(false);
        nombreInput.text = nombreInput.text.Trim();
        cabecera.text = "public class " + nombreInput.text;

        if (nombreInput.text.Length > nombreInput.characterLimit)
        {
            nombreInput.text = nombreInput.text.Remove(nombreInput.characterLimit, 1);
        }

        if (nombreInput.text.Length == 0)
        {
            GetButton("Finalizar").Locked = true;
            return;
        }

        bool repeat = nombreInput.text.Compare(this, modify);

        if (repeat)
        {
            GetButton("Finalizar").Locked = true;
            textoError.gameObject.SetActive(true);
            return;
        }

        p.title.text = nombreInput.text;

        GetButton("Finalizar").Locked = false;
    }

    public void Create()
    {

        if (modify)
        {
            m.anchorablePrefs.Remove(objectToModify);
            m.RemoveOfTypeObject(objectToModify);
            Destroy(objectToModify.gameObject);
        }

        ObjetoBase objetoScript = Instantiate(Manager.Instance.objetoBasePrefab, new Vector3(999, 999, 999), Quaternion.identity);

        objetoScript.nombre = nombreInput.text;

        string s = cabecera.text + " {\n";

        foreach (IntVariable var in variablesInt)
        {
            objetoScript.variablesInt.Add(var);
            var.gameObject.transform.parent = objetoScript.variablesParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }
        foreach (FloatVariable var in variablesFloat)
        {
            objetoScript.variablesFloat.Add(var);
            var.gameObject.transform.parent = objetoScript.variablesParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }
        foreach (BoolVariable var in variablesBoolean)
        {
            objetoScript.variablesBool.Add(var);
            var.gameObject.transform.parent = objetoScript.variablesParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }
        foreach (MetodoBase var in metodos.Values)
        {
            objetoScript.metodos.Add(var);
            var.gameObject.transform.parent = objetoScript.metodoParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }

        s += "}";

        objetoScript.codigo += s;

        objetoScript.Material = matClase;

        mc.NumberClases ++;

        m.anchorablePrefs.Add(objetoScript.gameObject.GetComponent<ObjetoBase>());
        
        variablesBoolean.Clear();
        variablesFloat.Clear();
        variablesInt.Clear();
        metodos.Clear();
    }

    public void Load(string nombre, Material m)
    {
        ObjetoBase objetoScript = Instantiate(Manager.Instance.objetoBasePrefab, new Vector3(999, 999, 999), Quaternion.identity);

        objetoScript.nombre = nombre;

        string s = cabecera.text + " {\n";

        foreach (IntVariable var in variablesInt)
        {
            objetoScript.variablesInt.Add(var);
            var.gameObject.transform.parent = objetoScript.variablesParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }
        foreach (FloatVariable var in variablesFloat)
        {
            objetoScript.variablesFloat.Add(var);
            var.gameObject.transform.parent = objetoScript.variablesParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }
        foreach (BoolVariable var in variablesBoolean)
        {
            objetoScript.variablesBool.Add(var);
            var.gameObject.transform.parent = objetoScript.variablesParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }
        foreach (MetodoBase var in metodos.Values)
        {
            objetoScript.metodos.Add(var);
            var.gameObject.transform.parent = objetoScript.metodoParent;
            var.gameObject.transform.localScale = Vector3.one;
            s += var.WriteFile();
        }

        s += "}";

        objetoScript.codigo += s;

        objetoScript.Material = m;

        mc.NumberClases ++;

        this.m.anchorablePrefs.Add(objetoScript.gameObject.GetComponent<ObjetoBase>());

        variablesBoolean.Clear();
        variablesFloat.Clear();
        variablesInt.Clear();
        metodos.Clear();
    }
    #endregion

}
