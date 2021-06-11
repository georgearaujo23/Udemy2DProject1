using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BolasShop : MonoBehaviour
{
    public static BolasShop instance;
    public List<Bolas> bolasList = new List<Bolas>();
    public List<GameObject> bolasSuporteList = new List<GameObject>();
    public GameObject baseBolaItem;
    public Transform conteudo;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        FillList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FillList()
    {
        foreach(Bolas b in bolasList)
        {
            b.comprou = PlayerPrefs.GetInt("comprouBola" + b.bolasId) == 1 ? true : false;
            b.usando = PlayerPrefs.GetInt("usaBola" + b.bolasId) == 1 ? true : false;

            GameObject itensBola = Instantiate(baseBolaItem) as GameObject;
            itensBola.transform.SetParent(conteudo, false);
            BolasSuporte item = itensBola.GetComponent<BolasSuporte>();
            item.bolaId = b.bolasId;
            item.bolaPreco.text = b.bolasPreco.ToString();
            item.bolaSprite.sprite = Resources.Load<Sprite>("Sprites/" + b.nomeSprite);
            item.btnCompra.GetComponent<CompraBola>().bolaId = b.bolasId;
            if (!b.comprou)
            {
                item.blockSprite.fillAmount = 1;
            }
            else
            {
                item.bolaPreco.text = "Comprado";
                if (b.usando)
                {
                    item.bolaPreco.text = "Usando";
                    item.btnCompra.SetActive(false);
                    OndeEstou.instance.bolaEmUso = b.bolasId;
                }
                else
                {
                    item.txtBtnCompra.text = "Usar";
                }
            }

            bolasSuporteList.Add(itensBola);

        }
    }

    public void UpdateSprite(int id, bool usando)
    {
        for(int i = 0; i < bolasSuporteList.Count; i++)
        {
            BolasSuporte bsuporte = bolasSuporteList[i].GetComponent<BolasSuporte>();

            if (bsuporte.bolaId == id)
            {
                if (usando)
                {
                    bsuporte.bolaPreco.text = "Usando";
                    bsuporte.btnCompra.SetActive(false);
                }
                else
                {
                    for (int j = 0; j < bolasList.Count; j++)
                    {
                        if (bolasList[j].bolasId == id)
                        {
                            if (bolasList[j].comprou)
                            {
                                bsuporte.blockSprite.fillAmount = 0;
                                bsuporte.bolaPreco.text = "Comprado";
                                SalvaBolasInf(id);
                                bsuporte.btnCompra.GetComponent<CompraBola>().txtBotao .text= "Usar";
                            }

                        }

                    }

                }

            }
            else if(usando)
            {
                if (!bsuporte.btnCompra.activeSelf)
                {
                    bsuporte.bolaPreco.text = "Comprado";
                    bsuporte.btnCompra.SetActive(true);
                    bsuporte.btnCompra.GetComponent<CompraBola>().txtBotao.text = "Usar";
                }
            }
            
        }
    }

    public void SalvaBolasInf(int idBola)
    {
        for(int i=0; i<bolasList.Count; i++)
        {
            if(bolasList[i].bolasId == idBola)
            {
                PlayerPrefs.SetInt("comprouBola" + idBola, bolasList[i].comprou ? 1 : 0);
                PlayerPrefs.SetInt("usaBola" + idBola, bolasList[i].usando ? 1 : 0);
            }
        }
    }

}
