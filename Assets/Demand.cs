using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public Button closeButton;

    public NPCInteraction npcInteraction; // ⭐️記得拉進Inspector！

    void Start()
    {
        dialogPanel.SetActive(false);
        closeButton.onClick.AddListener(CloseDialog);
    }

    public void OpenDialog(string message)
    {
        dialogPanel.SetActive(true);
        dialogText.text = message;
    }

    public void CloseDialog()
    {
        dialogPanel.SetActive(false);
        FindObjectOfType<SojaExiles.MouseLook>().LockCursor();
        if (npcInteraction != null)
        {
            npcInteraction.ReactivateTalkButton();  // ⭐️ 關掉對話框後叫NPC把Talk按鈕打開
        }
    }
}
