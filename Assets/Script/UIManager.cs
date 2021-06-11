using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    private Text pontosUI, bolasUI;
    private GameObject losePanel, winPanel, pausePanel;
    private bool pasado = false;
    private Button pauseBtn, playBtn;
    private Button menuLoseBtn, replayLoseBtn;
    private Button menuWinBtn, replayWinBtn, nextLevelBtn;
    private int coinsCountInit, coinsCountEnd;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        SceneManager.sceneLoaded += CarregaPontos;
        PegaDados();
    }

    public void StartUI()
    {

    }

    void CarregaPontos(Scene cena, LoadSceneMode modo)
    {
        PegaDados();
    }

    void PegaDados()
    {
        if (OndeEstou.instance.fase != 6)
        {
            pontosUI = GameObject.Find("PontosTxt").GetComponent<Text>();
            bolasUI = GameObject.Find("BolasTxt").GetComponent<Text>();
            losePanel = GameObject.Find("LosePanel");
            winPanel = GameObject.Find("WinPanel");
            pausePanel = GameObject.Find("PausePanel");
            pauseBtn = GameObject.Find("PauseBtn").GetComponent<Button>();
            playBtn = GameObject.Find("PlayBtn").GetComponent<Button>();
            replayLoseBtn = GameObject.Find("JogarLose").GetComponent<Button>();
            menuLoseBtn = GameObject.Find("MenuLose").GetComponent<Button>();
            replayWinBtn = GameObject.Find("JogarWin").GetComponent<Button>();
            menuWinBtn = GameObject.Find("MenuWin").GetComponent<Button>();
            nextLevelBtn = GameObject.Find("Proximo").GetComponent<Button>();

            playBtn.onClick.AddListener(Play);
            pauseBtn.onClick.AddListener(Pause);
            replayLoseBtn.onClick.AddListener(Replay);
            menuLoseBtn.onClick.AddListener(Menu);
            replayWinBtn.onClick.AddListener(Replay);
            menuWinBtn.onClick.AddListener(Menu);
            nextLevelBtn.onClick.AddListener(NextLevel);

            coinsCountInit = PlayerPrefs.GetInt("moedasSave");
        }
    }
    
    public void UpdateUI()
    {
        pontosUI.text = ScoreManager.instance.moedas.ToString();
        bolasUI.text = GameManager.instance.ballsCount.ToString();
        coinsCountEnd = ScoreManager.instance.moedas;
    }

    public void GameOverUI()
    {
        losePanel.GetComponent<Animator>().enabled = true;
    }

    public void WinGameUI()
    {
        winPanel.GetComponent<Animator>().enabled = true;
    }

    private void Pause()
    {   
        pausePanel.GetComponent<Animator>().enabled = true;
        pausePanel.GetComponent<Animator>().Play("Pause");
        Time.timeScale = 0;
    }

    private void Play()
    {
        pausePanel.GetComponent<Animator>().Play("Play");
        StartCoroutine(SetPauseActive());
        Time.timeScale = 1;
    }

    private void Menu()
    {
        if (!GameManager.instance.win)
        {
            SceneManager.LoadScene("Menu");
            ScoreManager.instance.PerdeMoedas(coinsCountEnd - coinsCountInit);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private void Replay()
    {
        if (!GameManager.instance.win)
        {
            SceneManager.LoadScene("Level" + OndeEstou.instance.fase);
            ScoreManager.instance.PerdeMoedas(coinsCountEnd - coinsCountInit);
        }
        else
        {
            SceneManager.LoadScene("Level" + OndeEstou.instance.fase);
        }
    }

    IEnumerator SetPauseActive()
    {
        yield return new WaitForSeconds(0.8f);
        pausePanel.GetComponent<Animator>().enabled = false;
    }

    void NextLevel()
    {
        if (GameManager.instance.win)
        {
            int temp = OndeEstou.instance.fase + 1;
            SceneManager.LoadScene("Level" + temp);
        }
    }

}


