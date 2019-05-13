using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Leap.Unity.Interaction;
using UnityEngine;


public class Manager : MonoBehaviour
{
    public static Manager Instance;

    Dictionary<string, Color> colors = new Dictionary<string, Color>();

    [HideInInspector]
    public Dictionary<string, MetodoBase> metodosPrefab = new Dictionary<string, MetodoBase>();
    [HideInInspector]
    public IntVariable intVariablePrefab;
    [HideInInspector]
    public FloatVariable floatVariablePrefab;
    [HideInInspector]
    public BoolVariable boolVariablePrefab;
    [HideInInspector]
    public ObjetoBase objetoBasePrefab;
    [HideInInspector]
    public VariableObjeto variableObjetoPrefab;

    public CustomAnchor papeleraCreadorClases;
    public CustomAnchor papeleraExploradorObjetos;
    public CustomAnchor papeleraClases;

    public CustomAnchor inspeccionarVariable;
    public CustomAnchor inspeccionarMetodo;

    [HideInInspector]
    public Dictionary<string, CustomMenu> menus;

    [HideInInspector]
    public MenuGrid mg;
    [HideInInspector]
    public MenuClases mc;

    [HideInInspector]
    public Consola c;

    [HideInInspector]
    public CreadorObjetos co;
    [HideInInspector]
    public CreadorAtributos ca;
    [HideInInspector]
    public CreadorMetodos cm;

    [HideInInspector]
    public MenuInicio ini;


