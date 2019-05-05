using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetodoRead : MetodoBase
{
    public override void Execute(List<TMP_InputField> inputs, TextMeshPro output)
    {
        StartCoroutine(Ejecutar(inputs, output));
    }

    public IEnumerator Ejecutar(List<TMP_InputField> inputs, TextMeshPro output){
        inputs[0].gameObject.SetActive(true);
        while(!inputsReady){
            yield return null;
        }
        output.text = inputs[0].text;
        inputs[0].gameObject.SetActive(false);
        ExploracionMetodo.Instance.GetButton("Ejecutar").gameObject.SetActive(true);
        inputsReady = false;
    }

    public override string WriteFile()
    {
        string s = " \n";
        s += "    public string Read(){\n";
        s += "        Scanner s = new Scanner(System.in);\n";
        s += "        string x = s.next();\n";
        s += "        return s;\n";
        s += "    }\n";

        return s;
    }
}
