using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private Text moedasLevel;
    [SerializeField] private Button lojaBTN;

    // Use this for initialization
    void Start()
    {
        ScoreManager.instance.GameStartScoreManager();
        moedasLevel.text = PlayerPrefs.GetInt("moedasSave").ToString();
        lojaBTN.onClick.AddListener(AcessarLoja);
    }


    private void AcessarLoja()
    {
        SceneManager.LoadScene("Loja");
    }

}