    AudioSource aud;
    public AudioClip[] sounds;


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "objeto"));
        objetoBasePrefab = Instantiate(bundle.LoadAsset<GameObject>("ObjetoBasico"), new Vector3(400, 400, 400), Quaternion.identity).GetComponent<ObjetoBase>();
        bundle.Unload(false);

        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "variable_objeto"));
        variableObjetoPrefab = Instantiate(bundle.LoadAsset<GameObject>("VariableObjeto"), new Vector3(500, 500, 500), Quaternion.identity).GetComponent<VariableObjeto>();
        bundle.Unload(false);

        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "variable_objeto"));
        variableObjetoPrefab = Instantiate(bundle.LoadAsset<GameObject>("VariableObjeto"), new Vector3(600, 600, 600), Quaternion.identity).GetComponent<VariableObjeto>();
        bundle.Unload(false);

        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "variables"));
        intVariablePrefab = Instantiate(bundle.LoadAsset<GameObject>("IntVariable"), new Vector3(700, 700, 700), Quaternion.identity).GetComponent<IntVariable>();
        floatVariablePrefab = Instantiate(bundle.LoadAsset<GameObject>("FloatVariable"), new Vector3(700, 700, 700), Quaternion.identity).GetComponent<FloatVariable>();
        boolVariablePrefab = Instantiate(bundle.LoadAsset<GameObject>("BoolVariable"), new Vector3(700, 700, 700), Quaternion.identity).GetComponent<BoolVariable>();
        bundle.Unload(false);

        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "metodos"));
        List<GameObject> metodos = bundle.LoadAllAssets<GameObject>().ToList();

        foreach (GameObject g in metodos)
        {
            MetodoBase m = Instantiate(g, new Vector3(800, 800, 800), Quaternion.identity).GetComponent<MetodoBase>();
            metodosPrefab.Add(m.nombre, m);
        }
        bundle.Unload(false);

        menus = new Dictionary<string, CustomMenu>();

        List<CustomMenu> menusList = FindObjectsOfType<CustomMenu>().ToList();

        foreach (CustomMenu c in menusList)
        {
            menus.Add(c.name, c);
        }

        foreach (CustomMenu c in menus.Values)
        {
            c.Init();
        }

        StringExtension.Init();

        mg = (MenuGrid)GetMenu("MenuGrid");
        mc = (MenuClases)GetMenu("MenuClases");
        c = (Consola)GetMenu("Consola");
        co = (CreadorObjetos)GetMenu("CreadorObjetos");
        ca = (CreadorAtributos)GetMenu("CreadorAtributos");
        cm = (CreadorMetodos)GetMenu("CreadorMetodos");
        ini = (MenuInicio)GetMenu("MenuInicio");

        aud = GetComponent<AudioSource>();

        string path = Path.Combine(Application.streamingAssetsPath, "save.sv");

        if (!File.Exists(path))
        {
            ini.GetButton("Cargar").Blocked = true;
        }
    }

    public Color GenerateColor()
    {
        Color color = new Color();

        float r;
        float g;
        float b;

        do
        {
            r = UnityEngine.Random.Range(0f, 1f);
            g = UnityEngine.Random.Range(0f, 1f);
            b = UnityEngine.Random.Range(0f, 1f);
        } while (colors.ContainsKey((r + g + b).ToString()));

        colors.Add((r + g + b).ToString(), color);

        color.r = r;
        color.g = g;
        color.b = b;
        color.a = 1.0f;

        return color;
    }

    public void Save()
    {
        string dataPath = Path.Combine(Application.streamingAssetsPath, "save.sv");
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream stream = File.Open(dataPath, FileMode.OpenOrCreate);

        SaveData save = new SaveData();

        for (int i = 0; i < mg.anchorablePrefs.Count; i++)
        {
            ClaseSav clase = new ClaseSav();
            clase.nombre = mg.anchorablePrefs[i].nombre;
            clase.color = new float[] { mg.anchorablePrefs[i].Material.color.r, mg.anchorablePrefs[i].Material.color.g, mg.anchorablePrefs[i].Material.color.b };
            for (int j = 0; j < mg.anchorablePrefs[i].variablesInt.Count; j++)
            {
                AtributoSav atributo = new AtributoSav();
                atributo.nombre = mg.anchorablePrefs[i].variablesInt[j].nombre;
                atributo.tipo = "int";
                atributo.proteccion = mg.anchorablePrefs[i].variablesInt[j].proteccion.ToString();
                clase.atributos.Add(atributo);
            }
            for (int j = 0; j < mg.anchorablePrefs[i].variablesFloat.Count; j++)
            {
                AtributoSav atributo = new AtributoSav();
                atributo.nombre = mg.anchorablePrefs[i].variablesFloat[j].nombre;
                atributo.tipo = "float";
                atributo.proteccion = mg.anchorablePrefs[i].variablesFloat[j].proteccion.ToString();
                clase.atributos.Add(atributo);
            }
            for (int j = 0; j < mg.anchorablePrefs[i].variablesBool.Count; j++)
            {
                AtributoSav atributo = new AtributoSav();
                atributo.nombre = mg.anchorablePrefs[i].variablesBool[j].nombre;
                atributo.tipo = "bool";
                atributo.proteccion = mg.anchorablePrefs[i].variablesBool[j].proteccion.ToString();
                clase.atributos.Add(atributo);
            }
            for (int j = 0; j < mg.anchorablePrefs[i].metodos.Count; j++)
            {
                MetodoSav metodo = new MetodoSav();
                metodo.nombre = mg.anchorablePrefs[i].metodos[j].nombre;
                clase.metodos.Add(metodo);
            }
            save.clases.Add(clase);
        }

        for (int i = 0; i < mg.variables.Count; i++)
        {
            VariableSav variable = new VariableSav();
            variable.clase = mg.variables[i].clase;
            variable.nombre = mg.variables[i].nombre;
            variable.color = new float[] { mg.variables[i].ColorVariable.color.r, mg.variables[i].ColorVariable.color.g, mg.variables[i].ColorVariable.color.b };
            save.variables.Add(variable);
        }

        for (int i = 0; i < mg.objects.Count; i++)
        {
            int x = 0;
            bool found = false;
            for (int j = 0; j < mg.anchorablePrefs.Count && !found; j++)
            {
                if (mg.objects[i].nombre == mg.anchorablePrefs[j].nombre)
                {
                    found = true;
                }
                else
                {
                    x++;
                }
            }
            save.objetos.Add(x);
        }

        binaryFormatter.Serialize(stream, save);
        stream.Close();

        ini.GetButton("Cargar").Blocked = false;

        c.Clear();
        mg.RemoveAllObjects();
        mg.RemoveAllVariables();
        mg.anchorablePrefs.Clear();
        mc.NumberClases = 0;
        mc.NumberObjetos = 0;
        mc.NumberVariables = 0;
        colors.Clear();
    }

    public void Load()
    {
        string dataPath = Path.Combine(Application.streamingAssetsPath, "save.sv");
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        FileStream stream = null;

        stream = File.Open(dataPath, FileMode.Open);


        SaveData save = new SaveData();
        try
        {
            save = (SaveData)binaryFormatter.Deserialize(stream);
        }
        catch
        {
            mc.ShowText("No ha sido posible leer el archivo, se creara uno nuevo al volver al menu de inicio");
        }


        for (int i = 0; i < save.clases.Count; i++)
        {

            for (int j = 0; j < save.clases[i].atributos.Count; j++)
            {
                ca.Load(save.clases[i].atributos[j].tipo, save.clases[i].atributos[j].proteccion, save.clases[i].atributos[j].nombre);
            }

            for (int j = 0; j < save.clases[i].metodos.Count; j++)
            {
                cm.Load(save.clases[i].metodos[j].nombre);
            }


            Material m = new Material(Shader.Find("Standard"));

            Color c = new Color(save.clases[i].color[0], save.clases[i].color[1], save.clases[i].color[2]);
            m.color = c;
            colors.Add(c.r + "," + c.g + "," + c.b, c);

            co.Load(save.clases[i].nombre, m);

        }



        for (int i = 0; i < save.variables.Count; i++)
        {
            Material c = null;
            bool found = false;
            for (int j = 0; j < mg.anchorablePrefs.Count && !found; j++)
            {
                if (save.variables[i].clase == mg.anchorablePrefs[j].nombre)
                {
                    found = true;
                    c = mg.anchorablePrefs[j].Material;
                }
            }
            Material m = new Material(Shader.Find("Standard"));
            Color cl = new Color(save.variables[i].color[0], save.variables[i].color[1], save.variables[i].color[2]);
            m.color = cl;
            colors.Add(cl.r + "," + cl.g + "," + cl.b, cl);
            mg.SpawnVariable(save.variables[i].clase, save.variables[i].nombre, m, c);
        }



        for (int i = 0; i < save.objetos.Count; i++)
        {
            mg.SpawnObject(save.objetos[i]);
        }

        stream.Close();



        if (stream != null)
        {
            stream.Close();
        }
    }

    public CustomMenu GetMenu(string nombre)
    {
        if (menus.ContainsKey(nombre))
        {
            return menus[nombre];
        }
        else
        {
            return null;
        }
    }

    public void PlaySound(int i){
        aud.clip = sounds[i];
        aud.Play();
    }
}
