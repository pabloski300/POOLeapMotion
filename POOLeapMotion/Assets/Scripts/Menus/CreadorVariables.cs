using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CreadorVariables : CustomMenu
{
    public TMP_InputField nombreInput;
    public TextMeshPro cabecera;
    public TextMeshPro textoError;
    public TextMeshPro info;

    [HideInInspector]
    public string clase;
    bool maxVars;

    Material matClase;
    Material matVariable;

    Color SetColor
    {
        set
        {
            matVariable = new Material(Shader.Find("Standard"));
            matVariable.color = value;
        }
    }

    MenuGrid mg;
    MenuClases mc;

    // Start is called before the first frame update
    public override void Init()
    {
        base.Init();

        mg = (MenuGrid)Manager.Instance.GetMenu("MenuGrid");
        mc = (MenuClases)Manager.Instance.GetMenu("MenuClases");

        nombreInput.gameObject.SetActive(false);

        GetButton("Crear").OnPress += (() => Crear());
    }

    public void Restart()
    {
        nombreInput.text = "";
    }

    public void OpenNew(string clase, Material matClase)
    {
        base.Open();
        this.clase = clase;
        Restart();
        TrimString();
        nombreInput.gameObject.SetActive(true);

        this.matClase = matClase;

        GenerateColor();

        if (mc.NumberVariables == mg.gridVariable.Count)
        {
            maxVars = true;
        }
        else
        {
            maxVars = false;
        }
    }

    public new void Close()
    {
        base.Close();
        nombreInput.DeactivateInputField();
        nombreInput.gameObject.SetActive(false);
    }

    public override void Callback()
    {
        TrimString();
    }

    public void TrimString()
    {
        textoError.gameObject.SetActive(false);
        nombreInput.text = nombreInput.text.Trim();
        cabecera.text = clase + " " + nombreInput.text;

        if (nombreInput.text.Length > nombreInput.characterLimit)
        {
            nombreInput.text = nombreInput.text.Remove(nombreInput.characterLimit, 1);
        }

        if (nombreInput.text.Length == 0)
        {
            GetButton("Crear").Blocked = true;
            return;
        }

        bool repeat = nombreInput.text.Compare(this);

        if (repeat)
        {
            GetButton("Crear").Blocked = true;
            textoError.gameObject.SetActive(true);
            return;
        }

        if (!maxVars)
        {
            GetButton("Crear").Blocked = false;
        }
    }

    public void Crear()
    {
        mg.SpawnVariable(clase, nombreInput.text, matVariable, matClase);
        mc.NumberVariables++;
        if (mc.NumberVariables == mg.gridVariable.Count)
        {
            maxVars = true;
            info.gameObject.SetActive(false);
            info.text = "Maximas Variables Creadas";
            info.gameObject.SetActive(true);
            nombreInput.text = "";
            TrimString();
            return;
        }
        info.gameObject.SetActive(false);
        info.text = "Variable Creada";
        info.gameObject.SetActive(true);
        nombreInput.Select();
        nombreInput.text = "";
        TrimString();
        GenerateColor();
    }

    public void GenerateColor()
    {
        SetColor = Manager.Instance.GenerateColor();
    }
}
