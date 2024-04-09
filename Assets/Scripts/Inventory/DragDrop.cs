using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.root);
        itemBeingDragged = gameObject;
    }


    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (transform.parent == startParent || transform.parent == transform.root)
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("InventoryBin"))
        {
            itemDelete = itemBeingDragged;
            deleteItemBoard.SetActive(true);
        }
        itemBeingDragged = null;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
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
