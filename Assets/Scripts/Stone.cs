using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stone
{
    public int index;
    public bool isOkey =false;

    public Stone(int i,bool b = false)
    {
        index = i;
        isOkey = b;
    }

    public Stone Clone()
    {
        return (Stone)this.MemberwiseClone();
    }
}
