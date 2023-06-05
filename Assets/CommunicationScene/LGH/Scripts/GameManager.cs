using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public ServerManager SM;
    public UIManager UM;
    public PlayerManager PM;
    private void Awake()
    {
        GM = this;
        SM = GetComponent<ServerManager>();
        UM = GetComponent<UIManager>();
        PM = GetComponent<PlayerManager>();
    }
}
