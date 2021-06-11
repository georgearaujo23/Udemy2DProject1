using UnityEngine;
using System.Collections;

public class MoedasControl : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D objCollision)
    {
        if (objCollision.gameObject.CompareTag("Bola"))
        {
            ScoreManager.instance.ColetaMoedas(10);
            AudioManager.instance.SonEfeitoPlay(1);
            Destroy(this.gameObject);
        }
    }
}
