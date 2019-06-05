using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Leap.Unity.Examples;
using System.IO;
using TMPro;

public class MenuGrid : CustomMenu
{
    public TextMeshPro info;

    [HideInInspector]
    public List<ObjetoBase> anchorablePrefs;

    public GameObject gridObjectParent;
    [HideInInspector]
    public List<CustomAnchor> gridObjeto;

    public GameObject gridVariableParent;
    [HideInInspector]
    public List<CustomAnchor> gridVariable;

    [HideInInspector]
    public List<ObjetoBase> objects;

    [HideInInspector]
    public List<VariableObjeto> variables;

    CustomAnchor centralAnchor;

    Transform objectsPivot;
    Transform variablesPivot;

    int activeObjectAnchors;
    int activeVariableAnchors;

    MenuClases m;
    Consola c;

    // Use this for initialization
    public override void Init()
    {
        base.Init();

        m = (MenuClases)Manager.Instance.GetMenu("MenuClases");
        c = (Consola)Manager.Instance.GetMenu("Consola");

        objectsPivot = GameObject.FindGameObjectWithTag("ObjectsPivot").transform;
        variablesPivot = GameObject.FindGameObjectWithTag("VariablesPivot").transform;
        objects = new List<ObjetoBase>();
        gridObjeto = gridObjectParent.GetComponentsInChildren<CustomAnchor>().ToList();
        gridVariable = gridVariableParent.GetComponentsInChildren<CustomAnchor>().ToList();

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
            newAnchorable.Init(anchor);
            objects.Add(newAnchorable);
            activeObjectAnchors++;
            newAnchorable.GraspEnd();
            c.Write("new " + "<#" + ColorUtility.ToHtmlStringRGB(newAnchorable.Material.color) + ">" + newAnchorable.nombre + "</color>();");
        }
        else
        {
            Debug.Log("Maximos objetos creados");
        }
    }

    public void SpawnVariable(string clase, string nombre, Material colorVariable, Material colorClase)
    {
        if (activeVariableAnchors < gridVariable.Count)
        {
            CustomAnchor anchor = gridVariable.FirstOrDefault(x => !x.gameObject.activeSelf);
            VariableObjeto variable = Instantiate(Manager.Instance.variableObjetoPrefab, anchor.transform.position, anchor.transform.rotation, variablesPivot);
            anchor.gameObject.SetActive(true);
            anchor.objectAnchored = variable;
            variables.Add(variable);
            activeVariableAnchors++;
            variable.ColorClase = colorClase;
            variable.ColorVariable = colorVariable;
            variable.Init(nombre, clase, anchor, colorVariable, colorClase);
            c.Write("<#" + ColorUtility.ToHtmlStringRGB(colorClase.color) + ">" + clase + "</color>" + " " + "<#" + ColorUtility.ToHtmlStringRGB(colorVariable.color) + ">" + nombre + "</color>" + ";");
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
            Debug.Log(activeObjectAnchors);
            m.NumberObjetos--;
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
            i--;
            m.NumberObjetos--;
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
                i--;
                m.NumberObjetos--;
            }
        }
        RemoveOfTypeVariable(o);
    }

    public void RemoveOneVariable(CustomAnchorable c)
    {
        if (activeVariableAnchors > 0)
        {
            CustomAnchor anchor = gridVariable.FirstOrDefault(x => x.objectAnchored == c);
            VariableObjeto oldObject = anchor.objectAnchored as VariableObjeto;
            variables.Remove(oldObject);
            anchor.NotifyDetached(oldObject.Anchorable);
            Destroy(oldObject.gameObject);
            anchor.gameObject.SetActive(false);
            activeVariableAnchors--;
            Debug.Log(activeObjectAnchors);
            m.NumberVariables--;
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
            CustomAnchor anchor = gridVariable.FirstOrDefault(x => x.objectAnchored == variables[i]);
            VariableObjeto oldObject = anchor.objectAnchored as VariableObjeto;
            variables.Remove(oldObject);
            anchor.NotifyDetached(oldObject.Anchorable);
            Destroy(oldObject.gameObject);
            anchor.gameObject.SetActive(false);
            activeVariableAnchors--;
            i--;
            m.NumberVariables--;
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
                i--;
                m.NumberVariables--;
            }
        }
    }

    public void RemoveClases(){
        while(anchorablePrefs.Count > 0){
            ObjetoBase b = anchorablePrefs[0];
            anchorablePrefs.Remove(b);
            Destroy(b.gameObject);
            m.NumberClases--;
        }
    }

    public void ShowText(string text)
    {
        info.gameObject.SetActive(false);
        info.text = text;
        info.gameObject.SetActive(true);
    }

}
