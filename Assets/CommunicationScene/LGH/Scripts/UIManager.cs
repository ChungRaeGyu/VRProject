using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject guiScreen;
    public GameObject memberScreen;

    public Button registerFormButton;
    public Button loginFormButton;
    public Button memberFormButton;

    public GameObject memberForm;
    public GameObject registerForm;
    public GameObject loginForm;

    public Button login_registerFormButton;
    public Button register_loginFormButton;

    public Button registerButton;
    public Button loginButton;

    public InputField registerIDIF;
    public InputField registerPwIF;
    public InputField registerPwCIF;
    public InputField registerNnIF;

    public InputField loginIDIF;
    public InputField loginPwIF;

    public Text nickname;

    public Button logoutButton;

    public Button gameStartButton;

    public Text registerIDText;
    public Text registerPwText;
    public Text registerPwCText;
    public Text registerNnText;

    public Text loginIDText;
    public Text loginPwText;

    public Text serverConnectText;
    public Button offlineButton;

    public Button registerIDCheckButton;
    public Text registerIDCheckText;
    public Button registerNnCheckButton;
    public Text registerNnCheckText;

    public GameObject playerScreen;
    public GameObject mainUI;

    public Button menuOptionButton;
    public Button menuExitButton;
    public Button menuCancleButton;

    public GameObject gameForm;
    public Button newGameButton;
    public Button loadGameButton;

    public GameObject newGameForm;
    public List<Button> newGameButtonL;
    public List<Text> newGameTextL;
    public Button nGF_GoToLoadGameFormButton;
    public Button nGF_GoToBackFormButton;
    public GameObject loadGameForm;
    public List<Button> loadGameButtonL;
    public List<Text> loadGameTextL;
    public Button lGF_GoToNewGameFormButton;
    public Button lGF_GoToBackFormButton;

    [System.Obsolete]
    void Start()
    {
        registerFormButton.onClick.AddListener(OnRegisterFormButtonAct);
        loginFormButton.onClick.AddListener(OnLoginFormButtonAct);
        memberFormButton.onClick.AddListener(OnMemberFormButtonAct);
        login_registerFormButton.onClick.AddListener(OnRegisterFormButtonAct);
        register_loginFormButton.onClick.AddListener(OnLoginFormButtonAct);

        registerButton.onClick.AddListener(OnRegisterButtonClicked);
        loginButton.onClick.AddListener(OnLoginButtonClicked);

        logoutButton.onClick.AddListener(OnLogoutButtonClicked);

        gameStartButton.onClick.AddListener(OnGameStartButtonAct);

        offlineButton.onClick.AddListener(OnGameStartButtonAct);

        registerIDCheckButton.onClick.AddListener(OnRegisterIDCheckButtonClicked);
        registerNnCheckButton.onClick.AddListener(OnRegisterNnCheckButtonClicked);

        menuOptionButton.onClick.AddListener(OnMenuOptionButtonAct);
        menuExitButton.onClick.AddListener(OnMenuExitButtonAct);
        menuCancleButton.onClick.AddListener(OnMenuCancleButtonAct);

        newGameButton.onClick.AddListener(OnNewGameButtonAct);
        loadGameButton.onClick.AddListener(OnLoadGameButtonAct);

        nGF_GoToLoadGameFormButton.onClick.AddListener(OnNGF_GoToLoadGameFormButtonAct);
        nGF_GoToBackFormButton.onClick.AddListener(OnNGF_GoToBackFormButtonAct);
        lGF_GoToNewGameFormButton.onClick.AddListener(OnLGF_GoToNewGameFormButtonAct);
        lGF_GoToBackFormButton.onClick.AddListener(OnLGF_GoToBackFormButtonAct);
    }

    /**
     * 내부 사용
     */

    void OnRegisterFormButtonAct()
    {
        memberFormButton.gameObject.SetActive(true);
        memberForm.SetActive(false);
        registerForm.SetActive(true);
        loginForm.SetActive(false);
    }

    void OnLoginFormButtonAct()
    {
        memberFormButton.gameObject.SetActive(true);
        memberForm.SetActive(false);
        registerForm.SetActive(false);
        loginForm.SetActive(true);
    }

    void OnMemberFormButtonAct()
    {
        memberFormButton.gameObject.SetActive(false);
        memberForm.SetActive(true);
        registerForm.SetActive(false);
        loginForm.SetActive(false);
    }

    void OnGameStartButtonAct()
    {
        SceneManager.LoadScene("Scenes/MapCustom");
    }

    void OnMenuOptionButtonAct()
    {

    }

    void OnMenuExitButtonAct()
    {
        GameManager.GM.PM.setPause(false);

        Application.Quit();
    }

    void OnMenuCancleButtonAct()
    {
        GameManager.GM.PM.setPause(false);

        GameManager.GM.PM.setPlayerScreen = false;

        playerScreen.SetActive(false);

        mainUI.SetActive(true);
    }

    void OnNewGameButtonAct()
    {
        gameForm.SetActive(false);
        newGameForm.SetActive(true);
    }

    void OnLoadGameButtonAct()
    {
        gameForm.SetActive(false);
        loadGameForm.SetActive(true);
    }

    void OnNGF_GoToLoadGameFormButtonAct()
    {
        newGameForm.SetActive(false);
        loadGameForm.SetActive(true);
    }

    void OnNGF_GoToBackFormButtonAct()
    {
        gameForm.SetActive(true);
        newGameForm.SetActive(false);
    }

    void OnLGF_GoToNewGameFormButtonAct()
    {
        newGameForm.SetActive(true);
        loadGameForm.SetActive(false);
    }

    void OnLGF_GoToBackFormButtonAct()
    {
        gameForm.SetActive(true);
        loadGameForm.SetActive(false);
    }

    /**
     * SM 으로 연동 하는 부분     
     */

    [System.Obsolete]
    void OnRegisterButtonClicked()
    {
        string id = registerIDIF.text;
        string pw = registerPwIF.text;
        string pwC = registerPwCIF.text;
        string nn = registerNnIF.text;

        GameManager.GM.SM.RegisterAct(id, pw, pwC, nn);
    }

    [System.Obsolete]
    void OnLoginButtonClicked()
    {
        string id = loginIDIF.text;
        string pw = loginPwIF.text;

        GameManager.GM.SM.LoginAct(id, pw);
    }

    [System.Obsolete]
    void OnLogoutButtonClicked()
    {
        GameManager.GM.SM.LogoutAct();
    }

    [System.Obsolete]
    void OnRegisterIDCheckButtonClicked()
    {
        string id = registerIDIF.text;

        GameManager.GM.SM.CheckIDAct(id);
    }

    [System.Obsolete]
    void OnRegisterNnCheckButtonClicked()
    {
        string nn = registerNnIF.text;

        GameManager.GM.SM.CheckNnAct(nn);
    }

    /**
     * SM 에게 연동 되는 부분     
     */

    public void ServerConnection(string sCT, bool network)
    {
        bool isOpen;
        isOpen = network;
        serverConnectText.text = sCT;

        if (isOpen == true)
        {
            // offlineButton.gameObject.SetActive(false);
        }
        else if (isOpen == false)
        {
            // offlineButton.gameObject.SetActive(true);
        }
    }

    public void RegisterSuccess()
    {
        OnLoginFormButtonAct();
    }

    public void LoginSuccess()
    {
        guiScreen.SetActive(false);
        memberScreen.SetActive(true);
    }

    public void MemberNickname(string nn)
    {
        nickname.text = nn;
    }

    public void LogoutSuccess()
    {
        guiScreen.SetActive(true);
        memberScreen.SetActive(false);

        loginIDIF.text = null;
        loginPwIF.text = null;
    }

    public void RegisterIDText(string rIT)
    {
        registerIDText.text = rIT;
    }

    public void RegisterPwText(string rPT)
    {
        registerPwText.text = rPT;
    }

    public void RegisterPwCText(string rPCT)
    {
        registerPwCText.text = rPCT;
    }
    public void RegisterNnText(string rNT)
    {
        registerNnText.text = rNT;
    }

    public void LoginIDText(string lIT)
    {
        loginIDText.text = lIT;
    }

    public void LoginPwText(string lPT)
    {
        loginPwText.text = lPT;
    }

    public void RegisterTextNull()
    {
        registerIDText.text = "";
        registerPwText.text = "";
        registerNnText.text = "";

        registerIDCheckText.text = "";
        registerNnCheckText.text = "";
    }

    public void RegisterConfirmTextNull()
    {
        registerPwCText.text = "";
    }

    public void LoginTextNull()
    {
        loginIDText.text = "";
        loginPwText.text = "";
    }

    public void CheckIDText(string idCheck)
    {
        registerIDCheckText.text = idCheck;
    }

    public void CheckNnText(string nnCheck)
    {
        registerNnCheckText.text = nnCheck;
    }

    /**
     * PM 에게 연동되는 부분
     */
     
    public void SwitchPlayerScreen(bool sPS)
    {
        playerScreen.SetActive(sPS);

        mainUI.SetActive(!sPS);
    }
}