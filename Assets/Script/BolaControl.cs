using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolaControl : MonoBehaviour
{
    /*ROTACAO BOLA*/
    private GameObject setaGO;
    public bool liberaRotacao = false;
    public bool liberaTiro = false;
    public float zAngulo;
    private bool bolaAtirada = false;
    /*FORÇA BOLA*/
    private Rigidbody2D bola;
    private float force = 1000f;
    private GameObject setaAtv;

    /*mote bola anim*/
    [SerializeField] private GameObject morteBolaAnim;

    /*Paredes*/
    private Transform paredeLD;
    private Transform paredeLE;

    private void Awake()
    {
        setaGO = GameObject.Find("Seta");
        setaAtv = setaGO.transform.GetChild(0).gameObject;
        setaGO.GetComponent<Image>().enabled = false;
        setaAtv.GetComponent<Image>().enabled = false;
        paredeLD = GameObject.Find("ParedeLDLevel").GetComponent<Transform>();
        paredeLE = GameObject.Find("ParedeLELevel").GetComponent<Transform>();
    }

    // Use this for initialization
    void Start()
    {
        /*FORÇA*/   
        bola = this.gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        PosicionaSeta();
        InputRotacao();
        LimiteRotacao();
        RotacaoSeta();

        /*força*/
        ControlaForca();
        AplicaForca();

        /*morte*/
        Paredes();

        if(bola.velocity.magnitude <= 0.2f && bolaAtirada)
        {
            StartCoroutine(VidaBola());
        }

    }

    private void OnMouseDown()
    {
        if(GameManager.instance.shot == 0)
        {
            liberaRotacao = true;
            setaGO.GetComponent<Image>().enabled = true;
            setaAtv.GetComponent<Image>().enabled = true;
        }
        
    }

    private void OnMouseUp()
    {
        liberaRotacao = false;
        setaGO.GetComponent<Image>().enabled = false;
        setaAtv.GetComponent<Image>().enabled = false;
        
        if (GameManager.instance.shot == 0 && force > 0.01)
        {
            liberaTiro = true;
            setaAtv.GetComponent<Image>().fillAmount = 0;
            GameManager.instance.shot = 1;
        }

    }

    void PosicionaSeta()
    {
        setaGO.GetComponent<Image>().rectTransform.position = transform.position;
    }

    void RotacaoSeta()
    {
        setaGO.GetComponent<Image>().transform.eulerAngles = new Vector3(0, 0, zAngulo);
    }

    void InputRotacao()
    {
        if (liberaRotacao)
        {
            float moveX = Input.GetAxis("Mouse X");
            float moveY = Input.GetAxis("Mouse Y");

            if (moveY > 0 && zAngulo < 90)
            {
                zAngulo += 2.5f;
            }

            if (moveY < 0 && zAngulo > 0)
            {
                zAngulo -= 2.5f;
            }

        }
    }

    void LimiteRotacao()
    {
        if (zAngulo >= 90)
        {
            zAngulo = 90;
        }

        if (zAngulo <= 0)
        {
            zAngulo = 0;
        }

    }

    /*força*/
    void AplicaForca()
    {
        float x = force * Mathf.Cos(zAngulo * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zAngulo * Mathf.Deg2Rad);

        if (liberaTiro)
        {
            bola.AddForce(new Vector2(x, y));
            liberaTiro = false;
            AudioManager.instance.SonEfeitoPlay(0);
            StartCoroutine(BolaLiberada());
        }
    }

    void ControlaForca()
    {
        if (liberaRotacao)
        {
            float mouseX = Input.GetAxis("Mouse X");

            if (mouseX < 0)
            {
                setaAtv.GetComponent<Image>().fillAmount += 0.8f * Time.deltaTime;
                force = setaAtv.GetComponent<Image>().fillAmount * 1200f;
            }

            if (mouseX > 0)
            {
                setaAtv.GetComponent<Image>().fillAmount -= 0.8f * Time.deltaTime;
                force = setaAtv.GetComponent<Image>().fillAmount * 1200f;
            }
        }
    }

    void BolaDinamica()
    {
        bola = this.gameObject.GetComponent<Rigidbody2D>();
        bola.isKinematic = false;
    }

    void Paredes()
    {
        if (this.gameObject.transform.position.x > paredeLD.position.x || this.gameObject.transform.position.x < paredeLE.position.x)
        {
            Destroy(this.gameObject);
            GameManager.instance.ballsCounterInScene--;
            GameManager.instance.ballsCount--;
        }
    }

    private void OnTriggerEnter2D(Collider2D objCollision)
    {
        if (objCollision.gameObject.CompareTag("Morte"))
        {
            Instantiate(morteBolaAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            GameManager.instance.ballsCounterInScene--;
            GameManager.instance.ballsCount--;
        }

        if (objCollision.gameObject.CompareTag("Gol")){
            GameManager.instance.win = true;
            PlayerPrefs.SetInt("Level" + (OndeEstou.instance.fase + 1), 1);
        }

    }

    IEnumerator VidaBola()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(morteBolaAnim, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        GameManager.instance.ballsCounterInScene--;
        GameManager.instance.ballsCount--;
    }

    IEnumerator BolaLiberada()
    {
        yield return new WaitForSeconds(0.5f);
        bolaAtirada = true;
    }

}
