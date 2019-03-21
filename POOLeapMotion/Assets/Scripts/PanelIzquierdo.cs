using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PanelIzquierdo : CustomMenu
{
    public LineaVariable[] lineasVariables;
    public LineaMetodo[] lineasMetodos;

    public static PanelIzquierdo Instance;

    public void Start()
    {
        for (int i = 0; i < lineasVariables.Length; i++)
        {
            lineasVariables[i].indice = i;
        }

        for (int i = 0; i < lineasMetodos.Length; i++)
        {
            lineasMetodos[i].indice = i;
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
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
                lineasMetodos[i].metodo = CreadorObjetos.Instance.metodos[key];
                lineasMetodos[i].nombre.text = "public " + lineasMetodos[i].metodo.nombre;
                lineasMetodos[i].gameObject.SetActive(true);
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
        lineasMetodos[i].metodo = CreadorObjetos.Instance.metodos[key];
        lineasMetodos[i].nombre.text = "public " + lineasMetodos[i].metodo.nombre;
        lineasMetodos[i].gameObject.SetActive(true);
    }

    public void AddInt(int index)
    {
        lineasVariables[index].type = "int";
        lineasVariables[index].intVariable = CreadorObjetos.Instance.variablesInt[CreadorObjetos.Instance.variablesInt.Count - 1];
        lineasVariables[index].floatVariable = null;
        lineasVariables[index].boolVariable = null;
        lineasVariables[index].nombre.text = "int " + lineasVariables[index].intVariable.nombre;

    }

    public void AddFloat(int index)
    {
        lineasVariables[index].type = "float";
        lineasVariables[index].intVariable = null;
        lineasVariables[index].floatVariable = CreadorObjetos.Instance.variablesFloat[CreadorObjetos.Instance.variablesFloat.Count - 1];
        lineasVariables[index].boolVariable = null;
        lineasVariables[index].nombre.text = "float " + lineasVariables[index].floatVariable.nombre;
    }

    public void AddBool(int index)
    {
        lineasVariables[index].type = "bool";
        lineasVariables[index].intVariable = null;
        lineasVariables[index].floatVariable = null;
        lineasVariables[index].boolVariable = CreadorObjetos.Instance.variablesBoolean[CreadorObjetos.Instance.variablesBoolean.Count - 1];
        lineasVariables[index].nombre.text = "boolean " + lineasVariables[index].boolVariable.nombre;
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
            }
            else
            {
                lineasVariables[i].gameObject.SetActive(false);
            }
        }

        lineasVariables[lineasVariables.Length - 1].gameObject.SetActive(false);
    }

    public void ReOrderMetodo(int j)
    {
        for (int i = j; i < lineasMetodos.Length - 1; i++)
        {
            if (lineasMetodos[i + 1].gameObject.activeSelf)
            {
                lineasMetodos[i].metodo = lineasMetodos[i + 1].metodo;
                lineasMetodos[i].nombre.text = lineasMetodos[i + 1].nombre.text;
            }
            else
            {
                lineasMetodos[i].gameObject.SetActive(false);
            }
        }

        lineasMetodos[lineasMetodos.Length - 1].gameObject.SetActive(false);
    }

    public void HideButtons()
    {
        foreach (CustomButton b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    public void ShowButtons()
    {
        foreach (CustomButton b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        for (int i = 0; i < lineasVariables.Length; i++)
        {
            lineasVariables[i].gameObject.SetActive(false);
        }
    }

    public void OpenNew()
    {
        Restart();
        Open();
    }
}
