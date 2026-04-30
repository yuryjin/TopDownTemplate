using UnityEngine;
using UnityEngine.UI;

public class TaskValidation : MonoBehaviour
{
    public Button confirmButton;
    public TMP_Text taskText;
    public GameObject rewardPanel;

    void Start()
    {
        confirmButton.onClick.AddListener(ConfirmTask);
        taskText.text = "Задание: Посади семена в огороде в реальном мире. Когда закончишь, нажми 'Я сделал'.";
        rewardPanel.SetActive(false);
    }

    void ConfirmTask()
    {
        // В реальности здесь можно добавить загрузку фото или голоса
        Debug.Log("Задание выполнено! Награда: +10 опыта.");
        rewardPanel.SetActive(true);
        // Добавить логику награды, например, увеличить опыт игрока
    }
}