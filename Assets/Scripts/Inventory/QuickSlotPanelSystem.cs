using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotPanelSystem : MonoBehaviour
{
    const float IMAGEINVENTORY_SCALE = 0.7f;

    public static QuickSlotPanelSystem Instance { get; set; }

    public GameObject quickSlotPanel;

    private GameObject whatSlotToEquip;
    private GameObject weaponToEquip;

    public List<GameObject> quickSlotList = new List<GameObject>();
    public List<string> weaponList = new List<string>();

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Instance = null;
            return;
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PopulateSlot();
    }

    private void PopulateSlot()
    {
        foreach(Transform slot in quickSlotPanel.transform)
        {
            if (slot.gameObject.CompareTag("QuickSlot"))
            {
                quickSlotList.Add(slot.gameObject);
            }
        }
    }

    public bool checkIsFull()
    {
        foreach(GameObject slot in quickSlotList)
        {
            if(slot.transform.childCount == 0)
            {
                return false;
            }
        }
        return true;
    }

    public GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in quickSlotList)
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }
    public void AddItemToQuickSlot(string itemName)
    {
        whatSlotToEquip = FindNextEmptySlot();
        weaponToEquip = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        if (itemName == "Mushroom")
        {
            weaponToEquip.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (itemName == "Stone" || itemName == "Stick" || itemName == "Sword" || itemName == "Axe" || itemName == "Bow")
        {
            weaponToEquip.transform.localScale = new Vector3(IMAGEINVENTORY_SCALE, IMAGEINVENTORY_SCALE, IMAGEINVENTORY_SCALE);
        }
        weaponToEquip.transform.SetParent(whatSlotToEquip.transform);
        string cleanName = itemName.Replace("(Clone)", "");

        // add item to temp quickslot.
        weaponList.Add(cleanName);
        // Add item to main inventory
        InventorySystem.Instance.controlAlertBoard(itemName, weaponToEquip.GetComponent<Image>().sprite);
        InventorySystem.Instance.itemList.Add(cleanName);
        CraftingSystem.Instance.refreshNeededMaterials();
    }

}
