using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputValidationAlphaOnly : TMP_InputValidator
{
    public override char Validate(ref string text, ref int pos, char ch)
    {
        if(!char.IsLetter(ch)){
            return char.MinValue;
        }

        text = text.Remove(pos,text.Length-pos);
        text += ch;
        pos++;
        return ch;
    }
}
