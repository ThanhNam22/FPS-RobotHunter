using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    public GameObject scrollViewContent;
    public GameObject itemPrefab;
    private static List<GameObject> existingScrollItems = new List<GameObject>();

    public void OnButtonClick(Image targetImage)
    {
        Button sourceButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        if (sourceButton != null && sourceButton.image != null)
        {
            // Kiểm tra xem targetImage có Image con chưa (không bao gồm chính nó)
            Image childImage = null;
            foreach (Transform child in targetImage.transform)
            {
                childImage = child.GetComponent<Image>();
                if (childImage != null)
                {
                    break;
                }
            }

            if (childImage == null)
            {
                // Nếu chưa có, tạo một Image con mới
                GameObject newChild = new GameObject("ChildImage");
                newChild.transform.SetParent(targetImage.transform, false);
                newChild.transform.localPosition = Vector3.zero;
                childImage = newChild.AddComponent<Image>();

                // Đảm bảo RectTransform của childImage vừa khít với cha
                RectTransform rectTransform = newChild.GetComponent<RectTransform>();
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;
                rectTransform.localScale = Vector3.one;
            }

            childImage.sprite = sourceButton.image.sprite;

            // Kiểm tra nếu đối tượng trong existingScrollItems có tag là Weapon1
            GameObject existingWeapon1Item = existingScrollItems.Find(item => item.CompareTag(itemPrefab.tag));
            if (existingWeapon1Item != null)
            {
                // Xử lý nếu tìm thấy đối tượng có tag là Weapon1
                Debug.Log("Found an existing item with tag Weapon1.");
                // Thực hiện hành động cần thiết, ví dụ cập nhật sprite
                existingWeapon1Item.GetComponent<Image>().sprite = sourceButton.image.sprite;
            }
            else
            {
                // Nếu không tìm thấy, tạo một item mới
                GameObject newItem = Instantiate(itemPrefab, scrollViewContent.transform);
                Image itemImage = newItem.GetComponent<Image>();
                itemImage.sprite = sourceButton.image.sprite;

                // Đặt tag cho item nếu cần
                newItem.tag = itemPrefab.tag;

                existingScrollItems.Add(newItem);
            }
        }
    }
}
