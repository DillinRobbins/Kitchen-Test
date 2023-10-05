using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : Ingredient
{
    protected override string Name
    { 
        get 
        { 
           return "Cheese"; 
        } 
    }
    protected override Sprite UISprite
    { 
        get 
        { 
            return Resources.Load<Sprite>("Sprites/Cheese"); 
        } 
    }

    protected override int Points
    { 
        get 
        { 
            return 10; 
        } 
    }

    protected Ingredient GetIngredientType()
    {
        return this;
    }
}
