using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }
    public Canvas referenceInventoryCanvas;

    public GameObject InventoryScreenUI;
    public bool isInventoryOpen;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;

    private void Awake()
    {
        isInventoryOpen = false;
        InventoryScreenUI.SetActive(isInventoryOpen);
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

    private void Start()
    {
        PopulateSlot();
    }

    private void Update()
    {
        if(isInventoryOpen)
            InventoryScreenUI.SetActive(isInventoryOpen);
        else
            InventoryScreenUI.SetActive(isInventoryOpen);
    }


    private void PopulateSlot()
    {
        foreach(Transform child in InventoryScreenUI.transform)
        {
            if (child.CompareTag("InventorySlot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    public void AddItemToInventory(string itemName)
    {
        whatSlotToEquip = findNextEmptySlot();
        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
        itemToAdd.transform.localScale = new Vector3(1, 1, 1);
        itemList.Add(itemName);
    }


    public bool checkIsInventoryFull()
    {
        bool check = true;
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                check = false;
            }
        }

        return check;
    }

    private GameObject findNextEmptySlot()
    {
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }
        }

        return new GameObject();
    }
}
