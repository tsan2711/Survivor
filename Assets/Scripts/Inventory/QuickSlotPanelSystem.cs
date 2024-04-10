using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotPanelSystem : MonoBehaviour
{
    public static QuickSlotPanelSystem Instance { get; set; }

    public GameObject quickSlotPanel;

    private GameObject whatSlotToEquip;
    private GameObject weaponToEquip;

    private List<GameObject> quickSlotList = new List<GameObject>();
    private List<string> weaponList = new List<string>();

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
            quickSlotList.Add(slot.gameObject);
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
    public void AddItem(string itemName)
    {
        whatSlotToEquip = FindNextEmptySlot();
        weaponToEquip = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        weaponToEquip.transform.SetParent(whatSlotToEquip.transform);
        weaponList.Add(itemName);
    }
}
