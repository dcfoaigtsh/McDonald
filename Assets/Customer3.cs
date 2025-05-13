using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SingleCustomer3: MonoBehaviour
{
    public TextMeshProUGUI statementText;
    public Button responseButton;
    public GameObject qaManager;
    public CustomerManager customerManager;

    [TextArea]
    public string orderMessage = "Please talk to clerk 3 and help me buy:\nFried Chicken, Salad, Onion Rings, Juice";

    void OnEnable()
    {
        ShowGreeting();
    }

    void ShowGreeting()
    {
        statementText.text = "Can you help me buy some food?";
        responseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Yes";
        responseButton.onClick.RemoveAllListeners();
        responseButton.onClick.AddListener(ShowOrder);
    }

    void ShowOrder()
    {
        statementText.text = orderMessage;
        responseButton.GetComponentInChildren<TextMeshProUGUI>().text = "OK";
        responseButton.onClick.RemoveAllListeners();
        responseButton.onClick.AddListener(StartQA);
    }

    void StartQA()
    {
        gameObject.SetActive(false);
        qaManager.SetActive(true);
    }

    // 呼叫此函數讓下一位顧客開始
    public void NotifyCustomerManager()
    {
        if (customerManager != null)
            customerManager.NextCustomer();
    }
}
