using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QAManager4 : MonoBehaviour
{
    public TextMeshProUGUI statementText;
    public TextMeshProUGUI scoreText;
    public List<Button> optionButtons;
    public Button restartButton;
    public SingleCustomer4 singleCustomer;  // ✅ 改為連接該顧客

    private int currentStage = 0;
    private List<string> selections = new List<string>();
    private int totalPrice = 0;

    private string[] questions = new string[] {
        "Please choose your main dish",
        "Please choose your side dish",
        "Please choose your snack",
        "Please choose your drink",
        "Please select the correct total price"
    };

    private List<List<(string name, int price)>> optionsPerStage = new List<List<(string, int)>> {
        new List<(string, int)> { ("Burger", 80), ("Hot Dog", 70), ("Sandwich", 75), ("Fried Chicken", 90) },
        new List<(string, int)> { ("French Fries", 40), ("Salad", 50), ("Corn Soup", 45), ("Curly Fries", 50) },
        new List<(string, int)> { ("Chicken Nuggets", 60), ("Onion Rings", 55), ("Donut", 50), ("Egg Tart", 50) },
        new List<(string, int)> { ("Coke", 60), ("Sprite", 60), ("Milk Tea", 70), ("Juice", 65) },
        new List<(string, int)> { ("$190", 190), ("$220", 220), ("$240", 240), ("$255", 255) }
    };

    void Start()
    {
        restartButton.gameObject.SetActive(false);
        scoreText.text = "";
        selections.Clear();
        totalPrice = 0;
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
            bool isCorrect = (index == 3);  // ✅ 第四個選項是正確答案 ($255)

            for (int i = 0; i < optionButtons.Count; i++)
            {
                var btnText = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (optionsPerStage[4][i].price == totalPrice)
                    btnText.color = Color.green;
                else if (i == index)
                    btnText.color = Color.red;
            }

            yield return new WaitForSeconds(1f);

            statementText.text = isCorrect ? "Order Completed!\nYour selections:" : "Wrong Price!\nYour selections:";
            for (int i = 0; i < selections.Count; i++)
                statementText.text += $"\n- {selections[i]}";
            statementText.text += $"\nTotal: {selectedOption.name}";

            restartButton.gameObject.SetActive(true);
            foreach (var btn in optionButtons)
                btn.gameObject.SetActive(false);

            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);

            // ✅ 通知顧客流程完成
            if (singleCustomer != null)
            {
                singleCustomer.NotifyCustomerManager();
            }
        }
    }

    public void RestartQuiz()
    {
        currentStage = 0;
        selections.Clear();
        totalPrice = 0;
        restartButton.gameObject.SetActive(false);

        foreach (var btn in optionButtons)
            btn.gameObject.SetActive(true);

        ShowCurrentStage();
    }
}
