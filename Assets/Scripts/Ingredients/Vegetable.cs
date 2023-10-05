using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : Ingredient
{
    protected override string Name
    {
        get
        {
            return "Lettuce";
        }
    }
    protected override Sprite UISprite
    {
        get
        {
            return Resources.Load<Sprite>("Sprites/Lettuce Head");
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
