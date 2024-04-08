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

    // Alert
    public GameObject alertBoard;
    public Text pickPopText;
    public Image pickPopImage;

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
        if(itemName == "Mushroom")
        {
            itemToAdd.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (itemName == "Stone" || itemName == "Stick" || itemName == "Sword" || itemName == "Axe" || itemName == "Bow")
        {
            itemToAdd.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

        }
        itemList.Add(itemName);
        controlAlertBoard(itemName, itemToAdd.GetComponent<Image>().sprite);

        // Appearing an Alert


    }

    private void controlAlertBoard(string itemName, Sprite itemToAdd)
    {
        alertBoard.SetActive(true);
        //pickPopImage.gameObject.SetActive(true);
        //pickPopText.gameObject.SetActive(true);
        pickPopText.text = "Pick " + itemName;
        pickPopImage.sprite = itemToAdd;
        StartCoroutine(onOffAlertBoard());
    }

    IEnumerator onOffAlertBoard()
    {
        yield return new WaitForSeconds(2);
        alertBoard.SetActive(false);
        //pickPopImage.gameObject.SetActive(false);
        //pickPopText.gameObject.SetActive(false);
    }


    public bool checkIsInventoryFull()
    {
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                return false;
            }
        }
        return true;
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

    public void deleteMaterialFromInventory(string materialName, int materialAmount)
    {
        int counterMaterial = materialAmount;
        Debug.Log(counterMaterial);
        for (int i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == (materialName + "(Clone)") && counterMaterial != 0)
                {
                    Debug.Log(slotList[i].transform.GetChild(0).name);
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counterMaterial--;
                }
            }
        }
    }

    public void reCalculateList()
    {
        itemList.Clear();

        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name.Replace("(Clone)", "");
                itemList.Add(name);
            }
        }
    }
}
