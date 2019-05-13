using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Leap.Unity.Animation;
using TMPro;

public class PanelIzquierdo : CustomMenu
{
    public GameObject lineasVariablesParent;
    public LineaVariable[] lineasVariables;
    public GameObject lineasMetodosParent;
    public LineaMetodo[] lineasMetodos;

    public TextMeshPro[] lineasVariablesText;
    public TextMeshPro[] lineasMetodosText;

    CreadorObjetos c;

    public override void Init()
    {
        base.Init();

        c = (CreadorObjetos)Manager.Instance.GetMenu("CreadorObjetos");

        for (int i = 0; i < lineasVariables.Length; i++)
        {
            lineasVariables[i].indice = i;
            lineasVariables[i].Init();
        }

        for (int i = 0; i < lineasMetodos.Length; i++)
        {
            lineasMetodos[i].indice = i;
            lineasMetodos[i].Init();
        }

        for (int i = 0; i < lineasVariablesText.Length; i++)
        {
            lineasVariablesText[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < lineasMetodosText.Length; i++)
        {
            lineasMetodosText[i].gameObject.SetActive(false);
        }
    }

    public void AddVariable(string type)
    {
        bool finish = false;
        for (int i = 0; i < lineasVariables.Length && !finish; i++)
        {
            if (!lineasVariables[i].gameObject.activeSelf)
            {
                finish = true;

                switch (type)
                {
                    case "int":
                        AddInt(i);
                        break;
                    case "float":
                        AddFloat(i);
                        break;
                    case "bool":
                        AddBool(i);
                        break;
                }
                lineasVariables[i].gameObject.SetActive(true);
            }
        }
    }

    public void AddMetodo(string key)
    {
        bool finish = false;
        for (int i = 0; i < lineasMetodos.Length && !finish; i++)
        {
            if (!lineasMetodos[i].gameObject.activeSelf)
            {
                finish = true;
                lineasMetodos[i].metodo = c.metodos[key];
                lineasMetodos[i].nombre.text = key;
                lineasMetodos[i].gameObject.SetActive(true);
                lineasMetodosText[i].text = "public " + lineasMetodos[i].metodo.nombre;
            }
        }
    }

    public void AddVariable(int i, string type)
    {
        switch (type)
        {
            case "int":
                AddInt(i);
                break;
            case "float":
                AddFloat(i);
                break;
            case "bool":
                AddBool(i);
                break;
        }
    }

    public void AddMetodo(int i, string key)
    {
        lineasMetodos[i].metodo = c.metodos[key];
        lineasMetodos[i].nombre.text = "public " + lineasMetodos[i].metodo.nombre;
        lineasMetodos[i].gameObject.SetActive(true);
        lineasMetodosText[i].text = "public " + lineasMetodos[i].metodo.nombre;
    }

    public void AddInt(int index)
    {
        lineasVariables[index].type = "int";
        lineasVariables[index].intVariable = c.variablesInt[c.variablesInt.Count - 1];
        lineasVariables[index].floatVariable = null;
        lineasVariables[index].boolVariable = null;
        lineasVariables[index].nombre.text = lineasVariables[index].intVariable.proteccion.ToString().ToLower() + " int " + lineasVariables[index].intVariable.nombre;
        lineasVariablesText[index].text = lineasVariables[index].intVariable.proteccion.ToString().ToLower() + " int " + lineasVariables[index].intVariable.nombre;
    }

    public void AddFloat(int index)
    {
        lineasVariables[index].type = "float";
        lineasVariables[index].intVariable = null;
        lineasVariables[index].floatVariable = c.variablesFloat[c.variablesFloat.Count - 1];
        lineasVariables[index].boolVariable = null;
        lineasVariables[index].nombre.text = lineasVariables[index].floatVariable.proteccion.ToString().ToLower() + " float " + lineasVariables[index].floatVariable.nombre;
        lineasVariablesText[index].text = lineasVariables[index].floatVariable.proteccion.ToString().ToLower() + " float " + lineasVariables[index].floatVariable.nombre;
    }

    public void AddBool(int index)
    {
        lineasVariables[index].type = "bool";
        lineasVariables[index].intVariable = null;
        lineasVariables[index].floatVariable = null;
        lineasVariables[index].boolVariable = c.variablesBoolean[c.variablesBoolean.Count - 1];
        lineasVariables[index].nombre.text = lineasVariables[index].boolVariable.proteccion.ToString().ToLower() + " boolean " + lineasVariables[index].boolVariable.nombre;
        lineasVariablesText[index].text = lineasVariables[index].boolVariable.proteccion.ToString().ToLower() + " boolean " + lineasVariables[index].boolVariable.nombre;
    }

    public void ReOrderVariable(int j)
    {
        for (int i = j; i < lineasVariables.Length - 1; i++)
        {
            if (lineasVariables[i + 1].gameObject.activeSelf)
            {
                lineasVariables[i].type = lineasVariables[i + 1].type;
                lineasVariables[i].intVariable = lineasVariables[i + 1].intVariable;
                lineasVariables[i].floatVariable = lineasVariables[i + 1].floatVariable;
                lineasVariables[i].boolVariable = lineasVariables[i + 1].boolVariable;
                lineasVariables[i].nombre.text = lineasVariables[i + 1].nombre.text;
                lineasVariablesText[i].text = lineasVariables[i + 1].nombre.text;
            }
            else
            {
                lineasVariables[i].gameObject.SetActive(false);
                lineasVariablesText[i].text = "";
            }
        }

        lineasVariables[lineasVariables.Length - 1].gameObject.SetActive(false);
        lineasVariablesText[lineasVariables.Length - 1].text = "";
    }

    public void ReOrderMetodo(int j)
    {
        for (int i = j; i < lineasMetodos.Length - 1; i++)
        {
            if (lineasMetodos[i + 1].gameObject.activeSelf)
            {
                lineasMetodos[i].metodo = lineasMetodos[i + 1].metodo;
                lineasMetodos[i].nombre.text = lineasMetodos[i + 1].nombre.text;
                lineasMetodosText[i].text = lineasMetodos[i + 1].nombre.text;
            }
            else
            {
                lineasMetodos[i].gameObject.SetActive(false);
                lineasMetodosText[i].text = "";
            }
        }

        lineasMetodos[lineasMetodos.Length - 1].gameObject.SetActive(false);
        lineasMetodosText[lineasMetodos.Length - 1].text = "";
    }

    public void HideButtons()
    {
        foreach (CustomButton b in Buttons.Values)
        {
            b.gameObject.SetActive(false);
        }
        foreach (TextMeshPro t in lineasMetodosText)
        {
            t.gameObject.SetActive(true);
        }
        foreach (TextMeshPro t in lineasVariablesText)
        {
            t.gameObject.SetActive(true);
        }
        lineasMetodosParent.SetActive(false);
        lineasVariablesParent.gameObject.SetActive(false);

    }

    public void ShowButtons()
    {
        foreach (CustomButton b in Buttons.Values)
        {
            b.gameObject.SetActive(true);
        }
        foreach (TextMeshPro t in lineasMetodosText)
        {
            t.gameObject.SetActive(false);
        }
        foreach (TextMeshPro t in lineasVariablesText)
        {
            t.gameObject.SetActive(false);
        }
        lineasMetodosParent.SetActive(true);
        lineasVariablesParent.gameObject.SetActive(true);
        UnlockButtonsDelayed(0.3f);
    }

    public void Restart()
    {
        for (int i = 0; i < lineasVariables.Length; i++)
        {
            lineasVariables[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < lineasMetodos.Length; i++)
        {
            lineasMetodos[i].gameObject.SetActive(false);
        }

        for(int i = 0; i<lineasMetodosText.Length; i++){
            lineasMetodosText[i].text = "";
        }

        for(int i = 0; i<lineasVariablesText.Length; i++){
            lineasVariablesText[i].text = "";
        }
    }

    public void OpenNew()
    {
        Restart();
        Open();
    }
}
