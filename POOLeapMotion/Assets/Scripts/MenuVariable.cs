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

    public MeshRenderer representacionClase;
    public MeshRenderer representacionVariable;

    // Start is called before the first frame update
    new void Awake()
    {
        base.Awake();
        nombreInput.inputValidator = InputValidationAlphaOnly.CreateInstance<InputValidationAlphaOnly>();

        nombreInput.gameObject.SetActive(false);

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

    public void OpenNew(string clase, Material colorClase)
    {
        base.Open();
        this.clase = clase;
        Restart();
        TrimString();
        nombreInput.gameObject.SetActive(true);  

        representacionClase.material = colorClase;
        
        GenerateRandomColor();
    }

    void GenerateRandomColor(){
        bool repeatColor;
        float r;
        float g;
        float b;

        do{
            repeatColor = false;
            System.Random random = new System.Random();
            
            r = UnityEngine.Random.Range(0f,1f);
            g = UnityEngine.Random.Range(0f,1f);
            b = UnityEngine.Random.Range(0f,1f);

            for(int i=0; i<MenuGrid.Instance.anchorablePrefs.Count && !repeatColor; i++){
                repeatColor = MenuGrid.Instance.anchorablePrefs[i].Material.color.r == r && 
                MenuGrid.Instance.anchorablePrefs[i].Material.color.g == g &&
                MenuGrid.Instance.anchorablePrefs[i].Material.color.b == b;
            }

            for(int i=0; i<MenuGrid.Instance.variables.Count && !repeatColor; i++){
                repeatColor = MenuGrid.Instance.variables[i].ColorVariable.color.r == r && 
                MenuGrid.Instance.variables[i].ColorVariable.color.g == g &&
                MenuGrid.Instance.variables[i].ColorVariable.color.b == b;
            }

        } while(repeatColor);

        Material colorVariable = new Material(Shader.Find("Standard"));
        colorVariable.color = new Color(r,g,b);

        representacionVariable.material = colorVariable;
    }

    public new void Close()
    {
        base.Close();
        nombreInput.DeactivateInputField();
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
        MenuGrid.Instance.SpawnVariable(clase, nombreInput.text, representacionVariable.material, representacionClase.material);
        MenuClases.Instance.NumberVariables ++;
        if (MenuClases.Instance.NumberVariables == MenuGrid.Instance.gridVariable.Count)
        {
            Close();
            MenuClases.Instance.Open();
        }
        TrimString();
        nombreInput.Select();
        nombreInput.text = "";
        GenerateRandomColor();
        
    }
}
