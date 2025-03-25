using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    //받아올 ID 변수
    private string _name;
    private string password;

    //활성화시키거나 꺼야 할 게임오브젝트
    public GameObject PopupLogin;
    public GameObject PopupSignup;
    public GameObject LoginError;
    public GameObject SignupError;


    //값을 받아오거나 할당해야되는 게임오브젝트
    //로그인
    public PopupBank popupbank;
    [Header ("로그인 변수")]
    public InputField IDLogin;
    public InputField PasswordLogin;

    //회원가입

    [Header("회원가입 변수")]
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
            notifierText.text = "ID를 확인해 주세요.";
        }
        if (string.IsNullOrEmpty(nameInput.text))
        {
            notifierText.text = "이름을 확인해 주세요.";
        }
        if (string.IsNullOrEmpty(passwordInput.text))
        {
            notifierText.text = "비밀번호를 확인해 주세요.";
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
                notifierText.text = "입력한 두개의 비밀번호가 서로 다릅니다.";
            }
        }
        else
        {
            notifierText.text = "비밀번호 확인이 안 되어 있습니다.";
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
            Debug.Log("ID 칸이 비어있음");
            return false;
        }
        if (string.IsNullOrEmpty(PasswordLogin.text))
        {
            Debug.Log("비밀번호 칸이 비어있음");
            return false;
        }

        string ID=PlayerPrefs.GetString(IDLogin.text + "/ID",null);
        if (ID == null)
        {
            Debug.Log("ID가 없음");
        }
        
        string password = PlayerPrefs.GetString(IDLogin.text + "/Password",null);
        if (password==null)
        {
            Debug.Log("비밀번호가 없음");
        }

        if (PasswordLogin.text == password)
        {
            GameManager.Instance.LoadData(ID);
            popupbank.GetUserData();
            GameManager.Instance.TogglePopups();
            Debug.Log("로그인 성공");
            return true;
        }
        else
        {
            Debug.Log("비밀번호가 다름");
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
