using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MenuVariable : CustomMenu
{
    public TMP_InputField nombreInput;
    public TextMeshPro cabecera;
    public TextMeshPro textoError;
    [HideInInspector]
    public string clase;

    public static MenuVariable Instance;

    public Material representacionClase;
    public Material representacionVariable;
    Material materialClase;

    Color finalColor;
    Color FinalColor
    {
        set
        {
            finalColor = value;
            representacionVariable.color = finalColor;
        }
    }

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();

        nombreInput.gameObject.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        GetButton("Crear").OnPress += (()=>Crear());
    }

    public void Restart()
    {
        nombreInput.text = "";
    }

    public void OpenNew(string clase, Material colorClase)
    {
        base.Open();
        this.clase = clase;
        Restart();
        TrimString();
        nombreInput.gameObject.SetActive(true);

        representacionClase.color = colorClase.color;
        materialClase = colorClase;

        GenerateColor();
    }

    public new void Close()
    {
        base.Close();
        nombreInput.DeactivateInputField();
        nombreInput.gameObject.SetActive(false);
    }

    public void TrimString()
    {

        textoError.gameObject.SetActive(false);
        GetButton("Crear").gameObject.SetActive(true);
        nombreInput.text = nombreInput.text.Trim();
        cabecera.text = clase + " " + nombreInput.text;
        if(nombreInput.text.Length == 0){
            GetButton("Crear").gameObject.SetActive(false);
        }

        if (nombreInput.text.Length > nombreInput.characterLimit)
        {
            nombreInput.text = nombreInput.text.Remove(nombreInput.characterLimit, 1);
        }

        bool repeat = nombreInput.text.Compare(this);

        if (repeat)
        {
            GetButton("Crear").gameObject.SetActive(false);
            textoError.gameObject.SetActive(true);
        }
    }

    public void Crear()
    {
        Material colorVariable = new Material(Shader.Find("Standard"));
        colorVariable.color = finalColor;
        MenuGrid.Instance.SpawnVariable(clase, nombreInput.text, colorVariable, materialClase);
        MenuClases.Instance.NumberVariables++;
        if (MenuClases.Instance.NumberVariables == MenuGrid.Instance.gridVariable.Count)
        {
            Close();
            MenuClases.Instance.Open();
        }
        TrimString();
        nombreInput.Select();
        nombreInput.text = "";
        GenerateColor();
    }

    public void GenerateColor()
    {
        FinalColor = Manager.Instance.GenerateColor();
    }

    public void RegenerateColor()
    {
        FinalColor = Manager.Instance.GenerateColor((finalColor.r+finalColor.g+finalColor.b).ToString());
    }
}
