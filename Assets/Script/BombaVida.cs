using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaVida : MonoBehaviour {

    private GameObject bombaRep;

	// Use this for initialization
	void Start () {
        bombaRep = GameObject.Find("BombaImg");
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine (Vida());	
	}

    IEnumerator Vida()
    {
        Destroy(bombaRep);
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }

}
