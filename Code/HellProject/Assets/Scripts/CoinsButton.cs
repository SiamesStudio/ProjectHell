using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CoinsButton : ButtonGeneric
{
    
       public override void ButtonPressed()
    {

        GetComponentInChildren<Canvas>().enabled = true;
        ShopManager.instance.gems.GetComponentInChildren<Canvas>().enabled = false;
        ShopManager.instance.time.GetComponentInChildren<Canvas>().enabled = false;
        ShopManager.instance.assets.GetComponentInChildren<Canvas>().enabled = false;

        Debug.Log("He pulsado el botón");
        SetButton1();
        SetButton2();
        SetButton3();
    }
    public virtual void SetButton1() {
        text1.text = "1  euro";
        buttonGeneric1.image = imagen1;

    }
    public virtual void SetButton2() {

        text2.text = "3  euros";
        buttonGeneric2.image = imagen2;
    }
    public virtual void SetButton3() {

        text3.text = "5  euros";
        buttonGeneric3.image = imagen3;
    }
    public void SetValue(int i)
    {
        switch (i)
        {
            case 0:
                BuyCoins(100,1);
                break;
            case 1:
                BuyCoins(400, 3);
                break;
            case 2:
                BuyCoins(700, 5);
                break;
        }
    }
    public void BuyCoins(int moreCoins, int money)
    {
        Debug.Log("Has comprado" + moreCoins +"monedas" + "me ha costado"+ money);
        ShopManager.instance.IncrementPlayerCoins(moreCoins);
        ShopManager.instance.IncrementPlayerMoney(-money);
    }
}
