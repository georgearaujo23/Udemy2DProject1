using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaManager : MonoBehaviour {

    [SerializeField] private GameObject bombaFx;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D objCollision)
    {
        if (objCollision.gameObject.CompareTag("Bola"))
        {
            Instantiate(bombaFx, new Vector2(this.transform.position.x, this.transform.position.y   ), Quaternion.identity);
        }
    }

}
