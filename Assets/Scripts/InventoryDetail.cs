using System.Collections;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryDetail;
    [SerializeField] GameObject inventory;
    [SerializeField] float moveDuration = 1f;
    private Vector3 targetPositionDetail;
    private Vector3 originalPositionDetail;
    private Vector3 targetPositionInventory;
    private Vector3 originalPositionInventory;

    void Start()
    {
        // Lưu lại vị trí ban đầu và vị trí đích khi mở inventoryDetail
        originalPositionDetail = inventoryDetail.transform.localPosition;
        targetPositionDetail = new Vector3(0, originalPositionDetail.y, originalPositionDetail.z);

        // Lưu lại vị trí ban đầu và vị trí đích khi mở inventory
        originalPositionInventory = inventory.transform.localPosition;
        targetPositionInventory = new Vector3(-originalPositionInventory.x, originalPositionInventory.y, originalPositionInventory.z); // Di chuyển theo chiều ngược lại
    }

    public void InventoryOpen()
    {
        StopAllCoroutines();
        inventoryDetail.SetActive(true);
        inventory.SetActive(true);

        // Di chuyển inventoryDetail vào vị trí x = 0 và inventory ra ngoài
        StartCoroutine(MoveToPosition(inventoryDetail, originalPositionDetail, targetPositionDetail));
        StartCoroutine(MoveToPosition(inventory, originalPositionInventory, targetPositionInventory, true));

        Time.timeScale = 0;
    }

    public void InventoryClose()
    {
        StopAllCoroutines();

        // Di chuyển inventoryDetail về vị trí ban đầu và inventory trở lại vị trí ban đầu
        StartCoroutine(CloseInventoryCoroutine());

        Time.timeScale = 1;
    }

    private IEnumerator CloseInventoryCoroutine()
    {
        // Di chuyển inventoryDetail về vị trí ban đầu
        yield return StartCoroutine(MoveToPosition(inventoryDetail, targetPositionDetail, originalPositionDetail));

        // Sau khi inventoryDetail di chuyển xong, hiện inventory
        inventory.SetActive(true);
        yield return StartCoroutine(MoveToPosition(inventory, targetPositionInventory, originalPositionInventory, false));
    }

    private IEnumerator MoveToPosition(GameObject obj, Vector3 start, Vector3 end, bool deactivateAfterMove = false)
    {
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            obj.transform.localPosition = Vector3.Lerp(start, end, elapsed / moveDuration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        obj.transform.localPosition = end;

        if (deactivateAfterMove)
        {
            obj.SetActive(false);
        }
    }
}
