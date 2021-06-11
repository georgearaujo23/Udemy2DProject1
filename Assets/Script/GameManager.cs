using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]  private GameObject[] ball;
    public int ballsCount= 3;
    private bool ballIsDead = false;
    public int ballsCounterInScene = 0;
    private Transform initialPositionBall;
    public int shot = 1;
    public bool win;
    public bool isPlaying;
    private bool addsExibido = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        SceneManager.sceneLoaded += StartPosition;
        initialPositionBall = GameObject.Find("PosicaoBola").GetComponent<Transform>();

    }

    // Use this for initialization
    void Start()
    {
        ScoreManager.instance.GameStartScoreManager();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        CreateBalls();

        if(ballsCount <= 0 && !win)
        {
            GameOver();
        }

        if (win)
        {
            WinGame();
        }

    }

    void CreateBalls()
    {
        if (OndeEstou.instance.fase >= 4)
        {

            if (ballsCount > 0 && ballsCounterInScene == 0 && Camera.main.transform.position.x <= 0.05f)
            {
                Instantiate(ball[OndeEstou.instance.bolaEmUso], new Vector2(initialPositionBall.position.x, initialPositionBall.position.y), Quaternion.identity);
                ballsCounterInScene++;
                shot = 0;
            }
        }
        else
        {

            if (ballsCount > 0 && ballsCounterInScene == 0)
            {
                Instantiate(ball[OndeEstou.instance.bolaEmUso], new Vector2(initialPositionBall.position.x, initialPositionBall.position.y), Quaternion.identity);
                ballsCounterInScene++;
                shot = 0;
            }

        }
    }

    void StartPosition(Scene scene, LoadSceneMode mode)
    {
        if(OndeEstou.instance.fase != 6)
        {
            initialPositionBall = GameObject.Find("PosicaoBola").GetComponent<Transform>();
            StartGame();
        }
    }

    private void GameOver()
    {
        UIManager.instance.GameOverUI();
        isPlaying = false;
        if (!addsExibido)
        {
            AdsUnity.instance.ShowAdds();
            addsExibido = true;
        }

    }

    private void WinGame()
    {
        UIManager.instance.WinGameUI();
        isPlaying = false;
    }

    void StartGame()
    {
        isPlaying = true;
        ballsCount = 3;
        ballsCounterInScene = 0;
        win = false;
        addsExibido = false;
    }


}
