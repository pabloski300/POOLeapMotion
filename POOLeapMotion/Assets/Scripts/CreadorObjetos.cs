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

    int numberVariables;
    public int NumberVariables
    {
        get { return numberVariables; }
        set
        {
            numberVariables = value;

            if (numberVariables > 2)
            {
                buttons[0].gameObject.SetActive(false);
            }
            else
            {
                buttons[0].gameObject.SetActive(true);
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
                buttons[1].gameObject.SetActive(false);
            }
            else
            {
                buttons[1].gameObject.SetActive(true);
            }

        }
    }

    AssetBundle bundle;

    ObjetoBase objectToModify;
    int indiceLinea;

    bool modify;

    #region Inicializacion
    private void Start()
    {
        nombreInput.inputValidator = InputValidationAlphaOnly.CreateInstance<InputValidationAlphaOnly>();
        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "objeto"));

        numberVariables = 0;
        numberMethods = 0;

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
        variablesInt.Clear();
        variablesFloat.Clear();
        variablesBoolean.Clear();
        metodos.Clear();
        nombreInput.text = "";
        buttons[3].gameObject.SetActive(false);
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
        Restart();
        Open();
    }

    public void OpenModify(ObjetoBase objeto)
    {
        Restart();
        Open();
        PanelIzquierdo.Instance.OpenNew();
        objectToModify = objeto;
        for(int i=0; i<objectToModify.variablesInt.Count; i++){
            variablesInt.Add(objectToModify.variablesInt[i]);
            PanelIzquierdo.Instance.AddVariable("int");
        }
        for(int i=0; i<objectToModify.variablesFloat.Count; i++){
            variablesFloat.Add(objectToModify.variablesFloat[i]);
            PanelIzquierdo.Instance.AddVariable("float");
        }
        for(int i=0; i<objectToModify.variablesBool.Count; i++){
            variablesBoolean.Add(objectToModify.variablesBool[i]);
            PanelIzquierdo.Instance.AddVariable("bool");
        }
        for(int i=0; i<objectToModify.metodos.Count; i++){
            metodos.Add(objectToModify.metodos[i].nombre,objectToModify.metodos[i]);
            PanelIzquierdo.Instance.AddMetodo(objectToModify.metodos[i].nombre);
        }
        nombreInput.text = objectToModify.nombre;
        modify = true;
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
        nombreInput.gameObject.SetActive(false);
    }

    #endregion

    #region Metodos
    public void TrimString()
    {
        textoError.gameObject.SetActive(false);
        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(true);
        buttons[3].gameObject.SetActive(true);
        nombreInput.text = nombreInput.text.Trim();
        cabecera.text = "public class " + nombreInput.text;

        bool repeat = false;

        if (!repeat)
        {
            textoError.text = "Este nombre esta en uso por una variable";
        }

        for (int i = 0; i < variablesInt.Count && !repeat; i++)
        {
            repeat = nombreInput.text.Equals(variablesInt[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for (int i = 0; i < variablesFloat.Count && !repeat; i++)
        {
            repeat = nombreInput.text.Equals(variablesFloat[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for (int i = 0; i < variablesBoolean.Count && !repeat; i++)
        {
            repeat = nombreInput.text.Equals(variablesBoolean[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if (!repeat)
        {
            textoError.text = "Este nombre esta en uso por otra clase";
        }

        for (int i = 0; i < Manager.Instance.anchorablePrefs.Count && !repeat; i++)
        {
            repeat = nombreInput.text.Equals(Manager.Instance.anchorablePrefs[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if(modify && repeat){
            repeat = !(nombreInput.text == objectToModify.nombre);
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

        if (nombreInput.text.Length == 0)
        {
            buttons[3].gameObject.SetActive(false);
        }
    }

    public void Create()
    {

        if(modify)
        {
            Manager.Instance.anchorablePrefs.Remove(objectToModify);
            Destroy(objectToModify.gameObject);
        }

        GameObject objeto = Instantiate(bundle.LoadAsset<GameObject>("ObjetoBasico"), Vector3.zero, Quaternion.identity);
        ObjetoBase objetoScript = objeto.GetComponent<ObjetoBase>();

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

        Manager.Instance.anchorablePrefs.Add(objeto.GetComponent<ObjetoBase>());
    }

    void CreateNew(){
        GameObject objeto = Instantiate(bundle.LoadAsset<GameObject>("ObjetoBasico"), Vector3.zero, Quaternion.identity);
        ObjetoBase objetoScript = objeto.GetComponent<ObjetoBase>();

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

        Manager.Instance.anchorablePrefs.Add(objeto.GetComponent<ObjetoBase>());
    }

    void CreateModify(){}
    #endregion

}
