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
     * UM �� ��ȣ�ۿ� �ϴ� �κ�
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
            Debug.Log("���� ���� ����: " + www.error);
            connection = "���� ���� ����";
            isOnline = false;
        }
        else
        {
            Debug.Log("���� ���� ����!");
            connection = "���� ���� ����!";
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
            Debug.Log("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            pwCText = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
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
     * ���� �۵��ϴ� �κ�
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
            Debug.Log("���̵� ���� ���ڰ� ���ԵǾ� �ֽ��ϴ�.");
            idText = "���̵� ���� ���ڰ� ���ԵǾ� �ֽ��ϴ�.";
            GameManager.GM.UM.RegisterIDText(idText);
            yield break;
        }

        if (string.IsNullOrWhiteSpace(id))
        {
            Debug.Log("���̵� �Է��ϼ���.");
            idText = "���̵� �Է��ϼ���.";
            GameManager.GM.UM.RegisterIDText(idText);
            yield break;
        }

        if (id.Length < 4)
        {
            Debug.Log("���̵�� 4�ڸ� �̻��̾�� �մϴ�.");
            idText = "���̵�� 4�ڸ� �̻��̾�� �մϴ�.";
            GameManager.GM.UM.RegisterIDText(idText);
            yield break;
        }

        if (pw.Contains(" "))
        {
            Debug.Log("��й�ȣ�� ���� ���ڰ� ���ԵǾ� �ֽ��ϴ�.");
            pwText = "��й�ȣ�� ���� ���ڰ� ���ԵǾ� �ֽ��ϴ�.";
            GameManager.GM.UM.RegisterPwText(pwText);
            yield break;
        }

        if (string.IsNullOrWhiteSpace(pw))
        {
            Debug.Log("��й�ȣ�� �Է��ϼ���.");
            pwText = "��й�ȣ�� �Է��ϼ���.";
            GameManager.GM.UM.RegisterPwText(pwText);
            yield break;
        }

        if (pw.Length < 6)
        {
            Debug.Log("��й�ȣ�� 6�ڸ� �̻��̾�� �մϴ�.");
            pwText = "��й�ȣ�� 6�ڸ� �̻��̾�� �մϴ�.";
            GameManager.GM.UM.RegisterPwText(pwText);
            yield break;
        }

        if (!Regex.IsMatch(pw, @"\d"))
        {
            Debug.Log("��й�ȣ�� ���ڰ� ���ԵǾ�� �մϴ�.");
            pwText = "��й�ȣ�� ���ڰ� ���ԵǾ�� �մϴ�.";
            GameManager.GM.UM.RegisterPwText(pwText);
            yield break;
        }

        if (!Regex.IsMatch(pw, @"[!@#$%^&*()_+\-=\[\]{};':\\|,.<>\/?]"))
        {
            Debug.Log("��й�ȣ�� Ư�����ڸ� �ϳ� �̻� �־�� �մϴ�.");
            pwText = "��й�ȣ�� Ư�����ڸ� �ϳ� �̻� �־�� �մϴ�.";
            GameManager.GM.UM.RegisterPwText(pwText);
            yield break;
        }

        if (nn.Contains(" "))
        {
            Debug.Log("�г��ӿ� ���� ���ڰ� ���ԵǾ� �ֽ��ϴ�.");
            nnText = "�г��ӿ� ���� ���ڰ� ���ԵǾ� �ֽ��ϴ�.";
            GameManager.GM.UM.RegisterNnText(nnText);
            yield break;
        }

        if (string.IsNullOrWhiteSpace(nn))
        {
            Debug.Log("�г����� �Է��ϼ���.");
            nnText = "�г����� �Է��ϼ���.";
            GameManager.GM.UM.RegisterNnText(nnText);
            yield break;
        }

        if (nn.Length > 16)
        {
            Debug.Log("�г����� �ִ� 16�ڸ����� �����մϴ�.");
            nnText = "�г����� �ִ� 16�ڸ����� �����մϴ�.";
            GameManager.GM.UM.RegisterNnText(nnText);
            yield break;
        }

        if (Regex.IsMatch(nn, @"[!@#$%^&*()_+\-=\[\]{};':\\|,.<>\/?]"))
        {
            Debug.Log("�г��ӿ��� Ư������ ����� �Ұ����մϴ�.");
            nnText = "�г��ӿ��� Ư������ ����� �Ұ����մϴ�.";
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
                if (www.downloadHandler.text == "�̹� �����ϴ� ID�Դϴ�.")
                {
                    Debug.Log("ID�� �̹� �����մϴ�.");

                    string checkID;
                    checkID = "ID�� �̹� �����մϴ�.";
                    GameManager.GM.UM.CheckIDText(checkID);
                }
                else if (www.downloadHandler.text == "�̹� �����ϴ� �г����Դϴ�.")
                {
                    Debug.Log("�г����� �̹� �����մϴ�.");

                    string checkNn;
                    checkNn = "�г����� �̹� �����մϴ�.";
                    GameManager.GM.UM.CheckNnText(checkNn);
                }
                else
                {
                    Debug.Log("ȸ������ ����!");

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
            Debug.Log("���̵� �Է��ϼ���.");
            idText = "���̵� �Է��ϼ���.";
            GameManager.GM.UM.LoginIDText(idText);
            yield break;
        }

        if (id.Length < 4)
        {
            Debug.Log("���̵�� 4�ڸ� �̻��̾�� �մϴ�.");
            idText = "���̵�� 4�ڸ� �̻��̾�� �մϴ�.";
            GameManager.GM.UM.LoginIDText(idText);
            yield break;
        }

        if (string.IsNullOrWhiteSpace(pw))
        {
            Debug.Log("��й�ȣ�� �Է��ϼ���.");
            pwText = "��й�ȣ�� �Է��ϼ���.";
            GameManager.GM.UM.LoginPwText(pwText);
            yield break;
        }

        if (pw.Length < 6)
        {
            Debug.Log("��й�ȣ�� 6�ڸ� �̻��̾�� �մϴ�.");
            pwText = "��й�ȣ�� 6�ڸ� �̻��̾�� �մϴ�.";
            GameManager.GM.UM.LoginPwText(pwText);
            yield break;
        }

        if (!Regex.IsMatch(pw, @"\d"))
        {
            Debug.Log("��й�ȣ�� ���ڰ� ���ԵǾ�� �մϴ�.");
            pwText = "��й�ȣ�� ���ڰ� ���ԵǾ�� �մϴ�.";
            GameManager.GM.UM.LoginPwText(pwText);
            yield break;
        }

        if (!Regex.IsMatch(pw, @"[!@#$%^&*()_+\-=\[\]{};':\\|,.<>\/?]"))
        {
            Debug.Log("��й�ȣ�� Ư�����ڸ� �ϳ� �̻� �־�� �մϴ�.");
            pwText = "��й�ȣ�� Ư�����ڸ� �ϳ� �̻� �־�� �մϴ�.";
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
                    Debug.Log("�α��� ����!");
                    Debug.Log("����� ��ȣ: " + response.num);

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
                    Debug.Log("�α׾ƿ� ����!");

                    playerNum = -1;

                    GameManager.GM.UM.LogoutSuccess();
                }
                else
                {
                    Debug.Log("�α׾ƿ� ����!");
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

                if (www.downloadHandler.text == "�̹� �����ϴ� ID�Դϴ�.")
                {
                    Debug.Log("ID�� �̹� �����մϴ�.");

                    checkID = "ID�� �̹� �����մϴ�.";
                }
                else
                {
                    Debug.Log("��� ������ ID�Դϴ�.");

                    checkID = "��� ������ ID�Դϴ�.";
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

                if (www.downloadHandler.text == "�̹� �����ϴ� �г����Դϴ�.")
                {
                    Debug.Log("�г����� �̹� �����մϴ�.");

                    checkNn = "�г����� �̹� �����մϴ�.";
                }
                else
                {
                    Debug.Log("��� ������ �г����Դϴ�.");

                    checkNn = "��� ������ �г����Դϴ�.";
                }

                GameManager.GM.UM.CheckNnText(checkNn);
            }
        }
    }

    /**
     * ���� ���
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
                Debug.Log("�г���: " + response.nick);

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