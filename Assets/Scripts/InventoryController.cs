using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public GameObject itemPrefab;
    public FoodData[] foods;

    void Start()
    {
        for (int i = 0; i < foods.Length; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            GameObject item = Instantiate(itemPrefab, slot.transform);
            item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            ItemView view = item.GetComponent<ItemView>();
            view.foodData = foods[i];
            view.Apply();
            slot.currentItem = item;
        }
    }
}
