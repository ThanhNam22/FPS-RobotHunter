using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static GameObject itemBeingDragged;
    private Vector3 startPosition;
    private Transform startParent;
    private Vector2 originalSize;
    private CanvasGroup canvasGroup;

    private static Dictionary<GameObject, Vector3> savedPositions = new Dictionary<GameObject, Vector3>();
    private static Dictionary<GameObject, Vector2> savedSizes = new Dictionary<GameObject, Vector2>();
    private static Dictionary<GameObject, Transform> savedParents = new Dictionary<GameObject, Transform>();

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup component is missing. Please ensure it is added.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        originalSize = GetComponent<RectTransform>().sizeDelta;
        canvasGroup.blocksRaycasts = false;

        if (!savedPositions.ContainsKey(gameObject))
        {
            savedPositions[gameObject] = startPosition;
            savedSizes[gameObject] = originalSize;
            savedParents[gameObject] = startParent;
        }

        // Tìm Canvas gốc và chuyển đối tượng kéo vào đó
        Canvas rootCanvas = transform.root.GetComponentInChildren<Canvas>();
        if (rootCanvas != null)
        {
            transform.SetParent(rootCanvas.transform);
        }
        else
        {
            Debug.LogWarning("No root Canvas found. Dragging might not work as expected.");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        canvasGroup.blocksRaycasts = true;

        // Nếu không được thả vào vị trí mới hợp lệ, phục hồi trạng thái ban đầu
        if (transform.parent == savedParents[gameObject] || transform.parent == transform.root)
        {
            transform.SetParent(savedParents[gameObject]);
            transform.position = savedPositions[gameObject];
            GetComponent<RectTransform>().sizeDelta = savedSizes[gameObject];
        }
    }
}
