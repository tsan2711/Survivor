using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerAttack : MonoBehaviour
{
    public bool isPlayerAttack;
    public float attackSpeed;

    private void Start()
    {
        isPlayerAttack = true;
    }

    void Update()
    {
        controlPlayerAttack();
    }

    private void controlPlayerAttack()
    {
        if (isPlayerAttack)
        {
            if(Input.GetMouseButtonDown(0) && !InventorySystem.Instance.isInventoryOpen && EquipmentSystem.currentEquipment != "")
            {
                isPlayerAttack = false;
                if(Weapon.anim != null)
                {
                    Weapon.anim.SetTrigger("attack");
                }
                StartCoroutine("waitForNextAttack");
            }
        }
    }
    IEnumerator waitForNextAttack()
    {
        yield return new WaitForSeconds(attackSpeed);
        isPlayerAttack = true;
    }
}
