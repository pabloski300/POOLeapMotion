using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtension
{
    public static bool Compare(this string s, CreadorObjetos c, bool modify){
        bool repeat = false;

        if (!repeat)
        {
            c.textoError.text = "Este nombre esta en uso por una variable";
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
            c.textoError.text = "Este nombre esta en uso por otra clase";
        }

        for (int i = 0; i < MenuGrid.Instance.anchorablePrefs.Count && !repeat; i++)
        {
            repeat = s.Equals(MenuGrid.Instance.anchorablePrefs[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if(modify && repeat){
            repeat = !(s == c.objectToModify.nombre);
        }

        return repeat;
    }

    public static bool Compare(this string s, CreadorVariable v, bool modify){
        bool repeat = false;

        if (!repeat)
        {
            v.textoError.text = "Este nombre esta en uso por otra variable";
        }

        for (int i = 0; i < CreadorObjetos.Instance.variablesInt.Count && !repeat; i++)
        {
            repeat = s.Equals(CreadorObjetos.Instance.variablesInt[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for (int i = 0; i < CreadorObjetos.Instance.variablesFloat.Count && !repeat; i++)
        {
            repeat = s.Equals(CreadorObjetos.Instance.variablesFloat[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }
        for (int i = 0; i < CreadorObjetos.Instance.variablesBoolean.Count && !repeat; i++)
        {
            repeat = s.Equals(CreadorObjetos.Instance.variablesBoolean[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if(modify && repeat && v.intVarToModify != null){
            repeat = !(s == v.intVarToModify.nombre);
        }

        if(modify && repeat && v.floatVarToModify != null){
            repeat = !(s == v.floatVarToModify.nombre);
        }

        if(modify && repeat && v.boolVarToModify != null){
            repeat = !(s == v.boolVarToModify.nombre);
        }

        if (!repeat)
        {
            v.textoError.text = "Este nombre esta en uso por la clase";
        }

        if (!repeat)
        {
            repeat = s.Equals(CreadorObjetos.Instance.nombreInput.text, StringComparison.InvariantCultureIgnoreCase);
        }
        
        return repeat;
    }

    public static bool Compare(this string s,MenuVariable v){
        bool repeat = false;

        if (!repeat)
        {
            v.textoError.text = "Este nombre esta en uso por una clase";
        }

        for (int i = 0; i < MenuGrid.Instance.anchorablePrefs.Count && !repeat; i++)
        {
            repeat = s.Equals(MenuGrid.Instance.anchorablePrefs[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        if (!repeat)
        {
            v.textoError.text = "Este nombre esta en uso por otra variable";
        }

        for (int i = 0; i < MenuGrid.Instance.variables.Count && !repeat; i++)
        {
            repeat = s.Equals(MenuGrid.Instance.variables[i].nombre, StringComparison.InvariantCultureIgnoreCase);
        }

        return repeat;
    }
}
