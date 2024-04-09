using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;
    // Health field
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    // Calories field
    [SerializeField]
    private int maxCalories = 200;
    private int currentCalories;

    public GameObject PlayerBody;
    private Vector3 lastPositionOfPlayer;
    private float distance;
    [SerializeField]
    private int RANGE_OF_CALORIES = 40;

    // Hydration field
    [SerializeField]
    private int maxHydrationPercent = 100;
    private int currentHydrationPercent = 100;
    [SerializeField]
    private bool isUnderWater = false;

    void Start()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        setInitialValue();
    }

    private void setInitialValue()
    {
        HealthBar.Instance.setMaxValue(maxHealth);
        CaloriesBar.Instance.setMaxValue(maxCalories);
        HydrationBar.Instance.setMaxValue(maxHydrationPercent);

        HealthBar.Instance.set(ref currentHealth, maxHealth);
        CaloriesBar.Instance.set(ref currentCalories, maxCalories);
        HydrationBar.Instance.set(ref currentHydrationPercent, maxHydrationPercent);
    }

    void Update()
    {
        distance += Vector3.Distance(PlayerBody.transform.position, lastPositionOfPlayer);
        lastPositionOfPlayer = PlayerBody.transform.position;

        if (distance >= RANGE_OF_CALORIES)
        {
            CaloriesBar.Instance.decrease(ref currentCalories, 2);
            distance = 0;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            HealthBar.Instance.decrease(ref currentHealth, 10);
        }
    }

    IEnumerator BeingUnderWater()
    {
        while (isUnderWater)
        {
            yield return new WaitForSeconds(3);
            HydrationBar.Instance.decrease(ref currentHydrationPercent, 10);
        }
    }

    public int getHealth()
    {
        return currentHealth;
    }
    public int getCalories()
    {
        return currentCalories;
    }
    public int getHydrationPercent()
    {
        return currentHydrationPercent;
    }

    public void addHealth(int health)
    {
        HealthBar.Instance.increase(ref currentHealth, health);
    }
    public void addCalories(int calories)
    {
        CaloriesBar.Instance.increase(ref currentCalories, calories);

    }
    public void addHydrationPercent(int hydration)
    {
        HydrationBar.Instance.increase(ref currentHydrationPercent, hydration);

    }

}
