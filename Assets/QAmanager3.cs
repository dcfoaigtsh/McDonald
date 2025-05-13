// ✅ Updated QAManager3.cs to match QAManager logic and correct answers
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QAManager3 : MonoBehaviour
{
    public TextMeshProUGUI statementText;
    public TextMeshProUGUI scoreText;
    public List<Button> optionButtons;
    public Button restartButton;
    public SingleCustomer3 singleCustomer;

    private int currentStage = 0;
    private List<string> selections = new List<string>();
    private int totalPrice = 0;
    private bool isCorrect = false;

    private string[] questions = new string[] {
        "Please choose your main dish",
        "Please choose your side dish",
        "Please choose your snack",
        "Please choose your drink",
        "Please select the correct total price"
    };

    private List<string> correctAnswers = new List<string> {
        "Sandwich",
        "Corn Soup",
        "Donut",
        "Milk Tea"
    };

    private List<List<(string name, int price)>> optionsPerStage = new List<List<(string, int)>> {
        new List<(string, int)> { ("Burger", 80), ("Hot Dog", 70), ("Sandwich", 75), ("Fried Chicken", 90) },
        new List<(string, int)> { ("French Fries", 40), ("Salad", 50), ("Corn Soup", 45), ("Curly Fries", 50) },
        new List<(string, int)> { ("Chicken Nuggets", 60), ("Onion Rings", 55), ("Donut", 50), ("Egg Tart", 50) },
        new List<(string, int)> { ("Coke", 60), ("Sprite", 60), ("Milk Tea", 70), ("Juice", 65) },
        new List<(string, int)> { ("$190", 190), ("$220", 220), ("$240", 240), ("$260", 260) }
    };

    void Start()
    {
        restartButton.gameObject.SetActive(false);
        scoreText.text = "";
        selections.Clear();
        totalPrice = 0;
        isCorrect = false;
        BindButtons();
        ShowCurrentStage();
    }

    void BindButtons()
    {
        for (int i = 0; i < optionButtons.Count; i++)
        {
            int idx = i;
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => StartCoroutine(OnOptionSelected(idx)));
        }

        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(RestartQuiz);
    }

    void ShowCurrentStage()
    {
        foreach (var btn in optionButtons)
        {
            btn.interactable = true;
            btn.gameObject.SetActive(true);
            btn.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }

        statementText.text = questions[currentStage];

        for (int i = 0; i < optionButtons.Count; i++)
        {
            var option = optionsPerStage[currentStage][i];
            string displayText = currentStage == 4 ? option.name : $"{option.name} (${option.price})";
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = displayText;
        }
    }

    IEnumerator OnOptionSelected(int index)
    {
        foreach (var btn in optionButtons)
            btn.interactable = false;

        var selectedOption = optionsPerStage[currentStage][index];

        if (currentStage < 4)
        {
            selections.Add(selectedOption.name);
            totalPrice += selectedOption.price;
            currentStage++;
            yield return new WaitForSeconds(1f);
            ShowCurrentStage();
        }
        else
        {
            bool priceCorrect = selectedOption.price == 240;

            bool orderCorrect = true;
            for (int i = 0; i < correctAnswers.Count; i++)
            {
                if (i >= selections.Count || selections[i] != correctAnswers[i])
                {
                    orderCorrect = false;
                    break;
                }
            }

            isCorrect = (orderCorrect && priceCorrect);

            for (int i = 0; i < optionButtons.Count; i++)
            {
                var btnText = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                int thisPrice = optionsPerStage[4][i].price;

                if (thisPrice == 240 && orderCorrect)
                {
                    btnText.color = Color.green;
                }
                else if (i == index)
                {
                    btnText.color = Color.red;
                }
            }

            yield return new WaitForSeconds(1f);

            if (orderCorrect && priceCorrect)
                statementText.text = "Order Completed!\nYour selections:";
            else if (orderCorrect && !priceCorrect)
                statementText.text = "Wrong Price!\nYour selections:";
            else
                statementText.text = "Wrong Order!\nYour selections:";

            for (int i = 0; i < selections.Count; i++)
            {
                if (i < correctAnswers.Count)
                {
                    if (selections[i] == correctAnswers[i])
                        statementText.text += $"\n<color=green>- {selections[i]}</color>";
                    else
                        statementText.text += $"\n<color=red>- {selections[i]}</color>";
                }
                else
                {
                    statementText.text += $"\n<color=red>- {selections[i]}</color>";
                }
            }

            if (!orderCorrect || !priceCorrect)
                statementText.text += $"\n<color=red>Total: {selectedOption.name}</color>";
            else
                statementText.text += $"\n<color=green>Total: {selectedOption.name}</color>";

            foreach (var btn in optionButtons)
                btn.gameObject.SetActive(false);

            restartButton.gameObject.SetActive(!isCorrect);

            if (isCorrect)
            {
                yield return new WaitForSeconds(1f);
                gameObject.SetActive(false);

                if (singleCustomer != null)
                    singleCustomer.NotifyCustomerManager();
            }
        }
    }

    public void RestartQuiz()
    {
        currentStage = 0;
        selections.Clear();
        totalPrice = 0;
        isCorrect = false;

        restartButton.gameObject.SetActive(false);

        foreach (var btn in optionButtons)
            btn.gameObject.SetActive(true);

        ShowCurrentStage();
    }
}
