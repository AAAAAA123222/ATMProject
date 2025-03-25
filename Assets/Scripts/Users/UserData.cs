using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string userName;
    public int bucks;
    public int balance;

    public UserData(string userName, int bucks, int balance)
    {
        this.userName = userName;
        this.bucks = bucks;
        this.balance = balance;
    }


}
