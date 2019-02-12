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

        pos++;
        text += ch;
        return ch;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
