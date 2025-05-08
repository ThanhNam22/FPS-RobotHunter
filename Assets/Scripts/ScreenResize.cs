using System.Collections.Generic;
using UnityEngine;

public class ScreenResizer : MonoBehaviour
{
    public List<RectTransform> screens; // Các màn hình trong scroll view
    private RectTransform parentRectTransform;

    void Start()
    {
        parentRectTransform = GetComponent<RectTransform>();
        ResizeAndPositionScreens();
    }

    void ResizeAndPositionScreens()
    {
        float screenWidth = parentRectTransform.rect.width;
        float screenHeight = parentRectTransform.rect.height;

        // Điều chỉnh kích thước và vị trí các màn hình
        for (int i = 0; i < screens.Count; i++)
        {
            screens[i].sizeDelta = new Vector2(screenWidth, screenHeight);

            // Đặt vị trí màn hình liền kề màn hình trước đó
            screens[i].anchoredPosition = new Vector2(i * screenWidth, 0);
        }
    }
}
