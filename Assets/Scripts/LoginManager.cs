using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    //�޾ƿ� ID ����
    private string _name;
    private string password;

    //Ȱ��ȭ��Ű�ų� ���� �� ���ӿ�����Ʈ
    public GameObject PopupLogin;
    public GameObject PopupSignup;
    public GameObject LoginError;
    public GameObject SignupError;


    //���� �޾ƿ��ų� �Ҵ��ؾߵǴ� ���ӿ�����Ʈ
    //�α���
    public PopupBank popupbank;
    [Header ("�α��� ����")]
    public InputField IDLogin;
    public InputField PasswordLogin;

    //ȸ������

    [Header("ȸ������ ����")]
    public InputField IDInput;
    public InputField nameInput;
    public InputField passwordInput;
    public InputField passwordConfirmInput;
    public TextMeshProUGUI notifierText;

    private void Start()
    {
        notifierText.text = string.Empty;
    }

    public bool ConfirmSignUp()
    {
        if (string.IsNullOrEmpty(IDInput.text))
        {
            notifierText.text = "ID�� Ȯ���� �ּ���.";
        }
        if (string.IsNullOrEmpty(nameInput.text))
        {
            notifierText.text = "�̸��� Ȯ���� �ּ���.";
        }
        if (string.IsNullOrEmpty(passwordInput.text))
        {
            notifierText.text = "��й�ȣ�� Ȯ���� �ּ���.";
        }
        if (!(string.IsNullOrEmpty(passwordConfirmInput.text)))
        {
            if (passwordInput.text == passwordConfirmInput.text)
            {
                SignUp();
                return true;
            }
            else
            {
                notifierText.text = "�Է��� �ΰ��� ��й�ȣ�� ���� �ٸ��ϴ�.";
            }
        }
        else
        {
            notifierText.text = "��й�ȣ Ȯ���� �� �Ǿ� �ֽ��ϴ�.";
        }
        return false;
    }
    public void SignUp()
    {
        string ID = IDInput.text;
        string name = nameInput.text;
        string password= passwordInput.text;

        GameManager.Instance.CreateNewAccount(ID, password, name);
    }
    public bool LogIn()
    {
        if (string.IsNullOrEmpty(IDLogin.text))
        {
            Debug.Log("ID ĭ�� �������");
            return false;
        }
        if (string.IsNullOrEmpty(PasswordLogin.text))
        {
            Debug.Log("��й�ȣ ĭ�� �������");
            return false;
        }

        string ID=PlayerPrefs.GetString(IDLogin.text + "/ID",null);
        if (ID == null)
        {
            Debug.Log("ID�� ����");
        }
        
        string password = PlayerPrefs.GetString(IDLogin.text + "/Password",null);
        if (password==null)
        {
            Debug.Log("��й�ȣ�� ����");
        }

        if (PasswordLogin.text == password)
        {
            GameManager.Instance.LoadData(ID);
            popupbank.GetUserData();
            GameManager.Instance.TogglePopups();
            Debug.Log("�α��� ����");
            return true;
        }
        else
        {
            Debug.Log("��й�ȣ�� �ٸ�");
        }

        Debug.Log("?");
        return false;
    }

    public void OnLoginButton()
    {
        bool iscompleted = LogIn();
        if (!iscompleted)
        {
            LoginError.SetActive(true);
        }
    }
    public void OnLoginSignupButton()
    {
        PopupSignup.SetActive(true);
    }
    public void OnCloseButton()
    {   
        PopupSignup.SetActive(false);
    }
    public void OnSignupButton()
    {
        bool iscompleted = ConfirmSignUp();
        if(iscompleted)
        {
            PopupSignup.SetActive(false);
        }
        else
        {
            SignupError.SetActive(true);
        }
    }
    public void OnSignupErrorButton()
    {
        SignupError.SetActive(false);
    }
    public void OnLoginErrorButton()
    {
        LoginError.SetActive(false);
    }

}
