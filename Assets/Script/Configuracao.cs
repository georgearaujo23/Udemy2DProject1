using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Configuracao : MonoBehaviour
{

    [SerializeField] private Image barraConfig, imgInfo;
    private Animator animConfig, animInfo;
    private bool sobe = true;
    private bool infoUp = true;
    private AudioSource musica;
    public Sprite somLigado, somDesligado;
    [SerializeField] private Button btnSom;

    private void Start()
    {
        animConfig = barraConfig.GetComponent<Animator>() as Animator;
        animInfo = imgInfo.GetComponent<Animator>() as Animator;
        musica = AudioManager.instance.musicaBG;
    }


    public void redeSocial()
    {
        Application.OpenURL("www.facebook.com/wtncursos/");
    }

    public void ConfigSom()
    {
        musica.mute = !musica.mute;

        if (musica.mute)
        {
            btnSom.image.sprite = somDesligado;
        }
        else
        {
            btnSom.image.sprite = somLigado;
        }

    }

    public void jogar()
    {
        SceneManager.LoadScene(6);
    }

    public void AnimaMenu()
    {
        
        if (sobe)
        {
            animConfig.Play("Exibe");
            sobe = false;
        }
        else
        {
            animConfig.Play("Recolhe");
            sobe = true;
        }
        
    }

    public void AnimaInformacoes()
    {
        if (infoUp)
        {
            animInfo.Play("InfoUp");
            infoUp = false;
        }
        else
        {
            animInfo.Play("InfoDown");
            infoUp = true;
        }
    }

}
