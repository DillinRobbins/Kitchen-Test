using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour, IInteractable
{
    private Color32 off = new(231, 36, 106, 255);
    private Color32 on = new(124, 238, 100, 255);

    [SerializeField] private SpriteRenderer ring;
    private PlayerInventory playerInventory;

    [SerializeField] private Transform orderLocation;
    [SerializeField] private GameObject customerPrefab;
    private Customer currentCustomer;

    private float timer = 5f;

    private void Awake()
    {
        SpawnNewCustomer();
    }

    private void OnEnable()
    {
        GameManager.OnEndGame += AddNegativePoints;
    }

    private void OnDisable()
    {
        GameManager.OnEndGame -= AddNegativePoints;
    }

    public void Interact(Ingredient ingredient)
    {
        currentCustomer.GiveIngredient(playerInventory, ingredient);
    }

    private void SpawnNewCustomer()
    {
        var customerObj = Instantiate(customerPrefab, orderLocation);
        currentCustomer = customerObj.GetComponent<Customer>();
        currentCustomer.OnOrderFulfilled += TriggerSpawnNewCustomer;
        currentCustomer.OnOrderFulfilled += AddPoints;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ring.color = on;
        if(collision.CompareTag("Player"))
            playerInventory ??= collision.GetComponent<PlayerInventory>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ring.color = off;
    }

    private void AddPoints(int score)
    {
        StatsManager.Instance.IncrementScore(score);
    }

    private void AddNegativePoints()
    {
        if(currentCustomer.GetScoreToAdd() < 0) StatsManager.Instance.IncrementScore(currentCustomer.GetScoreToAdd());
    }

    private void TriggerSpawnNewCustomer(int score)
    {
        currentCustomer.OnOrderFulfilled -= TriggerSpawnNewCustomer;
        currentCustomer.OnOrderFulfilled -= AddPoints;
        StartCoroutine(SpawnNewCustomerTimer());
    }

    private IEnumerator SpawnNewCustomerTimer()
    {
        yield return new WaitForSeconds(timer);
        SpawnNewCustomer();
    }
}
