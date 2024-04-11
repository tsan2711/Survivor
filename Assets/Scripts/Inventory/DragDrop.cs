using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Canvas
    [SerializeField]
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    // Delete Item Alert Board 
    public GameObject deleteItemBoard;
    public static GameObject itemDelete;
    //
    public static GameObject itemBeingDragged;

    Vector3 startPosition;
    Transform startParent;


    public static bool isDragging;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(!EquipmentSystem.isSelected);
        Debug.Log(gameObject.transform.parent);
        Debug.Log(EquipmentSystem.Instance.equipmentSlotList[EquipmentSystem.equipIndex]);
        if (!EquipmentSystem.isSelected && gameObject.transform.parent != EquipmentSystem.Instance.equipmentSlotList[EquipmentSystem.equipIndex])
        {
           
            isDragging = true;
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            startPosition = transform.position;
            startParent = transform.parent;
            transform.SetParent(transform.root);
            itemBeingDragged = gameObject;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;
            if (transform.parent == startParent || transform.parent == transform.root)
            {
                transform.position = startPosition;
                transform.SetParent(startParent);
            }
            if (eventData.pointerCurrentRaycast.gameObject)
            {
                if (eventData.pointerCurrentRaycast.gameObject.CompareTag("InventoryBin"))
                {
                    itemDelete = itemBeingDragged;
                    deleteItemBoard.SetActive(true);
                }
            }
            itemBeingDragged = null;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = InventorySystem.Instance.referenceInventoryCanvas;
        deleteItemBoard = InventorySystem.Instance.deleteItemBoard;
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
