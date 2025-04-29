using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QAManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Question> MyQuestions;
    List<Question> unansweredQuestions;
    Question currentQuestion;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI statementText;
    public List<Button> optionButtons;
    public Button restartButton;

    int score = 0;
    void Start()
    {   
        MyQuestions.Clear();
        AddExtraQuestions();

        score = 0;
        UpdateScoreUI();

        unansweredQuestions = new List<Question>(MyQuestions);
        for (int i = 0; i < optionButtons.Count; i++)
        {
            int index = i;
            optionButtons[i].onClick.AddListener(() => StartCoroutine(OnOptionSelected(index)));
        }
        PickQuestion();
        restartButton.gameObject.SetActive(false);
    }

    void AddExtraQuestions()
    {
        MyQuestions.Add(new Question {
            statement = "What is the result of 3 == 3.0?",
            options = new List<string> {
                "A. 0", "B. 1", "C. True", "D. False"
            },
            Answer = "C. True"
        });

        MyQuestions.Add(new Question {
            statement = "Which one is not equal?",
            options = new List<string> {
                "A. \"apple\" == \"Apple\"",
                "B. 5 != 3",
                "C. 1 == True",
                "D. 0 == False"
            },
            Answer = "A. \"apple\" == \"Apple\""
        });

        MyQuestions.Add(new Question {
            statement = "What is 7 % 3?",
            options = new List<string> {
                "A. 1", "B. 2", "C. 3", "D. 0"
            },
            Answer = "A. 1"
        });

        MyQuestions.Add(new Question {
            statement = "Which is an assignment operator?",
            options = new List<string> {
                "A. +", "B. ==", "C. =", "D. !="
            },
            Answer = "C. ="
        });

        MyQuestions.Add(new Question {
            statement = "How many times will range(3) repeat?",
            options = new List<string> {
                "A. 2", "B. 3", "C. 4", "D. Infinite"
            },
            Answer = "B. 3"
        });

        MyQuestions.Add(new Question {
            statement = "If while(True): keeps running, it is a?",
            options = new List<string> {
                "A. Syntax error", "B. One-time run", "C. Infinite loop", "D. Skipped"
            },
            Answer = "C. Infinite loop"
        });

        MyQuestions.Add(new Question {
            statement = "Which one is true?",
            options = new List<string> {
                "A. 10 < 5", "B. 5 > 8", "C. 3 == 3", "D. 1 != 1"
            },
            Answer = "C. 3 == 3"
        });

        MyQuestions.Add(new Question {
            statement = "If x = 10, is x > 5 and x < 15?",
            options = new List<string> {
                "A. 0", "B. 1", "C. True", "D. False"
            },
            Answer = "C. True"
        });

        MyQuestions.Add(new Question {
            statement = "What is the result of False == 0?",
            options = new List<string> {
                "A. True", "B. False", "C. Error", "D. None"
            },
            Answer = "A. True"
        });

        MyQuestions.Add(new Question {
            statement = "x = 4\nWhat is (x > 0 and x < 10)?",
            options = new List<string> {
                "A. True", "B. False", "C. 0", "D. Error"
            },
            Answer = "A. True"
        });
    }

    void PickQuestion()
    {   
        foreach (var btn in optionButtons)
        {
            btn.interactable = true;
            btn.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }

        int temp = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[temp];
        statementText.text = currentQuestion.statement;
        for(int i = 0; i < optionButtons.Count; i++)
        {
            optionButtons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.options[i]; //opt 1:text in the button
            optionButtons[i].gameObject.name = currentQuestion.options[i]; // opt 2: name of the button gameObject
        }
        
    
    }

    IEnumerator OnOptionSelected(int index)
    {
        string selected = currentQuestion.options[index];
        Debug.Log("你選擇了：" + selected);

        foreach (var btn in optionButtons)
            btn.interactable = false;

        Color correctColor = Color.green;
        Color wrongColor = Color.red;

        // 顯示正確與錯誤答案顏色
        for (int i = 0; i < optionButtons.Count; i++)
        {
            var btnText = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (currentQuestion.options[i] == currentQuestion.Answer)
            {
                btnText.color = correctColor;
            }
            else if (i == index && currentQuestion.options[i] != currentQuestion.Answer)
            {
                btnText.color = wrongColor;
            }
        }

        // 判斷答題正確與否
        if (selected == currentQuestion.Answer)
        {
            Debug.Log("正確答案！");
            score += 10;
            UpdateScoreUI();
        }
        else
        {
            Debug.Log("錯誤答案！");
        }

        yield return new WaitForSeconds(1f);

        unansweredQuestions.Remove(currentQuestion);

        if (unansweredQuestions.Count > 0)
        {
            PickQuestion();
        }
        else
        {
            statementText.text = "Quiz Complete!\nYour score: " + score + " / " + (MyQuestions.Count * 10);
            foreach (var btn in optionButtons)
            {
                btn.gameObject.SetActive(false);
            }
            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestartQuiz()
    {
    score = 0;
    UpdateScoreUI();

    unansweredQuestions = new List<Question>(MyQuestions);

    foreach (var btn in optionButtons)
    {
        btn.gameObject.SetActive(true);
    }

    restartButton.gameObject.SetActive(false); // 再玩一次按鈕隱藏
    PickQuestion();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]

public class Question
{
    public string statement;
    public List<string> options;
    public string Answer;
  
}
