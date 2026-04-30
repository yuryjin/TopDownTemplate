using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public Button nextButton;
    public Image slideImage;
    public GameObject slidePanel;

    private string[] dialogues = {
        "Привет, внучек! Сегодня мы поработаем на огороде. Но сначала я расскажу тебе, как это было раньше...",
        "Нажми 'Далее', чтобы начать историю."
    };

    private Sprite[] slides; // Массив спрайтов для слайдов
    private string[] slideTexts = {
        "В Древнем Египте люди работали в полях вручную, используя простые инструменты.",
        "В Ассирийской империи фермеры выращивали зерно и скот, используя ирригацию.",
        "На Руси крестьяне пахали землю сохой и сеяли вручную.",
        "В 15-м веке появились первые плуги, но работа оставалась тяжелой.",
        "В 18-м веке началась механизация, но многие все еще работали вручную.",
        "В наше время большие агропроизводства используют тракторы и машины.",
        "Но маленькие хозяйства, как наше, возвращают нас к традициям и заботе о земле."
    };

    private int currentDialogueIndex = 0;
    private int currentSlideIndex = 0;
    private bool inSlides = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
        slidePanel.SetActive(false);
        nextButton.onClick.AddListener(Next);
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        currentDialogueIndex = 0;
        ShowDialogue();
    }

    void ShowDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
            StartSlides();
        }
    }

    void StartSlides()
    {
        inSlides = true;
        slidePanel.SetActive(true);
        currentSlideIndex = 0;
        ShowSlide();
    }

    void ShowSlide()
    {
        if (currentSlideIndex < slideTexts.Length)
        {
            dialogueText.text = slideTexts[currentSlideIndex];
            // slideImage.sprite = slides[currentSlideIndex]; // Если есть спрайты
        }
        else
        {
            slidePanel.SetActive(false);
            // Переход к заданию
            Debug.Log("Переход к офлайн-заданию: работа на огороде.");
        }
    }

    void Next()
    {
        if (!inSlides)
        {
            currentDialogueIndex++;
            ShowDialogue();
        }
        else
        {
            currentSlideIndex++;
            ShowSlide();
        }
    }
}