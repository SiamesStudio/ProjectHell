using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeButton : ButtonGeneric
{
    public override void ButtonPressed()
    {
        GetComponentInChildren<Canvas>().enabled = true;
        ShopAux.instance.coins.GetComponentInChildren<Canvas>().enabled = false;
        ShopAux.instance.gems.GetComponentInChildren<Canvas>().enabled = false;
        SetButton1();
        SetButton2();
        SetButton3();
    }
    public virtual void SetButton1()
    {
        button1.text = "2  euro + 5 gemas";
        button1.enabled = true;
        imagen1.enabled = true;

    }
    public virtual void SetButton2()
    {
        button2.text = "4  euros + 8 gemas";
        button2.enabled = true;
        imagen2.enabled = true;
    }
    public virtual void SetButton3()
    {
        button3.text = "8  euros + 10 gemas";
        button3.enabled = true;
        imagen3.enabled = true;
    }
    public void SetValue(int i)
    {
        switch (i)
        {
            case 0:
                BuyTime(10, 2, 3);
                break;
            case 1:
                BuyTime(30, 5, 5);
                break;
            case 2:
                BuyTime(60, 8, 7);
                break;
        }
    }
    public void BuyTime(int moreTime, int money, int gems)
    {
        Debug.Log("Has comprado" + moreTime + "monedas y me ha costado: " + money + "y" + gems + "gemas");
        ShopAux.instance.incrementTime(moreTime);
        ShopAux.instance.incrementPlayerCoins(-gems);
        ShopAux.instance.incrementPlayerMoney(-money);
    }


}
