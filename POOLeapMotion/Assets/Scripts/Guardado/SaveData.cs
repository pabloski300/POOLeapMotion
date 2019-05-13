using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<ClaseSav> clases = new List<ClaseSav>();
    public List<VariableSav> variables = new List<VariableSav>();
    public List<int> objetos = new List<int>();
}
