using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string objectName;

    public bool playerInRange;

    public string getName()
    {
        return objectName;
    }

    private void OnDestroy()
    {
        if (playerInRange && !InventorySystem.Instance.checkIsInventoryFull())
        {
            if (!QuickSlotPanelSystem.Instance.checkIsFull())
            {
                QuickSlotPanelSystem.Instance.AddItemToQuickSlot(getName());
            } else
            {
                InventorySystem.Instance.AddItemToInventory(getName());
            }
            
        } else
        {
            // Write code to show text on User Interface to notice player that their inventory is full
            Debug.Log("Your inventory is full");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
