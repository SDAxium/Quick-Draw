using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using WebSocketSharp;
using Random = UnityEngine.Random;

public class UsernameManager : MonoBehaviour
{
    private TextMeshProUGUI _tmpText;
    public string inputText = "";
    public const string PlayerNamePrefKey = "Player Name";
    private void Start()
    {
        //Probably don't need to check if a TextMeshProUGUI exists anymore since it only exists on the Username object 
        if (gameObject.GetComponent<TextMeshProUGUI>() == null) return;
        _tmpText = gameObject.GetComponent<TextMeshProUGUI>();
        
        string defaultName = String.Empty;
        if (PlayerPrefs.HasKey(PlayerNamePrefKey))
        {
            defaultName = PlayerPrefs.GetString(PlayerNamePrefKey);
            _tmpText.text = defaultName;
        }

        PhotonNetwork.NickName = defaultName;
    }
    

    public void Append(string s)
    {
        inputText +=  s;
        _tmpText.text = inputText;
    }

    public void Backspace()
    {
        if (inputText.Length < 2)
        {
            inputText = " ";
        }
        inputText = inputText.Substring(0, inputText.Length - 1);
        _tmpText.text = inputText;
    }

    public void SetPlayerName()
    {
        string value = GameObject.Find("Username").GetComponent<TextMeshProUGUI>().text;
        if(string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            value = "Player " + Random.Range(0, 10);
            GameObject.Find("Username").GetComponent<TextMeshProUGUI>().text = value;
        }

        PhotonNetwork.NickName = value;
        
        PlayerPrefs.SetString(PlayerNamePrefKey, value);
    }

}
