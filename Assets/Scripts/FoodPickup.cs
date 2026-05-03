using UnityEngine;

/// <summary>
/// Собираемый предмет (еда). Требует триггер-коллайдер и Rigidbody2D у игрока с коллайдером.
/// </summary>
public class FoodPickup : MonoBehaviour
{
	[SerializeField] private string foodName = "Food";

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.attachedRigidbody == null)
			return;
		if (!other.CompareTag("Player") && other.GetComponentInParent<PlayerMovement>() == null)
			return;
		Debug.Log($"Collected food: {foodName}");
		Destroy(gameObject);
	}
}
