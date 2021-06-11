using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [System.Serializable]
    public class Level
    {
        public string levelText;
        public bool habilitado;
        public int desbloqueado;
    }

    public GameObject botao;
    public Transform localBtn;
    public List<Level> levelList;

    private void Awake()
    {
        Destroy(GameObject.Find("UIManager(Clone)"));
        Destroy(GameObject.Find("GameManager(Clone)"));
    }

    // Use this for initialization
    void Start () {
        FasesAdd();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    void FasesAdd()
    {
        foreach(Level level in levelList)
        {
            GameObject btnNovo = Instantiate(botao) as GameObject;

            BotaoLevel btnNew = btnNovo.GetComponent<BotaoLevel>();
            btnNew.levelTxtBtn.text = level.levelText;
            if (PlayerPrefs.GetInt("Level" + btnNew.levelTxtBtn.text) == 1)
            {
                level.desbloqueado = 1;
                level.habilitado = true;
            }

            btnNew.desbloqueadoBtn = level.desbloqueado;
            btnNew.GetComponent<Button>().interactable = level.habilitado;
            btnNew.GetComponentInChildren<Text>().enabled = level.habilitado;   
            btnNew.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level" +level.levelText));

            btnNovo.transform.SetParent(localBtn, false);
        }
    }

}
