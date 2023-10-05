using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMeat : Ingredient
{
    protected override string Name
    {
        get
        {
            return "Raw Meat";
        }
    }
    protected override Sprite UISprite
    {
        get
        {
            return Resources.Load<Sprite>("Sprites/RawMeat");
        }
    }

    protected override int Points
    {
        get
        {
            return 10;
        }
    }
}
