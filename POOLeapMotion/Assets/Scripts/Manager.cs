using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class Manager : MonoBehaviour
{
    public static Manager Instance;

    Dictionary<string, Color> colors = new Dictionary<string, Color>();

    public List<string> classNames = new List<string>();
    public List<string> classVariableNames = new List<string>();

    public Dictionary<string, MetodoBase> metodosPrefab = new Dictionary<string, MetodoBase>();
    public IntVariable intVariablePrefab;
    public FloatVariable floatVariablePrefab;
    public BoolVariable boolVariablePrefab;
    public ObjetoBase objetoBasePrefab;
    public VariableObjeto variableObjetoPrefab;

    [RuntimeInitializeOnLoadMethod]
    // Start is called before the first frame update
    void Awake()
    {
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
        Debug.Log(metodos.Count);
        foreach (GameObject g in metodos)
        {
            MetodoBase m = Instantiate(g, new Vector3(800, 800, 800), Quaternion.identity).GetComponent<MetodoBase>();
            metodosPrefab.Add(m.nombre, m);
        }
        bundle.Unload(false);

        Instance = this;

    }

    // Update is called once per frame
    void Update()
    {

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
        } while (colors.ContainsKey((r+g+b).ToString()));

        colors.Add((r+g+b).ToString(), color);

        color.r = r;
        color.g = g;
        color.b = b;
        color.a = 1.0f;

        return color;
    }

    public Color GenerateColor(string colorRGB)
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
        } while (colors.ContainsKey((r+g+b).ToString()));

        colors.Remove(colorRGB);
        colors.Add((r+g+b).ToString(), color);

        color.r = r;
        color.g = g;
        color.b = b;
        color.a = 1.0f;

        return color;
    }
}
