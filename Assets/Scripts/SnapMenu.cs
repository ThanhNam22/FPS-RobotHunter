using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SnapMenu : MonoBehaviour
{
    public GameObject scrollBar;

    private float[] position;
    private int currentIndex = 0;
    private bool isTransitioning = false;
    private bool isDragging = false;
    private float scrollpos = 0; // This is initialized to zero

    void Start()
    {
        int childCount = transform.childCount;
        position = new float[childCount];
        float distance = 1f / (childCount - 1);

        for (int i = 0; i < position.Length; i++)
        {
            position[i] = distance * i;
        }
    }

    void Update()
    {
        if (isTransitioning) return;

        if (Input.GetMouseButtonDown(0)) // Detect the start of a drag
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0)) // Detect the end of a drag
        {
            isDragging = false;
        }

        if (isDragging)
        {
            // Update scrollpos based on user dragging
            scrollpos = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            // Snap to nearest position when user stops dragging
            float distance = 1f / (position.Length - 1);
            for (int i = 0; i < position.Length; i++)
            {
                if (scrollpos < position[i] + (distance / 2) && scrollpos > position[i] - (distance / 2))
                {
                    scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, position[i], 0.1f);
                    currentIndex = i; // Update current index
                }
            }
        }
    }

    public void GoToNextScreen()
    {
        if (currentIndex < position.Length - 1)
        {
            currentIndex++;
            StartCoroutine(SmoothScrollToPosition(position[currentIndex]));
        }
    }

    public void GoToPreviousScreen()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            StartCoroutine(SmoothScrollToPosition(position[currentIndex]));
        }
    }

    public void GoToPage(int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex < position.Length)
        {
            currentIndex = pageIndex;
            StartCoroutine(SmoothScrollToPosition(position[currentIndex]));
        }
    }

    private IEnumerator SmoothScrollToPosition(float targetPosition)
    {
        isTransitioning = true; // Disable snapping during transition
        float duration = 0.5f; // Duration of the transition
        float elapsedTime = 0f;
        float startValue = scrollBar.GetComponent<Scrollbar>().value;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(startValue, targetPosition, elapsedTime / duration);
            yield return null;
        }

        scrollBar.GetComponent<Scrollbar>().value = targetPosition;
        isTransitioning = false; // Enable snapping after transition
    }
}
