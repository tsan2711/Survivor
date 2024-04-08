using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{

    public GameObject craftingScreenUI;
    public GameObject toolScreenUI;
    private Button toolBtn;

    private bool isToolScreenActive;


    // Weapons 
    private Text SwordMaterialRequire1;
    private Text SwordMaterialRequire2;
    private Button SwordCraftBtn;

    private Text AxeMaterialRequire1;
    private Text AxeMaterialRequire2;
    private Button AxeCraftBtn;

    private Text BowMaterialRequire1;
    private Text BowMaterialRequire2;
    private Text BowMaterialRequire3;
    private Button BowCraftBtn;

    //Blue print
    private Blueprint swordBP = new Blueprint("Sword", 2, "Stone", 3, "Stick", 3);
    private Blueprint axeBP = new Blueprint("Axe", 2, "Stone", 4, "Stick", 2);
    private Blueprint bowBP = new Blueprint("Bow", 3, "Stone", 2, "Stick", 2, "String", 2);

    // Start is called before the first frame update
    void Start()
    {
        toolBtn = craftingScreenUI.transform.Find("Tool_btn").GetComponent<Button>();
        isToolScreenActive = false;
        toolBtn.onClick.AddListener(delegate { openToolCategory(); });
        // Sword requires Materials
        SwordMaterialRequire1 = toolScreenUI.transform.Find("Items").transform.Find("Sword").transform.Find("Materials1").GetComponent<Text>();
        SwordMaterialRequire2 = toolScreenUI.transform.Find("Items").transform.Find("Sword").transform.Find("Materials2").GetComponent<Text>();
        SwordCraftBtn = toolScreenUI.transform.Find("Items").transform.Find("Sword").transform.Find("Craft_btn").GetComponent<Button>();
        // Axe requires Materials
        AxeMaterialRequire1 = toolScreenUI.transform.Find("Items").transform.Find("Axe").transform.Find("Materials1").GetComponent<Text>();
        AxeMaterialRequire2 = toolScreenUI.transform.Find("Items").transform.Find("Axe").transform.Find("Materials2").GetComponent<Text>();
        AxeCraftBtn = toolScreenUI.transform.Find("Items").transform.Find("Axe").transform.Find("Craft_btn").GetComponent<Button>();
        // Bow requires Materials
        BowMaterialRequire1 = toolScreenUI.transform.Find("Items").transform.Find("Bow").transform.Find("Materials1").GetComponent<Text>();
        BowMaterialRequire2 = toolScreenUI.transform.Find("Items").transform.Find("Bow").transform.Find("Materials2").GetComponent<Text>();
        BowMaterialRequire3 = toolScreenUI.transform.Find("Items").transform.Find("Bow").transform.Find("Materials1").GetComponent<Text>();
        BowCraftBtn = toolScreenUI.transform.Find("Items").transform.Find("Bow").transform.Find("Craft_btn").GetComponent<Button>();

        SwordCraftBtn.onClick.AddListener(delegate { craftAnyItem(swordBP); });
        AxeCraftBtn.onClick.AddListener(delegate { craftAnyItem(axeBP); });
        BowCraftBtn.onClick.AddListener(delegate { craftAnyItem(bowBP); });

    }

    // Update is called once per frame
    void Update()
    {
        refreshNeededMaterials();

        if (InventorySystem.Instance.isInventoryOpen)
        {
            screenActive().SetActive(true);
        } else
        {
            toolScreenUI.SetActive(false);
            craftingScreenUI.SetActive(false);
        }
    }

    private void openToolCategory()
    {
        craftingScreenUI.SetActive(false);
        toolScreenUI.SetActive(true);
    }

    public GameObject screenActive()
    {

        if (isToolScreenActive)
        {
            return toolScreenUI;
        }

        return craftingScreenUI;
    }




    public void craftAnyItem(Blueprint blueprintToCraft)
    {

        // Add item to Inventory
        InventorySystem.Instance.AddItemToInventory(blueprintToCraft.itemName);

        // Delete Item from Inventory after craftng
        if(blueprintToCraft.numOfMaterial == 1)
        {
            InventorySystem.Instance.deleteMaterialFromInventory(blueprintToCraft.material1,blueprintToCraft.materialAmount1);
        }
        else if (blueprintToCraft.numOfMaterial == 2)
        {
            Debug.Log("kaka");
            InventorySystem.Instance.deleteMaterialFromInventory(blueprintToCraft.material1, blueprintToCraft.materialAmount1);
            InventorySystem.Instance.deleteMaterialFromInventory(blueprintToCraft.material2, blueprintToCraft.materialAmount2);
        }
        else if(blueprintToCraft.numOfMaterial == 3)
        {
            InventorySystem.Instance.deleteMaterialFromInventory(blueprintToCraft.material1, blueprintToCraft.materialAmount1);
            InventorySystem.Instance.deleteMaterialFromInventory(blueprintToCraft.material2, blueprintToCraft.materialAmount2);
            InventorySystem.Instance.deleteMaterialFromInventory(blueprintToCraft.material3, blueprintToCraft.materialAmount3);
        }

        // After crafting, recalculate list item ( string )
        InventorySystem.Instance.reCalculateList();
        refreshNeededMaterials();
    }



    private void refreshNeededMaterials()
    {
        int stoneCount = 0;
        int stickCount = 0;
        int stringCount = 0;
        var itemInventoryList = InventorySystem.Instance.itemList;
        foreach(string itemName in itemInventoryList)
        {
            switch (itemName)
            {
                case "Stone":
                    stoneCount++;
                    break;
                case "Stick":
                    stickCount++;
                    break;
            }
        }
        // Sword materials needed
        SwordMaterialRequire1.text = "3 Stones [" + stoneCount + "]";
        SwordMaterialRequire2.text = "3 Sticks [" + stickCount + "]";
        if(stoneCount >= 3 && stickCount >= 3)
        {
            SwordCraftBtn.gameObject.SetActive(true);
        } else
        {
            SwordCraftBtn.gameObject.SetActive(false);
        }

        // Axe materials needed
        AxeMaterialRequire1.text = "4 Stones [" + stoneCount + "]";
        AxeMaterialRequire2.text = "2 Sticks [" + stickCount + "]";
        if (stoneCount >= 4 && stickCount >= 2)
        {
            AxeCraftBtn.gameObject.SetActive(true);
        }
        else
        {
            AxeCraftBtn.gameObject.SetActive(false);
        }
        // Bow materials needed
        BowMaterialRequire1.text = "2 Stones [" + stoneCount + "]";
        BowMaterialRequire2.text = "2 Sticks [" + stickCount + "]";
        BowMaterialRequire3.text = "2 Strings [" + stringCount + "]";
        if (stoneCount >= 2 && stickCount >= 2 && stringCount >= 2)
        {
            BowCraftBtn.gameObject.SetActive(true);
        }
        else
        {
            BowCraftBtn.gameObject.SetActive(false);
        }
    }   
}
