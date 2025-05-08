using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotInventory : MonoBehaviour, IDropHandler
{
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragHandeler.itemBeingDragged == null) return;
        GameObject targetObject = eventData.pointerEnter;

        // Kiểm tra đối tượng có tag "InventoryParent" và không có con
        if ((targetObject != null && targetObject.layer == LayerMask.NameToLayer("InventoryParentLayer")) ||
            (targetObject.transform.parent != null && targetObject.transform.parent.gameObject.layer == LayerMask.NameToLayer("InventoryParentLayer")))
        {
            // Tìm đối tượng cha đầu tiên có tag "InventoryParent" chưa có con
            Transform targetParent = FindEmptyParentWithTag("InventoryParent");

            if (targetParent != null)
            {
                GameObject draggedItem = DragHandeler.itemBeingDragged;
                draggedItem.transform.SetParent(targetParent); // Đặt item làm con của đối tượng có tag "InventoryParent"

                // Cập nhật vị trí và kích thước của item khi vào inventory
                RectTransform draggedItemRect = draggedItem.GetComponent<RectTransform>();
                RectTransform targetParentRect = targetParent.GetComponent<RectTransform>();

                // Đặt item ở giữa slot
                draggedItemRect.anchorMin = new Vector2(0.5f, 0.5f);
                draggedItemRect.anchorMax = new Vector2(0.5f, 0.5f);
                draggedItemRect.pivot = new Vector2(0.5f, 0.5f);
                draggedItemRect.anchoredPosition = Vector2.zero;
                draggedItemRect.localScale = Vector3.one;

                // Cập nhật kích thước item theo kích thước của đối tượng cha
                draggedItemRect.sizeDelta = targetParentRect.sizeDelta;
            }
        }
            
    }

    // Tìm đối tượng cha đầu tiên có tag "InventoryParent" chưa có con
    private Transform FindEmptyParentWithTag(string tag)
    {
        Transform[] allTransforms = GameObject.FindObjectsOfType<Transform>();

        foreach (Transform t in allTransforms)
        {
            if (t.parent != null && t.parent.CompareTag(tag) && t.childCount == 0)
            {
                return t;
            }
        }
        return null;
    }

}
