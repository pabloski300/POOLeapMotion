using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Leap.Unity.Examples;
using System.IO;

public class MenuGrid : MonoBehaviour
{

    [HideInInspector]
    public static MenuGrid Instance;

    public List<ObjetoBase> anchorablePrefs;

    public GameObject gridObjectParent;

    public List<CustomAnchor> gridObjeto;

    public GameObject gridVariableParent;

    public List<CustomAnchor> gridVariable;

    List<ObjetoBase> objects;

    [HideInInspector]
    public List<VariableObjeto> variables;

    CustomAnchor centralAnchor;

    Transform objectsPivot;

    int activeObjectAnchors;
    int activeVariableAnchors;

    AssetBundle bundle;

    // Use this for initialization
    void Start()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        objectsPivot = GameObject.FindGameObjectWithTag("ObjectsPivot").transform;
        objects = new List<ObjetoBase>();
        gridObjeto = gridObjectParent.GetComponentsInChildren<CustomAnchor>().ToList();
        gridVariable = gridVariableParent.GetComponentsInChildren<CustomAnchor>().ToList();
        Debug.Log(gridVariable.Count);
        centralAnchor = GameObject.FindGameObjectWithTag("CentralAnchor").GetComponent<CustomAnchor>();
        gridObjeto.Sort();

