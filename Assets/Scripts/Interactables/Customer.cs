using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.Events;
using UnityEditor.SceneManagement;
using System;

public class Customer : MonoBehaviour
{
    private float ingredientCount;
    private List<Ingredient> orderList = new();
    private List<GameObject> orderDisplayGameObjectList = new();
    private TextMeshProUGUI orderTimer;

    private GameObject orderImagePrefab;

    private float timer;
    private float scoreToAdd;
    public UnityAction<int> OnOrderFulfilled;

    private void Awake()
    {
        ingredientCount = RollIngredientCount();
        

        for(int i = 0; i < ingredientCount; i++)
        {
            var ingredient = ReturnRandomIngredient();
            orderList.Add(ingredient);
            scoreToAdd += ingredient.GetPoints();
        }
    }

    private void Start()
    {
        orderImagePrefab = Resources.Load<GameObject>("Prefab/Order Image");

        foreach (var ingredient in orderList)
        {
            var orderObj = Instantiate(orderImagePrefab, transform);
            orderObj.GetComponent<Image>().sprite = ingredient.GetSprite();
            orderDisplayGameObjectList.Add(orderObj);
        }

        orderTimer = GetComponentInChildren<TextMeshProUGUI>();

        timer = scoreToAdd;
        orderTimer.text = scoreToAdd.ToString();
    }

    private void FixedUpdate()
    {
        scoreToAdd -= Time.deltaTime;

        orderTimer.text = Mathf.FloorToInt(scoreToAdd).ToString();
    }

    private int RollIngredientCount()
    {
        int roll = UnityEngine.Random.Range(1, 101);

        if (roll <= 50) return 2;
        return 3;
    }

    private Ingredient ReturnRandomIngredient()
    {
        int roll = UnityEngine.Random.Range(0, 3);
        if (roll == 0) return new Cheese();
        else if(roll == 1) return new Salad();
        else return new CookedMeat();
    }

    public Ingredient IngredientMatchesOrder(Ingredient ingredient)
    {
        if (ingredient == null) return null;

        foreach(var order in orderList)
        {
            if (order.GetType() == ingredient.GetType()) return order;
        }
        return null;
    }

    public void GiveIngredient(PlayerInventory playerInventory, Ingredient ingredient)
    {
        var orderToRemove = IngredientMatchesOrder(ingredient);

        if (orderToRemove != null)
        {
            playerInventory.RemoveIngredient();
            var orderDisplayToRemove = orderDisplayGameObjectList.Find(x => x.GetComponent<Image>().sprite == ingredient.GetSprite());
            orderDisplayGameObjectList.Remove(orderDisplayToRemove);
            Destroy(orderDisplayToRemove);

            orderList.Remove(orderToRemove);
        }
        else
            Debug.Log("You don't have the right ingredient");

        if (orderList.Count == 0) CompleteCustomerOrder();
    }

    public int GetScoreToAdd()
    {
        return Mathf.FloorToInt(scoreToAdd);
    }

    private void CompleteCustomerOrder()
    {
        OnOrderFulfilled.Invoke(Mathf.FloorToInt(scoreToAdd));
        Destroy(gameObject);
    }
}
