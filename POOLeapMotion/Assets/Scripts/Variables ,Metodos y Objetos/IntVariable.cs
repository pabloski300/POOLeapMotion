using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntVariable : Variable<int>
{
    public override string WriteFile()
    {
        return "    int "+ nombre +";\n";
    }
}
