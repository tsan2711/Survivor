using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerContactInventory : MonoBehaviour
{
    private bool state;
    private void Awake()
    {
        state = true;
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            InventorySystem.Instance.isInventoryOpen = state;
            if (state)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;

            }
            state = !state;
        }
    }
}
