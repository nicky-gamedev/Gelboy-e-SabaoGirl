﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePlayerScript : MonoBehaviour
{
    public GameObject _playerInterface;

    public void ChooseGelman()
    {
        PlayerPrefs.SetInt("player", 1);
    }

    public void ChooseSabaogirl()
    {
        PlayerPrefs.SetInt("player", 2);
    }

    public void GoToMapScene()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenClosePlayerInterface()
    {
        _playerInterface.SetActive(!_playerInterface.activeInHierarchy);
    }
}
