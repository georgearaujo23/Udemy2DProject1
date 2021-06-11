using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspetoManager : MonoBehaviour {

    private SliderJoint2D espeto;
    private JointMotor2D auxiliar;



	// Use this for initialization
	void Start () {
        espeto = GetComponent<SliderJoint2D>();
        auxiliar = espeto.motor;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(espeto.limitState == JointLimitState2D.UpperLimit)
        {
            auxiliar.motorSpeed = Random.Range(-1, -5);
            espeto.motor = auxiliar;
        }

        if (espeto.limitState == JointLimitState2D.LowerLimit)
        {
            auxiliar.motorSpeed = Random.Range(1, 5);
            espeto.motor = auxiliar;
        }
        

    }
}
