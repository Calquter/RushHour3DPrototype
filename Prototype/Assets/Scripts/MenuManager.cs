using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _goldTxt;

    private void Start()
    {
        _goldTxt.text = "$" + PlayerPrefs.GetInt("MyGold");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.DeleteAll();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("MyCurrentLevel"));
    }
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        _goldTxt.text = "$" + PlayerPrefs.GetInt("MyGold");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
