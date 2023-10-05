using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Stove : MonoBehaviour, IInteractable
{
    private Color32 off = new(231, 36, 106, 255);
    private Color32 on = new(124, 238, 100, 255);

    [SerializeField] private SpriteRenderer ring;

    private PlayerInventory playerInventory;
    [SerializeField] private SpriteRenderer ingredientSpriteRenderer;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Slider progressBarSlider;

    bool foodPrepared = false;

    private float timer = 6f;
    private float maxTime = 6f;

    public void Interact(Ingredient ingredient)
    {
        if(foodPrepared)
        {
            playerInventory.GiveCookedMeat();
            ingredientSpriteRenderer.sprite = null;
            foodPrepared = false;
        }
        else if (ingredient is RawMeat)
        {
            ingredientSpriteRenderer.sprite = ingredient.GetSprite();
            playerInventory.RemoveIngredient();
            StartCoroutine(PrepareFood());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ring.color = on;

        if (collision.CompareTag("Player"))
            playerInventory ??= collision.GetComponent<PlayerInventory>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ring.color = off;
    }

    private IEnumerator PrepareFood()
    {
        progressBar.SetActive(true);

        while(timer > 0)
        {
            timer -= Time.deltaTime;
            progressBarSlider.value = (maxTime - timer) / maxTime;
            yield return null;
        }
        progressBar.SetActive(false);
        progressBarSlider.value = 0;
        timer = maxTime;

        foodPrepared = true;
        ingredientSpriteRenderer.sprite = Resources.Load<Sprite>("Sprites/CookedMeat");
    }
}
