using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class OndeEstou : MonoBehaviour
{

    public int fase = -1;
    public int bolaEmUso = 1;
    [SerializeField]
    private GameObject UIManagerGO, GameManagerGO;

    public static OndeEstou instance;

    private float orthoSize = 5;
    [SerializeField] private float aspect = 1.8f;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(instance.gameObject);
        }
        //PlayerPrefs.DeleteAll();
        SceneManager.sceneLoaded += VerificaFase;

    }

    void VerificaFase(Scene scene, LoadSceneMode mode)
    {
        fase = SceneManager.GetActiveScene().buildIndex;
        if (fase != 6 && fase != 5 && fase != 0)
        {
            Instantiate(UIManagerGO);
            Instantiate(GameManagerGO);
            Camera.main.projectionMatrix = Matrix4x4.Ortho(-orthoSize * aspect, orthoSize * aspect, -orthoSize, orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
        }
        
    }


}
