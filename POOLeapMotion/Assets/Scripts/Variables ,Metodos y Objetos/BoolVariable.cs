using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolVariable : Variable<bool>
{
    public override string WriteFile()
    {
        return "    boolean "+ nombre +";\n";
    }
}
