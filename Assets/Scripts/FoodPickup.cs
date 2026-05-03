using UnityEngine;

/// <summary>
/// Подбираемая еда: добавляет запись в инвентарь по FoodData и синхронизирует спрайт на карте.
/// </summary>
public class FoodPickup : MonoBehaviour
{
	[SerializeField] private FoodData foodData;
	[SerializeField] private string foodName = "Food";

	void Start()
	{
		if (foodData != null && foodData.sprite != null)
		{
			var sr = GetComponent<SpriteRenderer>();
			if (sr != null)
				sr.sprite = foodData.sprite;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.attachedRigidbody == null)
			return;
		if (!other.CompareTag("Player") && other.GetComponentInParent<PlayerMovement>() == null)
			return;

		if (foodData != null)
		{
			var inv = FindFirstObjectByType<InventoryController>();
			if (inv != null && inv.TryAddFood(foodData))
			{
				Destroy(gameObject);
				return;
			}
			Debug.LogWarning($"Инвентарь полон или недоступен: {foodData.foodName}");
			return;
		}

		Debug.Log($"Collected food: {foodName}");
		Destroy(gameObject);
	}
}
