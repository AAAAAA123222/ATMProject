using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }

    }
    public const string userNameKey = "ID/userName";
    public const string bucksKey = "ID/bucks";
    public const string balanceKey = "ID/balance";

    public string currentLoginID;
    public UserData userData;

    public GameObject LoginManager;
    public GameObject popupBank;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveData()
    {
        string name = userData.userName;
        int bucks = userData.bucks;
        int balance=userData.balance;
        PlayerPrefs.SetString(currentLoginID+ "/Name", null);
        PlayerPrefs.SetInt(currentLoginID+ "/Bucks", bucks);
        PlayerPrefs.SetInt(currentLoginID+ "/Balance", balance);
    }

    public UserData LoadData(string ID)
    {
        string name = PlayerPrefs.GetString(ID+"/Name", null);
        int bucks = PlayerPrefs.GetInt(ID+"/Bucks", 55000);
        int balance = PlayerPrefs.GetInt(ID+"/Balance", 125000);

        currentLoginID = ID;
        userData = new UserData(name, bucks, balance);
        return userData;
    }

    public void CreateNewAccount(string ID, string password, string name)
    {
        PlayerPrefs.SetString(ID + "/ID", ID);
        PlayerPrefs.SetString(ID + "/Password", password);
        PlayerPrefs.SetString(ID + "/Name", name);
    }
    public void TogglePopups()
    {
        //둘 다 활성화되는 걸 막기 위한 방어코드
        if(LoginManager.activeInHierarchy==popupBank.activeInHierarchy)
        {
            LoginManager.SetActive(true);
            popupBank.SetActive(false);
        }
        //이후 값 반전
        LoginManager.SetActive(!LoginManager.activeInHierarchy);
        popupBank.SetActive(!popupBank.activeInHierarchy);
    }
    
}