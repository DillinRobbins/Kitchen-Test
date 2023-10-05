using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class Ingredient
{
    protected virtual string Name { get; }
    protected virtual Sprite UISprite { get; }
    protected virtual int Points { get; }

    public string GetName() => Name;

    public Sprite GetSprite() => UISprite;

    public int GetPoints() => Points;
}
