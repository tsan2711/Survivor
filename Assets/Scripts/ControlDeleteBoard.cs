using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlDeleteBoard : MonoBehaviour
{
    public Button tickBtn;
    public Button crossBtn;

    void Start()
    {
        tickBtn.onClick.AddListener(delegate { pressTickBtn(); });
    }

    private void pressTickBtn()
    {
        if (DragDrop.itemDelete)
        {
            DestroyImmediate(DragDrop.itemDelete);
            InventorySystem.Instance.reCalculateList();
            CraftingSystem.Instance.refreshNeededMaterials();
            gameObject.SetActive(false);
        }
    }
}
