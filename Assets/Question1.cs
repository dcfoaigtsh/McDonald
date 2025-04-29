using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject talkButton;  // 頭上的Talk按鈕
    public DialogManager dialogManager;  // 管理對話框

    void Start()
    {
    
        talkButton.SetActive(true);  // 一開始就顯示按鈕
    }

    // 不需要Update了！

    public void OnTalkButtonClicked()
    {
        
        talkButton.SetActive(false);  // 按下後先隱藏自己
        FindObjectOfType<SojaExiles.MouseLook>().UnlockCursor();
        dialogManager.OpenDialog("Can you help me buy a hamburger?");
    }

    // 新增一個方法，給DialogManager用，當對話框關掉時叫回來
    public void ReactivateTalkButton()
    {
        talkButton.SetActive(true);
    }
}