        for (int i = 0; i < gridObjeto.Count; i++)
        {
            gridObjeto[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < gridVariable.Count; i++)
        {
            gridVariable[i].gameObject.SetActive(false);
        }
        activeObjectAnchors = 0;
        activeVariableAnchors = 0;

        bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "variable_objeto"));
    }

    public void ReturnToAnchor(CustomAnchorable emmisor)
    {
        if (emmisor != null)
        {
            emmisor.Anchorable.anchor = null;
        }
        foreach (CustomAnchorable target in objects)
        {
            if (target != emmisor)
            {
                if (target.Anchorable.anchor == target.CentralAnchor)
                {
                    target.ReturnToStart();
                }
            }
        }
    }

    public void ReturnToAnchor(ObjetoBase emmisor, Anchor final)
    {
        if (emmisor != null)
        {
            emmisor.Anchorable.anchor = final;
        }
        foreach (CustomAnchorable target in objects)
        {
            if (target != emmisor)
            {
                if (target.Anchorable.anchor == target.CentralAnchor)
                {
                    target.ReturnToStart();
                }
            }
        }
    }

    public void SpawnObject(int i)
    {
        if (activeObjectAnchors < gridObjeto.Count)
        {
            CustomAnchor anchor = gridObjeto.FirstOrDefault(x => !x.gameObject.activeSelf);
            GameObject newObject = Instantiate(anchorablePrefs[i], anchor.gameObject.transform.position, anchor.gameObject.transform.rotation, objectsPivot).gameObject;
            ObjetoBase newAnchorable = newObject.GetComponent<ObjetoBase>();
            anchor.gameObject.SetActive(true);
            anchor.objectAnchored = newAnchorable;
            newAnchorable.Init();
            newAnchorable.Anchorable.anchor = anchor;
            newAnchorable.GridAnchor = anchor;
            newAnchorable.CentralAnchor = centralAnchor;
            objects.Add(newAnchorable);
            activeObjectAnchors++;
            newAnchorable.ReturnToStart();
            Consola.Instance.Write("new " + newAnchorable.nombre + "();");

        }
        else
        {
            Debug.Log("Maximos objetos creados");
        }
    }

    public void SpawnVariable(string clase, string nombre)
    {
        if (activeVariableAnchors < gridVariable.Count)
        {
            CustomAnchor anchor = gridVariable.FirstOrDefault(x => !x.gameObject.activeSelf);
            GameObject objeto = Instantiate(bundle.LoadAsset<GameObject>("VariableObjeto"), anchor.transform.position, anchor.transform.rotation);
            VariableObjeto variable = objeto.GetComponent<VariableObjeto>();
            anchor.gameObject.SetActive(true);
            anchor.objectAnchored = variable;
            variable.Init();
            variable.Anchorable.anchor = anchor;
            variable.GridAnchor = anchor;
            variable.CentralAnchor = centralAnchor;
            variables.Add(variable);
            activeVariableAnchors++;
            variable.ReturnToStart();
            variable.Init(nombre,clase);
            Consola.Instance.Write(clase + " " + nombre + ";");
        }
    }

    public void RemoveLastObject()
    {
        if (activeObjectAnchors > 0)
        {
            CustomAnchor anchor = gridObjeto.LastOrDefault(x => x.gameObject.activeSelf);
            ObjetoBase oldObject = anchor.objectAnchored as ObjetoBase;
            objects.Remove(oldObject);
            anchor.NotifyDetached(oldObject.Anchorable);
            Destroy(oldObject.gameObject);
            anchor.gameObject.SetActive(false);
            activeObjectAnchors--;
            ReturnToAnchor(null);
            Debug.Log(activeObjectAnchors);
            MenuClases.Instance.NumberObjetos--;
        }
        else
        {
            Debug.Log("No existen objetos creados");
        }
    }

    public void RemoveOneObject(CustomAnchorable c)
    {
        if (activeObjectAnchors > 0)
        {
            CustomAnchor anchor = gridObjeto.FirstOrDefault(x => x.objectAnchored == c);
            ObjetoBase oldObject = anchor.objectAnchored as ObjetoBase;
            objects.Remove(oldObject);
            anchor.NotifyDetached(oldObject.Anchorable);
            Destroy(oldObject.gameObject);
            anchor.gameObject.SetActive(false);
            activeObjectAnchors--;
            ReturnToAnchor(null);
            Debug.Log(activeObjectAnchors);
            MenuClases.Instance.NumberObjetos--;
        }
        else
        {
            Debug.Log("No existen objetos creados");
        }
    }

    public void RemoveAllObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            CustomAnchor anchor = gridObjeto.FirstOrDefault(x => x.objectAnchored == objects[i]);
            ObjetoBase oldObject = anchor.objectAnchored as ObjetoBase;
            objects.Remove(oldObject);
            anchor.NotifyDetached(oldObject.Anchorable);
            Destroy(oldObject.gameObject);
            anchor.gameObject.SetActive(false);
            activeObjectAnchors--;
            ReturnToAnchor(null);
            i--;
            MenuClases.Instance.NumberObjetos--;
        }
    }

    public void RemoveOfTypeObject(ObjetoBase o)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            CustomAnchor anchor = gridObjeto.FirstOrDefault(x => x.objectAnchored == objects[i]);
            ObjetoBase oldObject = anchor.objectAnchored as ObjetoBase;
            if (oldObject.nombre == o.nombre)
            {
                objects.Remove(oldObject);
                anchor.NotifyDetached(oldObject.Anchorable);
                Destroy(oldObject.gameObject);
                anchor.gameObject.SetActive(false);
                activeObjectAnchors--;
                ReturnToAnchor(null);
                i--;
                MenuClases.Instance.NumberObjetos--;
            }
        }
    }

        public void RemoveLastVariable()
    {
        if (activeVariableAnchors > 0)
        {
            CustomAnchor anchor = gridObjeto.LastOrDefault(x => x.gameObject.activeSelf);
            VariableObjeto oldObject = anchor.objectAnchored as VariableObjeto;
            variables.Remove(oldObject);
            anchor.NotifyDetached(oldObject.Anchorable);
            Destroy(oldObject.gameObject);
            anchor.gameObject.SetActive(false);
            activeVariableAnchors--;
            ReturnToAnchor(null);
            Debug.Log(activeVariableAnchors);
            MenuClases.Instance.NumberVariables--;
        }
        else
        {
            Debug.Log("No existen objetos creados");
        }
    }

    public void RemoveOneVariable(CustomAnchorable c)
    {
        if (activeObjectAnchors > 0)
        {
            CustomAnchor anchor = gridObjeto.FirstOrDefault(x => x.objectAnchored == c);
            VariableObjeto oldObject = anchor.objectAnchored as VariableObjeto;
            variables.Remove(oldObject);
            anchor.NotifyDetached(oldObject.Anchorable);
            Destroy(oldObject.gameObject);
            anchor.gameObject.SetActive(false);
            activeVariableAnchors--;
            ReturnToAnchor(null);
            Debug.Log(activeObjectAnchors);
            MenuClases.Instance.NumberVariables--;
        }
        else
        {
            Debug.Log("No existen objetos creados");
        }
    }

    public void RemoveAllVariables()
    {
        for (int i = 0; i < variables.Count; i++)
        {
            CustomAnchor anchor = gridObjeto.FirstOrDefault(x => x.objectAnchored == variables[i]);
            VariableObjeto oldObject = anchor.objectAnchored as VariableObjeto;
            variables.Remove(oldObject);
            anchor.NotifyDetached(oldObject.Anchorable);
            Destroy(oldObject.gameObject);
            anchor.gameObject.SetActive(false);
            activeVariableAnchors--;
            ReturnToAnchor(null);
            i--;
            MenuClases.Instance.NumberVariables--;
        }
    }

    public void RemoveOfTypeVariable(ObjetoBase o)
    {
        for (int i = 0; i < variables.Count; i++)
        {
            CustomAnchor anchor = gridVariable.FirstOrDefault(x => x.objectAnchored == variables[i]);
            VariableObjeto oldObject = anchor.objectAnchored as VariableObjeto;
            if (oldObject.clase == o.nombre)
            {
                variables.Remove(oldObject);
                anchor.NotifyDetached(oldObject.Anchorable);
                Destroy(oldObject.gameObject);
                anchor.gameObject.SetActive(false);
                activeVariableAnchors--;
                //ReturnToAnchor(null);
                i--;
                MenuClases.Instance.NumberVariables--;
            }
        }
    }

}
