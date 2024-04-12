using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }
    public bool onTarget;
    public GameObject handIcon;
    public GameObject circleIcon;



    public GameObject interaction_info_UI;
    public Text interaction_text;
    private bool isPlayerInRange;


    public GameObject selectedTree;
    public GameObject chopHolder;
    // Start is called before the first frame update

    void Start()
    {
        //onTarger = false;
        if(Instance != null && Instance != this)
        {
            Destroy(Instance);
        } else
        {
            Instance = this;
        }

        isPlayerInRange = false;
        interaction_text = interaction_info_UI.GetComponent<Text>();
        handIcon.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();


            ChopableTree chopableTree = selectionTransform.gameObject.GetComponent<ChopableTree>();
            if(chopableTree && chopableTree.playerInRange)
            {
                chopableTree.canBeChopped = true;
                selectedTree = chopableTree.gameObject;
                chopHolder.SetActive(true);
            } else
            {
                if(selectedTree != null)
                {
                    selectedTree.gameObject.GetComponent<ChopableTree>().canBeChopped = false;
                    selectedTree = null;
                }
                chopHolder.gameObject.SetActive(false);
            }


            if (interactable)
            {
                isPlayerInRange = interactable.playerInRange;
            }
            if (!InventorySystem.Instance.isInventoryOpen)
            {
                if (interactable && isPlayerInRange)
                {
                    interaction_text.text = interactable.getName();
                    interaction_info_UI.SetActive(true);
                    //onTarget = true;
                    if (interactable.gameObject.CompareTag("PickUpItem"))
                    {
                        handIcon.SetActive(true);
                        circleIcon.SetActive(false);

                    } else
                    {
                        handIcon.SetActive(false);
                        circleIcon.SetActive(true);
                    }

                }
                else
                {
                    interaction_info_UI.SetActive(false);
                    circleIcon.SetActive(true);
                    handIcon.SetActive(false);
                    isPlayerInRange = false;
                    //onTarget = false;
                }
            }
            else
            {
                circleIcon.SetActive(false);
                handIcon.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange && selectionTransform.gameObject.CompareTag("PickUpItem"))
            {
                Debug.Log("Destroy GameObject");
                Destroy(selectionTransform.gameObject);
                selectionTransform = null;
            }
        }
        else
        {
            isPlayerInRange = false;
            interaction_info_UI.SetActive(false);
            //onTarget = false;

        }
    }
}
