
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

    public static CreadorObjetos Instance;

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
                GetButton("AñadirVariable").transform.parent.position = new Vector3(GetButton("AñadirVariable").transform.position.x,
                                                                            panel.lineasVariables[numberVariables].transform.position.y,
                                                                            GetButton("AñadirVariable").transform.position.z);
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
                GetButton("AñadirMetodo").transform.parent.position = new Vector3(GetButton("AñadirMetodo").transform.position.x,
                                                                            panel.lineasMetodos[numberMethods].transform.position.y,
                                                                            GetButton("AñadirMetodo").transform.position.z);
                GetButton("AñadirMetodo").gameObject.SetActive(true);
            }

        }
    }

    public ObjetoBase objectToModify;
    int indiceLinea;

    public Material colorRep;
    Color finalColor;
    Color FinalColor
    {
        set
        {
            finalColor = value;
            colorRep.color = finalColor;
        }
    }

    bool modify;

    public PanelIzquierdo panel;

    #region Inicializacion
    private new void Awake()
    {
        base.Awake();

        numberVariables = 0;
        numberMethods = 0;

        nombreInput.gameObject.SetActive(true);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        GetButton("Finalizar").OnPress += (()=>End());
    }

    public void Restart()
    {
        variablesInt.Clear();
        variablesFloat.Clear();
        variablesBoolean.Clear();
        metodos.Clear();
        nombreInput.text = "";
        GetButton("Finalizar").gameObject.SetActive(false);
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
        GenerateColor();
    }

    public void OpenModify(ObjetoBase objeto)
    {
        Restart();
        Open();
        PanelIzquierdo.Instance.OpenNew();
        objectToModify = objeto;
        for (int i = 0; i < objectToModify.variablesInt.Count; i++)
        {
            variablesInt.Add(objectToModify.variablesInt[i]);
            PanelIzquierdo.Instance.AddVariable("int");
            numberVariables++;
        }
        for (int i = 0; i < objectToModify.variablesFloat.Count; i++)
        {
            variablesFloat.Add(objectToModify.variablesFloat[i]);
            PanelIzquierdo.Instance.AddVariable("float");
            numberVariables++;
        }
        for (int i = 0; i < objectToModify.variablesBool.Count; i++)
        {
            variablesBoolean.Add(objectToModify.variablesBool[i]);
            PanelIzquierdo.Instance.AddVariable("bool");
            numberVariables++;
        }
        for (int i = 0; i < objectToModify.metodos.Count; i++)
        {
            metodos.Add(objectToModify.metodos[i].nombre, objectToModify.metodos[i]);
            PanelIzquierdo.Instance.AddMetodo(objectToModify.metodos[i].nombre);
            numberMethods++;
        }
        nombreInput.text = objectToModify.nombre;
        nombreInput.Select();
        nombreInput.stringPosition = nombreInput.text.Length;
        modify = true;
        FinalColor = objectToModify.Material.color;
        TrimString();
    }

    public new void Open()
    {
        nombreInput.gameObject.SetActive(true);
        base.Open();
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
        GetButton("Finalizar").gameObject.SetActive(true);
        nombreInput.text = nombreInput.text.Trim();
        cabecera.text = "public class " + nombreInput.text;

        bool repeat = nombreInput.text.Compare(this, modify);

        if (repeat)
        {
            GetButton("Finalizar").gameObject.SetActive(false);
            textoError.gameObject.SetActive(true);
        }

        if (nombreInput.text.Length > nombreInput.characterLimit)
        {
            nombreInput.text = nombreInput.text.Remove(nombreInput.characterLimit, 1);
        }

        if (nombreInput.text.Length == 0)
        {
            GetButton("Finalizar").gameObject.SetActive(false);
        }
    }

    public void Create()
    {

        if (modify)
        {
            MenuGrid.Instance.anchorablePrefs.Remove(objectToModify);
            MenuGrid.Instance.RemoveOfTypeObject(objectToModify);
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

        //Debug.Log(s);

        objetoScript.codigo += s;

        objetoScript.Material = new Material(Shader.Find("Standard"));
        objetoScript.Material.color = finalColor;

        MenuGrid.Instance.anchorablePrefs.Add(objetoScript.gameObject.GetComponent<ObjetoBase>());
    }

    public void GenerateColor()
    {
        FinalColor = Manager.Instance.GenerateColor();
    }

    public void ReGenerateColor()
    {
        FinalColor = Manager.Instance.GenerateColor((finalColor.r + finalColor.g + finalColor.b).ToString());
    }
    #endregion

}
