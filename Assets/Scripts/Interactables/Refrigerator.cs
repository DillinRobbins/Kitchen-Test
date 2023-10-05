using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour, IInteractable
{
    private Color32 off = new(231, 36, 106, 255);
    private Color32 on = new(124, 238, 100, 255);

    [SerializeField] private SpriteRenderer ring;
    [SerializeField] private GameObject ingredientButtons;

    PlayerInventory playerInventory;

    public void Interact(Ingredient ingredient)
    {
        if(ingredientButtons.activeSelf) ingredientButtons.SetActive(false);
        else ingredientButtons.SetActive(true);
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

        if(ingredientButtons.activeSelf) ingredientButtons.SetActive(false);
    }
}
