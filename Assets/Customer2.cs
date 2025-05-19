using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SingleCustomer2: MonoBehaviour
{
    public TextMeshProUGUI statementText;
    public Button responseButton;
    public GameObject qaManager;
    public CustomerManager customerManager;
    public GameObject completeIcon;
    

    [TextArea]
    public string orderMessage = "Please talk to clerk 2 and help me buy:\nHot Dog, Salad, Onion Rings, Sprite";

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
        if (completeIcon != null)
            completeIcon.SetActive(true);
        if (customerManager != null)
            customerManager.NextCustomer();
    }
}
