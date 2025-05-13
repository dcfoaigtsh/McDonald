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
            Title = "Welcome",
            Content = "Welcome to 'Order Assistant'!\n\nYou are a new assistant tasked with helping customers place their orders. Each customer will tell you what they want—read carefully and make sure you get it right!"
        });

        Infos.Add(new Info()
        {
            Title = "How to Play",
            Content = "1. When a customer appears, click the button to talk to them.\n2. Follow the instructions and go to the correct clerk to place the order.\n3. Each customer will ask for a main dish, side dish, snack, and drink."
        });

        Infos.Add(new Info()
        {
            Title = "Your Goal",
            Content = "Help all customers complete their orders correctly.\n\nOnce all orders are completed, the game will move to the result screen to see how well you did. Good luck!"
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
