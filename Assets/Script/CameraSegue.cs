using UnityEngine;
using System.Collections;

public class CameraSegue : MonoBehaviour
{

    [SerializeField] private Transform paredeE, paredeD, bola;
    private float t = 1;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            
            if(transform.position.x != paredeE.position.x)
            {
                t -= .06f * Time.deltaTime;
                transform.position = new Vector3(Mathf.SmoothStep(paredeE.position.x, Camera.main.transform.position.x, t ), transform.position.y , transform.position.z);
            }

            if (bola == null && GameManager.instance.ballsCounterInScene > 0)
            {
                bola = GameObject.Find("Bola(Clone)").GetComponent<Transform>();
            }else if(GameManager.instance.ballsCounterInScene > 0)
            {
                Vector3 posCam = transform.position;
                posCam.x = bola.position.x;
                posCam.x = Mathf.Clamp(posCam.x, paredeE.position.x, paredeD.position.x);
                transform.position = posCam; 
            }
        }
    }
}
