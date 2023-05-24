using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keyboard : MonoBehaviour
{
    // public TMP_InputField inputField;
    public InputField inputField; // inputFiled 라는 것이 내가 클릭하는 것으로 바껴야함
    public GameObject normalButtons;
    public GameObject capsButtons;
    private bool caps;

    public GameObject normalNumbers;
    public GameObject shiftNumbers;

    void Start()
    {
        caps = false;
    }

    public void InsertChar(string s)
    {
        inputField.text += s;
    }

    public void DeleteChar()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void InsertSpace()
    {
        inputField.text += " ";
    }

    public void TruncateText()
    {
        inputField.text = null;
    }

    public void CapsPressed()
    {
        if (!caps)
        {
            normalButtons.SetActive(false);
            capsButtons.SetActive(true);
            caps = true;

            normalNumbers.SetActive(false);
            shiftNumbers.SetActive(true);
        }
        else
        {
            capsButtons.SetActive(false);
            normalButtons.SetActive(true);
            caps = false;

            shiftNumbers.SetActive(false);
            normalNumbers.SetActive(true);
        }
    }
}
