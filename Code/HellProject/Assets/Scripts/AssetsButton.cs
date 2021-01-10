using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetsButton : ButtonGeneric
{
   

    public override void ButtonPressed()
    {

        GetComponentInChildren<Canvas>().enabled = true;
        ShopAux.instance.gems.GetComponentInChildren<Canvas>().enabled = false;
        ShopAux.instance.time.GetComponentInChildren<Canvas>().enabled = false;
        ShopAux.instance.coins.GetComponentInChildren<Canvas>().enabled = false;


        Debug.Log("He pulsado el botón");
        SetButton1();
        SetButton2();
        SetButton3();
    }
    public virtual void SetButton1()
    {
        //Comprar texto
        text1.text = "3 euros";
        buttonGeneric1.image = imagen1;

    }
    public virtual void SetButton2()
    {
        
        text2.text = "5  euros";
        buttonGeneric2.image = imagen2;
    }
    public virtual void SetButton3()
    {
        //comprar Monumentos

        text3.text = "Proximamente";
        buttonGeneric3.image = imagen3;
    }
    public void SetValue(int i)
    {
        switch (i)
        {
            case 0:
                BuyTexts(0, 3);
                break;
            case 1:
                BuyTexts(1, 5);
                break;
            case 2:
                Debug.Log("Proximamente");
                break;
            
        }
    }
    public void BuyMonuments(int monument, int gems)
    {
        Debug.Log("Has comprado el monumento de indice:" + monument + "monedas" + "me ha costado" + gems);
       // ShopAux.instance.IncrementPlayerCoins(monument);
        ShopAux.instance.IncrementPlayerGems(-gems);
    }
    public void BuyTexts(int text, int money)
    {
        Debug.Log("Has comprado el texto de indice" + text + "monedas" + "me ha costado" + money);
       // ShopAux.instance.IncrementPlayerCoins(moreCoins);
        ShopAux.instance.IncrementPlayerMoney(-money);
    }
}

