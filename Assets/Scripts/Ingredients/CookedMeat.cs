using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedMeat : Ingredient
{
    protected override string Name
    {
        get
        {
            return "Cooked Meat";
        }
    }
    protected override Sprite UISprite
    {
        get
        {
            return Resources.Load<Sprite>("Sprites/CookedMeat");
        }
    }

    protected override int Points
    {
        get
        {
            return 30;
        }
    }
}
