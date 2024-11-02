using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] List<PowerUp> powerups;
    [SerializeField] List<TextMeshProUGUI> shopPlacesTexts;
    [SerializeField] List<GameObject> shopPlacesButtons;
    [SerializeField] GameObject shopInterface;
    [SerializeField] Player player;


    public bool isUp = false;

    public void SetupShop(){
        for (int i = 0; i < powerups.Count; i++)
        {
            shopPlacesTexts[i].text = powerups[i].ItemName + powerups[i].Price;
            shopPlacesButtons[i].SetActive(true);
        }
    }
    public void StartShop(){
        shopInterface.SetActive(true);
        isUp = true;
    }

    public void CloseShop(){
        isUp = false;
        shopInterface.SetActive(false);
    }

    public void BuyItem(int index){
        PowerUp item = powerups[index];
        if(player.SpendMoney(item.Price)){
            player.AddPowerUp(item);
        }
    }
}
