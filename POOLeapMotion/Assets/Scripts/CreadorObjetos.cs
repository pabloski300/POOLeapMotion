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
    private new void Awake()
    {
        base.Awake();

        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "objeto"));

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
        Open();
        Restart();
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
            numberVariables++;
        }
        for(int i=0; i<objectToModify.variablesFloat.Count; i++){
            variablesFloat.Add(objectToModify.variablesFloat[i]);
            PanelIzquierdo.Instance.AddVariable("float");
            numberVariables++;
        }
        for(int i=0; i<objectToModify.variablesBool.Count; i++){
            variablesBoolean.Add(objectToModify.variablesBool[i]);
            PanelIzquierdo.Instance.AddVariable("bool");
            numberVariables++;
        }
        for(int i=0; i<objectToModify.metodos.Count; i++){
            metodos.Add(objectToModify.metodos[i].nombre,objectToModify.metodos[i]);
            PanelIzquierdo.Instance.AddMetodo(objectToModify.metodos[i].nombre);
            numberMethods++;
        }
        nombreInput.text = objectToModify.nombre;
        nombreInput.Select();
        nombreInput.stringPosition = nombreInput.text.Length;
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
        nombreInput.DeactivateInputField();
        nombreInput.gameObject.SetActive(false);
    }

    #endregion

    #region Metodos
    public void TrimString()
    {
        textoError.gameObject.SetActive(false);
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

        for (int i = 0; i < MenuGrid.Instance.anchorablePrefs.Count && !repeat; i++)
        {
            repeat = nombreInput.text.Equals(MenuGrid.Instance.anchorablePrefs[i].nombre, StringComparison.InvariantCultureIgnoreCase);
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
            MenuGrid.Instance.anchorablePrefs.Remove(objectToModify);
            MenuGrid.Instance.RemoveOfTypeObject(objectToModify);
            Destroy(objectToModify.gameObject);
        }

        GameObject objeto = Instantiate(bundle.LoadAsset<GameObject>("ObjetoBasico"), new Vector3(999,999,999), Quaternion.identity);
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

        bool colorRepeat;

        float r;
        float g;
        float b;

        do{
            colorRepeat = false;
            System.Random random = new System.Random();
            
            r = UnityEngine.Random.Range(0f,1f);
            g = UnityEngine.Random.Range(0f,1f);
            b = UnityEngine.Random.Range(0f,1f);

            for(int i=0; i<MenuGrid.Instance.anchorablePrefs.Count && !colorRepeat; i++){
                colorRepeat = MenuGrid.Instance.anchorablePrefs[i].Material.color.r == r && 
                MenuGrid.Instance.anchorablePrefs[i].Material.color.g == g &&
                MenuGrid.Instance.anchorablePrefs[i].Material.color.b == b;
            }

            for(int i=0; i<MenuGrid.Instance.variables.Count && !colorRepeat; i++){
                colorRepeat = MenuGrid.Instance.variables[i].ColorVariable.color.r == r && 
                MenuGrid.Instance.variables[i].ColorVariable.color.g == g &&
                MenuGrid.Instance.variables[i].ColorVariable.color.b == b;
            }

        } while(colorRepeat);

        objetoScript.Material = new Material(Shader.Find("Standard"));
        objetoScript.Material.color = new Color(r,g,b);

        MenuGrid.Instance.anchorablePrefs.Add(objeto.GetComponent<ObjetoBase>());
    }
    #endregion

}
