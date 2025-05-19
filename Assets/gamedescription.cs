using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameDescription : MonoBehaviour
{
    public GameObject InfomationBoard;
    public TextMeshProUGUI InfoContent;
    public Button CloseButton, NextButton, PreviousButton;
    public List<Info> Infos = new List<Info>();  // 初始化避免 null
    public int currentInfo;

    void Awake()
    {
        Infos.Add(new Info()
        {
            Content ="Welcome to 'Order Assistant'!\n\nYou are a new helper. Your job is to take orders from customers. Read carefully and choose the right food!"
        });

        Infos.Add(new Info()
        {
            Content = "1. When a customer comes, click to talk.\n2. Follow the steps and find the right clerk.\n3. Each customer wants a main dish, side dish, snack, and drink."
        });

        Infos.Add(new Info()
        {
            Content = "Help every customer finish their order!\n\nWhen all orders are done, you’ll see your results. Good luck!"
        });
    }

    void Start()
    {
        currentInfo = 0;
        InfomationBoard.SetActive(true);
        SetupPageContent();

        NextButton.onClick.AddListener(() => TurnPage(1));
        PreviousButton.onClick.AddListener(() => TurnPage(-1));
        CloseButton.onClick.AddListener(() => InfomationBoard.SetActive(false));

        UpdateButtonVisibility();
    }

    void SetupPageContent()
    {
        InfoContent.text = Infos[currentInfo].Content;
    }

    void TurnPage(int dir)
    {
        currentInfo += dir;
        currentInfo = Mathf.Clamp(currentInfo, 0, Infos.Count - 1);

        SetupPageContent();
        UpdateButtonVisibility();
    }

    void UpdateButtonVisibility()
    {
        if (currentInfo == 0)
        {
            PreviousButton.gameObject.SetActive(false);
            NextButton.gameObject.SetActive(true);
            CloseButton.gameObject.SetActive(false);
        }
        else if (currentInfo == Infos.Count - 1)
        {
            PreviousButton.gameObject.SetActive(true);
            NextButton.gameObject.SetActive(false);
            CloseButton.gameObject.SetActive(true);
        }
        else
        {
            PreviousButton.gameObject.SetActive(true);
            NextButton.gameObject.SetActive(true);
            CloseButton.gameObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class Info
{
    public string Title;
    public string Content;
}
