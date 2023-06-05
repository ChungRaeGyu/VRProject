using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;

public class ServerManager : MonoBehaviour
{
    public int playerNum;

    [Obsolete]
    void OpenClient()
    {
        StartCoroutine(CheckServerConnection());
    }

    [Obsolete]
    private void Awake()
    {
        OpenClient();

        DontDestroyOnLoad(this.gameObject);
    }

    /**
     * UM 과 상호작용 하는 부분
     */

    [Obsolete]
    IEnumerator CheckServerConnection()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://43.201.175.73:3000/checkServerConnection");
        yield return www.SendWebRequest();

        string connection;
        bool isOnline;

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("서버 연결 실패: " + www.error);
            connection = "서버 연결 실패";
            isOnline = false;
        }
        else
        {
            Debug.Log("서버 연결 성공!");
            connection = "서버 연결 성공!";
            isOnline = true;
        }
        GameManager.GM.UM.ServerConnection(connection, isOnline);
    }

    [Obsolete]
    public void RegisterAct(string id, string pw, string pwC, string nn)
    {
        GameManager.GM.UM.RegisterConfirmTextNull();

        string pwCText;

        if (pw != pwC)
        {
            Debug.Log("비밀번호가 일치하지 않습니다.");
            pwCText = "비밀번호가 일치하지 않습니다.";
            GameManager.GM.UM.RegisterPwCText(pwCText);
            return;
        }

        GameManager.GM.UM.RegisterConfirmTextNull();

        StartCoroutine(Register(id, pw, nn));
    }

    [Obsolete]
    public void LoginAct(string id, string pw)
    {
        StartCoroutine(Login(id, pw));
    }

    [Obsolete]
    public void LogoutAct()
    {
        StartCoroutine(Logout());
    }

    [Obsolete]
    public void CheckIDAct(string id)
    {
        StartCoroutine(CheckId(id));
    }

    [Obsolete]
    public void CheckNnAct(string nn)
    {
        StartCoroutine(CheckNick(nn));
    }

    /**
     * 실제 작동하는 부분
     */

    [Obsolete]
    IEnumerator Register(string id, string pw, string nn)
    {
        GameManager.GM.UM.RegisterTextNull();

        id = id.Trim();
        pw = pw.Trim();
        nn = nn.Trim();

        string idText;
        string pwText;
        string nnText;

        if (id.Contains(" "))
        {
            Debug.Log("아이디에 공백 문자가 포함되어 있습니다.");
            idText = "아이디에 공백 문자가 포함되어 있습니다.";
            GameManager.GM.UM.RegisterIDText(idText);
            yield break;
        }

        if (string.IsNullOrWhiteSpace(id))
        {
            Debug.Log("아이디를 입력하세요.");
            idText = "아이디를 입력하세요.";
            GameManager.GM.UM.RegisterIDText(idText);
            yield break;
        }

        if (id.Length < 4)
        {
            Debug.Log("아이디는 4자리 이상이어야 합니다.");
            idText = "아이디는 4자리 이상이어야 합니다.";
            GameManager.GM.UM.RegisterIDText(idText);
            yield break;
        }

        if (pw.Contains(" "))
        {
            Debug.Log("비밀번호에 공백 문자가 포함되어 있습니다.");
            pwText = "비밀번호에 공백 문자가 포함되어 있습니다.";
            GameManager.GM.UM.RegisterPwText(pwText);
            yield break;
        }

        if (string.IsNullOrWhiteSpace(pw))
        {
            Debug.Log("비밀번호를 입력하세요.");
            pwText = "비밀번호를 입력하세요.";
            GameManager.GM.UM.RegisterPwText(pwText);
            yield break;
        }

        if (pw.Length < 6)
        {
            Debug.Log("비밀번호는 6자리 이상이어야 합니다.");
            pwText = "비밀번호는 6자리 이상이어야 합니다.";
            GameManager.GM.UM.RegisterPwText(pwText);
            yield break;
        }

        if (!Regex.IsMatch(pw, @"\d"))
        {
            Debug.Log("비밀번호에 숫자가 포함되어야 합니다.");
            pwText = "비밀번호에 숫자가 포함되어야 합니다.";
            GameManager.GM.UM.RegisterPwText(pwText);
            yield break;
        }

        if (!Regex.IsMatch(pw, @"[!@#$%^&*()_+\-=\[\]{};':\\|,.<>\/?]"))
        {
            Debug.Log("비밀번호에 특수문자를 하나 이상 넣어야 합니다.");
            pwText = "비밀번호에 특수문자를 하나 이상 넣어야 합니다.";
            GameManager.GM.UM.RegisterPwText(pwText);
            yield break;
        }

        if (nn.Contains(" "))
        {
            Debug.Log("닉네임에 공백 문자가 포함되어 있습니다.");
            nnText = "닉네임에 공백 문자가 포함되어 있습니다.";
            GameManager.GM.UM.RegisterNnText(nnText);
            yield break;
        }

        if (string.IsNullOrWhiteSpace(nn))
        {
            Debug.Log("닉네임을 입력하세요.");
            nnText = "닉네임을 입력하세요.";
            GameManager.GM.UM.RegisterNnText(nnText);
            yield break;
        }

        if (nn.Length > 16)
        {
            Debug.Log("닉네임은 최대 16자리까지 가능합니다.");
            nnText = "닉네임은 최대 16자리까지 가능합니다.";
            GameManager.GM.UM.RegisterNnText(nnText);
            yield break;
        }

        if (Regex.IsMatch(nn, @"[!@#$%^&*()_+\-=\[\]{};':\\|,.<>\/?]"))
        {
            Debug.Log("닉네임에는 특수문자 사용이 불가능합니다.");
            nnText = "닉네임에는 특수문자 사용이 불가능합니다.";
            GameManager.GM.UM.RegisterNnText(nnText);
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("pw", pw);
        form.AddField("nick", nn);

        using (UnityWebRequest www = UnityWebRequest.Post("http://43.201.175.73:3000/register", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text == "이미 존재하는 ID입니다.")
                {
                    Debug.Log("ID가 이미 존재합니다.");

                    string checkID;
                    checkID = "ID가 이미 존재합니다.";
                    GameManager.GM.UM.CheckIDText(checkID);
                }
                else if (www.downloadHandler.text == "이미 존재하는 닉네임입니다.")
                {
                    Debug.Log("닉네임이 이미 존재합니다.");

                    string checkNn;
                    checkNn = "닉네임이 이미 존재합니다.";
                    GameManager.GM.UM.CheckNnText(checkNn);
                }
                else
                {
                    Debug.Log("회원가입 성공!");

                    GameManager.GM.UM.RegisterSuccess();

                    GameManager.GM.UM.RegisterTextNull();
                }
            }
        }
    }

    [Obsolete]
    IEnumerator Login(string id, string pw)
    {
        GameManager.GM.UM.LoginTextNull();

        id = id.Trim();
        pw = pw.Trim();

        string idText;
        string pwText;

        if (string.IsNullOrWhiteSpace(id))
        {
            Debug.Log("아이디를 입력하세요.");
            idText = "아이디를 입력하세요.";
            GameManager.GM.UM.LoginIDText(idText);
            yield break;
        }

        if (id.Length < 4)
        {
            Debug.Log("아이디는 4자리 이상이어야 합니다.");
            idText = "아이디는 4자리 이상이어야 합니다.";
            GameManager.GM.UM.LoginIDText(idText);
            yield break;
        }

        if (string.IsNullOrWhiteSpace(pw))
        {
            Debug.Log("비밀번호를 입력하세요.");
            pwText = "비밀번호를 입력하세요.";
            GameManager.GM.UM.LoginPwText(pwText);
            yield break;
        }

        if (pw.Length < 6)
        {
            Debug.Log("비밀번호는 6자리 이상이어야 합니다.");
            pwText = "비밀번호는 6자리 이상이어야 합니다.";
            GameManager.GM.UM.LoginPwText(pwText);
            yield break;
        }

        if (!Regex.IsMatch(pw, @"\d"))
        {
            Debug.Log("비밀번호에 숫자가 포함되어야 합니다.");
            pwText = "비밀번호에 숫자가 포함되어야 합니다.";
            GameManager.GM.UM.LoginPwText(pwText);
            yield break;
        }

        if (!Regex.IsMatch(pw, @"[!@#$%^&*()_+\-=\[\]{};':\\|,.<>\/?]"))
        {
            Debug.Log("비밀번호에 특수문자를 하나 이상 넣어야 합니다.");
            pwText = "비밀번호에 특수문자를 하나 이상 넣어야 합니다.";
            GameManager.GM.UM.LoginPwText(pwText);
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("pw", pw);

        using (UnityWebRequest www = UnityWebRequest.Post("http://43.201.175.73:3000/login", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var response = JsonUtility.FromJson<LoginResponse>(www.downloadHandler.text);
                if (response.success)
                {
                    Debug.Log("로그인 성공!");
                    Debug.Log("사용자 번호: " + response.num);

                    playerNum = response.num;

                    GameManager.GM.UM.LoginTextNull();

                    GameManager.GM.UM.LoginSuccess();

                    StartCoroutine(GetNickname());
                }
                else
                {
                    Debug.Log(response.message);
                }
            }
        }
    }

    [Obsolete]
    IEnumerator Logout()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://43.201.175.73:3000/logout"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var response = JsonUtility.FromJson<LogoutResponse>(www.downloadHandler.text);
                if (response.success)
                {
                    Debug.Log("로그아웃 성공!");

                    playerNum = -1;

                    GameManager.GM.UM.LogoutSuccess();
                }
                else
                {
                    Debug.Log("로그아웃 실패!");
                }
            }
        }
    }

    [Obsolete]
    IEnumerator CheckId(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);

        using (UnityWebRequest www = UnityWebRequest.Post("http://43.201.175.73:3000/checkId", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                string checkID;

                if (www.downloadHandler.text == "이미 존재하는 ID입니다.")
                {
                    Debug.Log("ID가 이미 존재합니다.");

                    checkID = "ID가 이미 존재합니다.";
                }
                else
                {
                    Debug.Log("사용 가능한 ID입니다.");

                    checkID = "사용 가능한 ID입니다.";
                }

                GameManager.GM.UM.CheckIDText(checkID);
            }
        }
    }

    [Obsolete]
    IEnumerator CheckNick(string nick)
    {
        WWWForm form = new WWWForm();
        form.AddField("nick", nick);

        using (UnityWebRequest www = UnityWebRequest.Post("http://43.201.175.73:3000/checkNick", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                string checkNn;

                if (www.downloadHandler.text == "이미 존재하는 닉네임입니다.")
                {
                    Debug.Log("닉네임이 이미 존재합니다.");

                    checkNn = "닉네임이 이미 존재합니다.";
                }
                else
                {
                    Debug.Log("사용 가능한 닉네임입니다.");

                    checkNn = "사용 가능한 닉네임입니다.";
                }

                GameManager.GM.UM.CheckNnText(checkNn);
            }
        }
    }

    /**
     * 내부 사용
     */

    [Obsolete]
    IEnumerator GetNickname()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://43.201.175.73:3000/nick"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var response = JsonUtility.FromJson<NicknameResponse>(www.downloadHandler.text);
                Debug.Log("닉네임: " + response.nick);

                GameManager.GM.UM.MemberNickname(response.nick);
            }
        }
    }
}

[Serializable]
public class LoginResponse
{
    public bool success;
    public int num;
    public string message;
}

[Serializable]
public class NicknameResponse
{
    public string nick;
}

[Serializable]
public class LogoutResponse
{
    public bool success;
}