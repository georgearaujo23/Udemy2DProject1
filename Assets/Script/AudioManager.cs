using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] clips;
    public AudioSource musicaBG;

    /*Efeitos*/
    public AudioClip[] clipsEfeitos;
    public AudioSource somEfeitos ;

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
    }

    private void Update()
    {
        if (!musicaBG.isPlaying)
        {
            musicaBG.clip = GetRandom();
            musicaBG.Play();
        }
    }

    public AudioClip GetRandom()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void SonEfeitoPlay(int index)
    {
        somEfeitos.clip = clipsEfeitos[index];
        somEfeitos.Play();
    }
}
