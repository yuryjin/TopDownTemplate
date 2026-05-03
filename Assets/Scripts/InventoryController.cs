using UnityEngine;

public class InventoryController : MonoBehaviour
{
	public GameObject inventoryPanel;
	public GameObject slotPrefab;
	public GameObject itemPrefab;
	[Tooltip("Если itemPrefab не задан — берётся первый элемент (как в старых сценах).")]
	public GameObject[] itemPrefabs;
	public int slotCount = 24;
	public FoodData[] foods;

	void Start()
	{
		GameObject prefab = ResolveItemPrefab();
		if (inventoryPanel == null || slotPrefab == null || prefab == null)
		{
			Debug.LogError("InventoryController: задайте inventoryPanel, slotPrefab и itemPrefab (или itemPrefabs[0]).");
			return;
		}

		for (int i = 0; i < slotCount; i++)
		{
			Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
			if (slot == null)
			{
				Debug.LogError("InventoryController: у slotPrefab нет компонента Slot.");
				continue;
			}

			FoodData fd = foods != null && i < foods.Length ? foods[i] : null;
			if (fd == null)
				continue;

			GameObject item = Instantiate(prefab, slot.transform);
			item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			ItemView view = item.GetComponent<ItemView>();
			if (view != null)
			{
				view.foodData = fd;
				view.Apply();
			}
			slot.currentItem = item;
		}
	}

	GameObject ResolveItemPrefab()
	{
		if (itemPrefab != null)
			return itemPrefab;
		if (itemPrefabs != null && itemPrefabs.Length > 0 && itemPrefabs[0] != null)
			return itemPrefabs[0];
		return null;
	}

	public bool TryAddFood(FoodData data)
	{
		if (data == null)
			return false;
		GameObject prefab = ResolveItemPrefab();
		if (inventoryPanel == null || prefab == null)
			return false;

		foreach (Transform child in inventoryPanel.transform)
		{
			Slot slot = child.GetComponent<Slot>();
			if (slot == null || slot.currentItem != null)
				continue;

			GameObject item = Instantiate(prefab, slot.transform);
			item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			ItemView view = item.GetComponent<ItemView>();
			if (view != null)
			{
				view.foodData = data;
				view.Apply();
			}
			slot.currentItem = item;
			return true;
		}
		return false;
	}
}
