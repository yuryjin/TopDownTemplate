using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public FoodData foodData;

    void Awake()
    {
        Apply();
    }

    public void Apply()
    {
        if (foodData == null) return;
        var img = GetComponent<Image>();
        if (img != null) img.sprite = foodData.sprite;
        gameObject.name = foodData.foodName;
    }
}
