using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour
{
    public Button tryAgainButton;

    private void Start()
    {
        tryAgainButton.onClick.AddListener(OnTryAgainButtonAct);
    }

    void OnTryAgainButtonAct()
    {
        SceneManager.LoadScene("Scenes/MapCustom");
    }
}
