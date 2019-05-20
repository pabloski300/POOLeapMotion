using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtension
{
    static MenuGrid mg;
    static CreadorObjetos c;

    public static void Init()
    {
        mg = (MenuGrid)Manager.Instance.GetMenu("MenuGrid");
        c = (CreadorObjetos)Manager.Instance.GetMenu("CreadorObjetos");
    }
    public static bool Compare(this string s, CreadorObjetos c, bool modify)
    {
        bool repeat = false;

        if (!repeat)
        {
            if (Manager.Instance.english)
            {
                c.textoError.text = "This name belongs to an attribute";
            }
            else
            {
                c.textoError.text = "Este nombre esta en uso por un atributo";
            }
        }

        for (int i = 0; i < c.variablesInt.Count && !repeat; i++)
        {
            repeat = s.Equals(c.variablesInt[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for (int i = 0; i < c.variablesFloat.Count && !repeat; i++)
        {
            repeat = s.Equals(c.variablesFloat[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for (int i = 0; i < c.variablesBoolean.Count && !repeat; i++)
        {
            repeat = s.Equals(c.variablesBoolean[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if (!repeat)
        {
            if (Manager.Instance.english)
            {
                c.textoError.text = "This name belongs to another class";
            }
            else
            {
                c.textoError.text = "Este nombre esta en uso por otra clase";
            }
        }

        for (int i = 0; i < mg.anchorablePrefs.Count && !repeat; i++)
        {
            repeat = s.Equals(mg.anchorablePrefs[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if (modify && repeat)
        {
            repeat = !(s == c.objectToModify.nombre);
        }

        return repeat;
    }

    public static bool Compare(this string s, CreadorAtributos v, bool modify)
    {
        bool repeat = false;

        if (!repeat)
        {
            if (Manager.Instance.english)
            {
                v.textoError.text = "This name belongs to another attribute";
            }
            else
            {
                v.textoError.text = "Este nombre esta en uso por otra variable";
            }
        }

        for (int i = 0; i < c.variablesInt.Count && !repeat; i++)
        {
            repeat = s.Equals(c.variablesInt[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for (int i = 0; i < c.variablesFloat.Count && !repeat; i++)
        {
            repeat = s.Equals(c.variablesFloat[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for (int i = 0; i < c.variablesBoolean.Count && !repeat; i++)
        {
            repeat = s.Equals(c.variablesBoolean[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if (modify && repeat && v.intVarToModify != null)
        {
            repeat = !(s == v.intVarToModify.nombre);
        }

        if (modify && repeat && v.floatVarToModify != null)
        {
            repeat = !(s == v.floatVarToModify.nombre);
        }

        if (modify && repeat && v.boolVarToModify != null)
        {
            repeat = !(s == v.boolVarToModify.nombre);
        }

        if (!repeat)
        {
            v.textoError.text = "Este nombre esta en uso por la clase";
        }

        if (!repeat)
        {
            repeat = s.Equals(c.nombreInput.text, StringComparison.InvariantCultureIgnoreCase);
        }

        return repeat;
    }

    public static bool Compare(this string s, CreadorVariables v)
    {
        bool repeat = false;

        if (!repeat)
        {
            if (Manager.Instance.english)
            {
                v.textoError.text = "This name belongs to a class";
            }
            else
            {
                v.textoError.text = "Este nombre esta en uso por una clase";
            }
        }

        for (int i = 0; i < mg.anchorablePrefs.Count && !repeat; i++)
        {
            repeat = s.Equals(mg.anchorablePrefs[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if (!repeat)
        {
            if (Manager.Instance.english)
            {
                v.textoError.text = "This name belongs to another variable";
            }
            else
            {
                v.textoError.text = "Este nombre esta en uso por otra variable";
            }
        }

        for (int i = 0; i < mg.variables.Count && !repeat; i++)
        {
            repeat = s.Equals(mg.variables[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        return repeat;
    }
}
