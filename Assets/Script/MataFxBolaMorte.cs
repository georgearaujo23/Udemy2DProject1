using UnityEngine;
using System.Collections;

public class MataFxBolaMorte : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(MataFX());
    }

    IEnumerator MataFX()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
    }
}
