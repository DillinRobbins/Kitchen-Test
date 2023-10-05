using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private SpriteRenderer carriedIngredientSpriteRenderer;
    
    private Ingredient ingredient;

    private void Awake()
    {
        //SetIngredient(new Salad());
    }

    public bool HasIngredient()
    {
        if(ingredient == null) return false;
        return true;
    }

    public Ingredient CheckIngredient()
    {
        return ingredient;
    }

    public void SetIngredient(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        carriedIngredientSpriteRenderer.sprite = ingredient.GetSprite();
    }

    public void GiveCheese()
    {
        SetIngredient(new Cheese());
    }

    public void GiveRawMeat()
    {
        SetIngredient(new RawMeat());
    }

    public void GiveCookedMeat()
    {
        SetIngredient(new CookedMeat());
    }

    public void GiveLettuce()
    {
        SetIngredient(new Vegetable());
    }

    public void GiveSalad()
    {
        SetIngredient(new Salad());
    }

    public Ingredient RemoveIngredient()
    {
        if(ingredient != null)
        {
            var copy = ingredient;
            ingredient = null;
            carriedIngredientSpriteRenderer.sprite = null;
            return copy;
        }

        Debug.Log("You aren't carrying an ingredient");
        return null;
    }
}
