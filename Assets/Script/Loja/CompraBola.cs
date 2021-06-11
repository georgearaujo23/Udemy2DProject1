using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompraBola : MonoBehaviour
{

    public int bolaId;
    public Text txtBotao;
    private GameManager txtMoedas;
    private bool usou;

    public void ComprarBola()
    {
        usou = false;
        int idUsandoAnterior = -1;

        for (int i =0; i< BolasShop.instance.bolasList.Count; i++)
        {
            if (BolasShop.instance.bolasList[i].bolasId == bolaId && !BolasShop.instance.bolasList[i].comprou) 
            {
                if(PlayerPrefs.GetInt("moedasSave") >= BolasShop.instance.bolasList[i].bolasPreco)
                {
                    BolasShop.instance.bolasList[i].comprou = true;
                    BolasShop.instance.UpdateSprite(bolaId, false);
                    ScoreManager.instance.PerdeMoedas(BolasShop.instance.bolasList[i].bolasPreco);
                    GameObject.Find("PontosTxt").GetComponent<Text>().text = PlayerPrefs.GetInt("moedasSave").ToString();
                }
                else
                {
                    print("Falido");
                }
                

            }
            else if (BolasShop.instance.bolasList[i].bolasId == bolaId && BolasShop.instance.bolasList[i].comprou)
            {
                BolasShop.instance.bolasList[i].usando = true;
                usou = true;
                OndeEstou.instance.bolaEmUso = bolaId;
                BolasShop.instance.UpdateSprite(bolaId, true);
            }

            if (usou && BolasShop.instance.bolasList[i].bolasId != bolaId && BolasShop.instance.bolasList[i].comprou)
            {
                idUsandoAnterior = BolasShop.instance.bolasList[i].usando ? i : idUsandoAnterior;
                BolasShop.instance.bolasList[i].usando = false;
            }

            BolasShop.instance.SalvaBolasInf(BolasShop.instance.bolasList[i].bolasId);
        }

        if(idUsandoAnterior != -1)
        {
            BolasShop.instance.bolasList[idUsandoAnterior].usando = !usou;
            BolasShop.instance.SalvaBolasInf(BolasShop.instance.bolasList[idUsandoAnterior].bolasId);
        }
        

    }

}
