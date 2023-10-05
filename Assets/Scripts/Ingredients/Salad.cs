using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salad : Ingredient
{
    protected override string Name
    {
        get
        {
            return "Salad";
        }
    }
    protected override Sprite UISprite
    {
        get
        {
            return Resources.Load<Sprite>("Sprites/Salad");
        }
    }

    protected override int Points
    {
        get
        {
            return 20;
        }
    }
}
