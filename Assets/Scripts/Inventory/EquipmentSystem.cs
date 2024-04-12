using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSystem : MonoBehaviour
{
    //Singleton
    public static EquipmentSystem Instance;

    // --- Quick slot panel --- //
    public GameObject quickSlotPanel;
    public GameObject numberSlots;
    public static bool isSelected;
    public bool confirm;
    private Transform indexOfSlot;
    public int index;
    public static int equipIndex;
    private GameObject whatSlotToAdd;
    public List<GameObject> equipmentSlotList = new List<GameObject>();

    // Equip weapon
    public GameObject toolHolder;
    private GameObject itemSelected;
    private bool equipable;
    public static string currentEquipment;


    private void Start()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        isSelected = false;
        PopulateSlot();
        DeactiveNumberSlot();
    }
    private void Update()
    {
        ControlSlotInput();
        ControlEquipItem();
    }

    private void PopulateSlot()
    {
        foreach (Transform slot in quickSlotPanel.transform)
        {
            if (slot.CompareTag("QuickSlot"))
            {
                equipmentSlotList.Add(slot.gameObject);
            }
        }
    }

    private void DeactiveNumberSlot()
    {
        foreach(Transform number in numberSlots.transform)
        {
            number.GetChild(0).GetComponent<Text>().color = Color.grey;
        }
    }

    private void ControlSlotInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DeactiveNumberSlot();
            if (!checkCurrentSlotEmpty(0))
            {
                if(index == -1)
                {
                    index = 1;
                    isSelected = false;
                    indexOfSlot = null;
                    return;
                }
               index = 1;
                indexOfSlot = numberSlots.transform.GetChild(index - 1);
                if (index == 1)
                {
                    isSelected = true;
                    equipIndex = 0;
                    confirm = true;
                    indexOfSlot.GetChild(0).GetComponent<Text>().color = Color.white;
                    index = -1;
                }
                // Control equip Item
                ControlEquipItem();
                //

            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DeactiveNumberSlot();
            if (confirm)
            {
                isSelected = false;
                confirm = false;
            }
            if (!checkCurrentSlotEmpty(1))
            {
                if (index == -2)
                {
                    index = 2;

                    indexOfSlot = null;
                    isSelected = false;
                    return;
                }
                index = 2;
                indexOfSlot = numberSlots.transform.GetChild(index - 1);
                if (index == 2)
                {
                    equipIndex = 1;
                    isSelected = true;
                    confirm = true;
                    indexOfSlot.GetChild(0).GetComponent<Text>().color = Color.white;
                    index = -2;
                    // Control equip Item
                    ControlEquipItem();
                    //

                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DeactiveNumberSlot();
            if (confirm)
            {
                isSelected = false;
                confirm = false;
            }
            if (!checkCurrentSlotEmpty(2))
            {
                if (index == -3)
                {
                    index = 3;
                    isSelected = false;
                    indexOfSlot = null;
                    return;
                }
                index = 3;
                indexOfSlot = numberSlots.transform.GetChild(index - 1);
                if (index == 3)
                {
                    equipIndex = 2;
                    isSelected = true;
                    confirm = true;
                    indexOfSlot.GetChild(0).GetComponent<Text>().color = Color.white;
                    index = -3;
                }
                // Control equip Item
                ControlEquipItem();
                //

            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DeactiveNumberSlot();
            if (confirm)
            {
                isSelected = false;
                confirm = false;
            }
            if (!checkCurrentSlotEmpty(3))
            {
                if (index == -4)
                {
                    index = 4;
                    isSelected = false;
                    indexOfSlot = null;
                    return;
                }
                index = 4;
                indexOfSlot = numberSlots.transform.GetChild(index - 1);
                if (index == 4)
                {
                    equipIndex = 3;
                    isSelected = true;
                    confirm = true;
                    indexOfSlot.GetChild(0).GetComponent<Text>().color = Color.white;
                    index = -4;
                }
                // Control equip Item
                ControlEquipItem();
                //

            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            DeactiveNumberSlot();
            if (confirm)
            {
                isSelected = false;
                confirm = false;
            }
            if (!checkCurrentSlotEmpty(4))
            {
                indexOfSlot = numberSlots.transform.GetChild(4);
                if (index == -5)
                {
                    index = 5;
                    indexOfSlot = null;
                    isSelected = false;
                    return;
                }
                index = 5;
                indexOfSlot = numberSlots.transform.GetChild(index - 1);
                if (index == 5)
                {
                    equipIndex = 4;
                    isSelected = true;
                    confirm = true;
                    indexOfSlot.GetChild(0).GetComponent<Text>().color = Color.white;
                    index = -5;
                }
                // Control equip Item
                ControlEquipItem();
                //

            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            DeactiveNumberSlot();
            if (confirm)
            {
                isSelected = false;
                confirm = false;
            }
            if (!checkCurrentSlotEmpty(5))
            {
                indexOfSlot = numberSlots.transform.GetChild(5);
                if (index == -6)
                {
                    index = 6;
                    isSelected = false;
                    indexOfSlot = null;
                    return;
                }
                index = 6;
                indexOfSlot = numberSlots.transform.GetChild(index - 1);
                if (index == 6)
                {
                    equipIndex = 5;
                    isSelected = true;
                    confirm = true;
                    indexOfSlot.GetChild(0).GetComponent<Text>().color = Color.white;
                    index = -6;
                }
                // Control equip Item
                ControlEquipItem();
                //

            }
        }
    }

    public void ControlEquipItem()
    {
        int amountChildToolHolder = toolHolder.transform.childCount;
        if (isSelected && amountChildToolHolder == 0 )
        {
            Debug.Log(equipIndex);
            Transform indexOfSlot = quickSlotPanel.transform.GetChild(equipIndex);
            equipable = indexOfSlot.GetChild(0).GetComponent<InventoryItem>().isEquipable;
            if (equipable){
                string itemName = indexOfSlot.GetChild(0).name;
                currentEquipment = itemName.Replace("(Clone)", "_Model");
                itemSelected = Instantiate(Resources.Load<GameObject>(currentEquipment), new Vector3(), new Quaternion());
                itemSelected.transform.SetParent(toolHolder.transform);
                itemSelected.transform.localPosition = new Vector3(1, 0.4f, 2.75f);
                itemSelected.transform.localRotation = Quaternion.Euler(85f, 190f, 100f);
            } else
            {
                currentEquipment = "";
            }
        }
        if (!isSelected && amountChildToolHolder > 0)
        {
            currentEquipment = "";
            DestroyImmediate(toolHolder.transform.GetChild(0).gameObject);
        }
    }

    public GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in equipmentSlotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }
    public bool CheckIsFull()
    {
        foreach (GameObject slot in equipmentSlotList)
        {
            if (slot.transform.childCount == 0)
            {
                return false;
            }
        }
        return true;
    }

    public void AddItemToQuickSlot(GameObject equipment)
    {
        whatSlotToAdd = FindNextEmptySlot();
        equipment.transform.SetParent(whatSlotToAdd.transform);
    }

    public bool checkCurrentSlotEmpty(int index)
    {
        if(quickSlotPanel.transform.GetChild(index).childCount > 0)
        {
            return false;
        }
        return true;
    }
}
