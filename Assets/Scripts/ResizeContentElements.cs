using UnityEngine;

public class ScaleContentAndElements : MonoBehaviour
{
    public RectTransform canvasRectTransform; // Canvas chính
    public RectTransform contentRectTransform; // Content của Scroll View

    void Start()
    {
        ScaleContentAndChildren();
    }

    void ScaleContentAndChildren()
    {
        // Lấy kích thước của Canvas
        Vector2 canvasSize = canvasRectTransform.rect.size;

        // Đặt kích thước của Content gấp 3 lần Canvas (vì có 3 thành phần con)
        contentRectTransform.sizeDelta = new Vector2(canvasSize.x * 3, canvasSize.y);

        // Lặp qua từng thành phần trong Content và điều chỉnh kích thước
        foreach (Transform child in contentRectTransform)
        {
            RectTransform childRect = child.GetComponent<RectTransform>();
            if (childRect != null)
            {
                // Đặt kích thước của mỗi thành phần để chiếm toàn bộ không gian Canvas
                childRect.sizeDelta = canvasSize;
            }
        }
    }

    void Update()
    {
        // Gọi lại hàm ScaleContentAndChildren khi kích thước Canvas thay đổi
        ScaleContentAndChildren();
    }
}
