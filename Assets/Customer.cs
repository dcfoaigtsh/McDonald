using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Header("List of all customer GameObjects in order")]
    public List<GameObject> customerList;  // 拖入所有顧客物件

    private int currentCustomerIndex = 0;

    void Start()
    {
        ActivateCustomer(currentCustomerIndex);
    }

    // 啟用指定顧客，其餘關閉
    void ActivateCustomer(int index)
    {
        for (int i = 0; i < customerList.Count; i++)
        {
            customerList[i].SetActive(i == index);
        }

        Debug.Log($"✅ 顧客 {index + 1} 出現！");
    }

    // 提供給 QAManager 呼叫，切換下一位顧客
    public void NextCustomer()
{
    // 如果已經處理完最後一位顧客，就不做任何事
    if (currentCustomerIndex >= customerList.Count)
    {
        Debug.Log("⚠️ 所有顧客都完成點餐！（已超出索引）");
        return;
    }

    // 關閉當前顧客
    customerList[currentCustomerIndex].SetActive(false);
    currentCustomerIndex++;

    // 如果還有下一位，顯示下一位
    if (currentCustomerIndex < customerList.Count)
    {
        ActivateCustomer(currentCustomerIndex);
    }
    else
    {
        Debug.Log("✅ 所有顧客都完成點餐！");
        // 在這裡觸發結算畫面或其他 UI
    }
}
}
