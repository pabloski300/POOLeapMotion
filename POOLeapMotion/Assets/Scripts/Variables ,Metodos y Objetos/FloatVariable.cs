using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatVariable : Variable<float>
{
    public override string WriteFile()
    {
        return "    float "+ nombre +";\n";
    }
}
