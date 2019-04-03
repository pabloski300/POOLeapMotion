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

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        nombreInput.inputValidator = InputValidationAlphaOnly.CreateInstance<InputValidationAlphaOnly>();

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
        nombreInput.text = "";
    }

    public void OpenNew(string clase)
    {
        base.Open();
        this.clase = clase;
        Restart();
        TrimString();
        nombreInput.gameObject.SetActive(true);
        nombreInput.Select();
    }

    public new void Close()
    {
        base.Close();
        nombreInput.gameObject.SetActive(false);
    }

    public void TrimString()
    {
        bool repeat = false;
        textoError.gameObject.SetActive(false);
        buttons[0].gameObject.SetActive(true);
        nombreInput.text = nombreInput.text.Trim();
        cabecera.text = clase +" "+ nombreInput.text;

        if (nombreInput.text.Length > nombreInput.characterLimit)
        {
            nombreInput.text = nombreInput.text.Remove(nombreInput.characterLimit, 1);
        }

        if (!repeat)
        {
            textoError.text = "Este nombre esta en uso por una clase";
        }

        for (int i = 0; i < MenuGrid.Instance.anchorablePrefs.Count && !repeat; i++)
        {
            repeat = nombreInput.text.Equals(MenuGrid.Instance.anchorablePrefs[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if (!repeat)
        {
            textoError.text = "Este nombre esta en uso por otra variable";
        }

        for (int i = 0; i < MenuGrid.Instance.variables.Count && !repeat; i++)
        {
            repeat = nombreInput.text.Equals(MenuGrid.Instance.variables[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if (repeat)
        {
            buttons[0].gameObject.SetActive(false);
            textoError.gameObject.SetActive(true);
        }
    }

    public void Crear()
    {
        MenuGrid.Instance.SpawnVariable(clase, nombreInput.text);
        MenuClases.Instance.NumberVariables ++;
        if (MenuClases.Instance.NumberVariables == MenuGrid.Instance.gridVariable.Count)
        {
            Close();
            MenuClases.Instance.Open();
        }
        TrimString();
        nombreInput.Select();
        nombreInput.text = "";
        
    }
}
