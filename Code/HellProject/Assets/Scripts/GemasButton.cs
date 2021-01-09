using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GemasButton : ButtonGeneric
{
    // Start is called before the first frame update
    
    public override void ButtonPressed()
    {
        GetComponentInChildren<Canvas>().enabled = true;
        ShopAux.instance.coins.GetComponentInChildren<Canvas>().enabled = false;
        ShopAux.instance.time.GetComponentInChildren<Canvas>().enabled = false;
        SetButton1();
        SetButton2();
        SetButton3();
    }
    public void SetButton1()
    {
        button1.text = "1  euro + 100 monedas";
        button1.enabled = true;
        imagen1.enabled = true;

    }
    public void SetButton2()
    {
        button2.text = "3  euros + 200 monedas";
        button2.enabled = true;
        imagen2.enabled = true;
    }
    public void SetButton3()
    {
        button3.text = "5  euros + 350 monedas";
        button3.enabled = true;
        imagen3.enabled = true;
    }
    public void SetValue(int i)
    {
        switch (i)
        {
            case 0:
                BuyGems(10, 1, 100);
                break;
            case 1:
                BuyGems(30, 3, 200);
                break;
            case 2:
                BuyGems(50, 5, 350);
                break;
        }
    }
   public void BuyGems(int moreGems, int money, int coins)
    {
        Debug.Log("Has comprado" + moreGems + "monedas y me ha costado: " + money + "y" + coins+ "monedas");
        ShopAux.instance.IncrementPlayerGems(moreGems);
        ShopAux.instance.IncrementPlayerCoins(-coins);
        ShopAux.instance.IncrementPlayerMoney(-money);
    }
}
