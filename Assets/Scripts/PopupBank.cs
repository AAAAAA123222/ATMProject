using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    //원본 데이터
    private UserData userData;

    //그 원본 데이터를 담아놓을 값들
    //serializefield는 값을 확인하기 위해 임시로 달아놓은 키워드 (삭제 예정)
    [SerializeField] private string userName;
    [SerializeField] private int balance;
    [SerializeField] private int bucks;

    //값을 바꿀 텍스트
    public TextMeshProUGUI buckValue;
    public TextMeshProUGUI balanceValue;
    public TextMeshProUGUI userNameText;

    //활성화하고 비활성화시킬 게임오브젝트
    public GameObject withdrawUI;
    public GameObject depositUI;
    public GameObject transferUI;
    public GameObject withdrawButton;
    public GameObject depositButton;
    public GameObject transferButton;
    public GameObject errorPopup;

    //값을 가져올 게임오브젝트(인풋필드)
    public InputField depositInputField;
    public InputField withdrawInputField;
    public InputField targetToTransfer;
    public InputField transferInputField;


    // Start is called before the first frame update
    void Start()
    {
        GetUserData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetUserData()
    {
        userData = GameManager.Instance.userData;
        userName = userData.userName;
        balance = userData.balance;
        bucks = userData.bucks;
        Refresh();
    }

    public void Refresh()
    {
        buckValue.text=bucks.ToString("N0");
        balanceValue.text=balance.ToString("N0");
        userNameText.text=userName;
    }

    public void HideSelectButtons()
    {
        depositButton.SetActive(false);
        withdrawButton.SetActive(false);
        transferButton.SetActive(false);
    }

    public void OnDepositButton()
    {
        if (depositButton != null && depositUI!=null&& withdrawButton != null)
        {
            HideSelectButtons();
            depositUI.SetActive(true);
        }
    }

    public void OnWithdrawButton()
    {
        if (withdrawButton != null&&withdrawUI!=null&& depositButton != null)
        {
            HideSelectButtons();
            withdrawUI.SetActive(true);
        }
    }
    public void OnTransferButton()
    {
        if (withdrawButton != null&&withdrawUI!=null&& depositButton != null)
        {
            HideSelectButtons();
            transferUI.SetActive(true);
        }
    }

    public void OnBackButton()
    {
        if (withdrawButton != null && withdrawUI!=null&& depositButton!=null&&depositUI!=null)
        {
            withdrawUI.SetActive(false);
            depositUI.SetActive(false);
            transferUI.SetActive(false);
            withdrawButton.SetActive(true);
            depositButton.SetActive(true);
            transferButton.SetActive(true);
        }
    }

    public void Toggle (GameObject go)
    {
        if (go != null)
        {
            bool isActive = go.activeInHierarchy;
            go.SetActive(!isActive);
        }
    }

    public void Deposit (int number)
    {
        if (bucks>=number)
        {
            UserData userData =GameManager.Instance.userData;

            bucks -= number;
            userData.bucks = bucks;
            balance += number;
            userData.balance= balance;
            GameManager.Instance.SaveData();

            Refresh();
        }
        else
        {
            Debug.Log("현금이 부족합니다.");
        }
    }

    public void CustomDeposit()
    {
        int desiredNumber;
        bool isnum = int.TryParse(depositInputField.text, out desiredNumber);

        if (isnum)
        {
            Deposit(desiredNumber);
            Refresh();
        }
        else
        {
            Debug.Log("돈이 없거나 숫자가 아닙니다.");
        }
    }
    public void Withdraw(int number)
    {
        if (balance >= number)
        {
            UserData userData = GameManager.Instance.userData;

            bucks += number;
            userData.bucks = bucks;
            balance -= number;
            userData.balance = balance;
            GameManager.Instance.SaveData();

            Refresh();
        }
        else
        {
            Debug.Log("잔액이 부족합니다.");
        }
    }
    public void CustomWithdraw()
    {
        int desiredNumber;
        bool isnum = int.TryParse(withdrawInputField.text, out desiredNumber);

        if (isnum)
        {
            Withdraw(desiredNumber);
            Refresh();
        }
        else
        {
            Debug.Log("숫자가 아닙니다.");
        }
    }    

    public void Transfer()
    {
        int desiredNumber;
        bool isnum = int.TryParse(transferInputField.text, out desiredNumber);
        if (!isnum)
        {
            Debug.Log("숫자가 아님");
        }
        bool isValidTarget = false;

        string ID = PlayerPrefs.GetString(targetToTransfer.text + "/ID", null);
        if (!(string.IsNullOrEmpty(ID)))
        {
            isValidTarget = true;
        }
        else
        {
            Debug.Log("대상이 없음");
        }

        bool isNotSelf=false;

        if (!(ID==GameManager.Instance.currentLoginID))
        {
            isNotSelf = true;
        }
        else
        {
            Debug.Log("대상이 자신임");
        }

        if (isnum&& isValidTarget &&isNotSelf)
        {
            if (balance >= desiredNumber)
            {
                balance -= desiredNumber;
                userData.balance = balance;
                PlayerPrefs.SetInt(targetToTransfer.text + "/Balance", PlayerPrefs.GetInt(targetToTransfer.text + "/Balance", 0) + desiredNumber);
                GameManager.Instance.SaveData();
                Refresh();
            }
        }
        else
        {
            errorPopup.SetActive(true);
        }
    }

    public void OnErrorCloseButton()
    {
        errorPopup.SetActive(false);
    }

}