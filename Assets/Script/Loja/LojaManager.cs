using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LojaManager : MonoBehaviour
{
    public static LojaManager instance;
    [SerializeField] public Button btnMenu;
    [SerializeField] private Text moedasLevel;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        btnMenu.onClick.AddListener(AcessarMenu);
        ScoreManager.instance.UpdateScore();
        moedasLevel.text = PlayerPrefs.GetInt("moedasSave").ToString();
    }

    private void AcessarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

