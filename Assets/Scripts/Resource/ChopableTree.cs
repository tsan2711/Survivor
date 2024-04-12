using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ChopableTree : MonoBehaviour
{
    public bool playerInRange;
    public bool canBeChopped;

    public float treeMaxHealth = 5;
    public float treeCurrentHealth;

    private Animator anim;

    private void Awake()
    {
        treeCurrentHealth = treeMaxHealth;
        anim = transform.parent.transform.parent.GetComponent<Animator>();
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

    public void GetHit(int damage)
    {
        if(treeCurrentHealth - damage > 0)
        {
            treeCurrentHealth -= damage;
            anim.SetTrigger("shake");
        } else
        {
            treeCurrentHealth -= damage;
            TreeIsDead();
        }
    }

    private void TreeIsDead()
    {
        Destroy(transform.parent.transform.parent.gameObject);
        Vector3 treePosition = transform.position;

        canBeChopped = false;
        //SelectionManager.Instance.selectedTree = null;
        SelectionManager.Instance.chopHolder.gameObject.SetActive(false);
        GameObject brokenTree = Instantiate(Resources.Load<GameObject>("Chopped_Tree"),
            treePosition, Quaternion.Euler(0, 0, 0));


    }

    private void Update()
    {
        if (canBeChopped)
        {
            GlobalState.Instance.resourceHealth = treeCurrentHealth;
            GlobalState.Instance.resourceMaxHealth = treeMaxHealth;
        }
    }
}
