using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // --- Is this item trashable --- //



    // --- Item Info UI --- //
    private GameObject itemInfoBoard;
    public Text nameText;
    public Text descriptionText;
    public Text functionalityText;
    public string name, description, functionality;

    // --- Consumption --- //

    private GameObject itemPendingConsumption;

    public bool isConsumable;

    public int healthEffect;
    public int caloriesEffect;
    public int hydrationEffect;

   

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        itemInfoBoard.SetActive(true);
        nameText.text = name;
        descriptionText.text = description;
        functionalityText.text = functionality;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        itemInfoBoard.SetActive(false);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if(pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                itemPendingConsumption = gameObject;
                consuming(healthEffect, caloriesEffect, hydrationEffect);
            }
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                DestroyImmediate(gameObject);
                InventorySystem.Instance.reCalculateList();
                CraftingSystem.Instance.refreshNeededMaterials();
            }
        }
    }

    void Start()
    {
        // Get References
        itemInfoBoard = InventorySystem.Instance.itemBoard;
        nameText = InventorySystem.Instance.itemName;
        descriptionText = InventorySystem.Instance.itemDescription;
        functionalityText = InventorySystem.Instance.itemFunctionality;
        //


    }

    private void consuming(int health, int calories, int hydration)
    {
        itemInfoBoard.SetActive(false);
        healthEffecCalculation(health);
        caloriesEffectCalculation(calories);
        hydrationEffectCalculation(hydration);
    }

    private void healthEffecCalculation(int health)
    {
        PlayerState.Instance.addHealth(health);
    }
    private void caloriesEffectCalculation(int calories)
    {
        PlayerState.Instance.addCalories(calories);

    }
    private void hydrationEffectCalculation(int hydration)
    {
        PlayerState.Instance.addHydrationPercent(hydration);
    }


}
