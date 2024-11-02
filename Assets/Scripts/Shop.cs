using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] List<PowerUp> powerups;
    public bool isUp = false;

    public void StartShop(){
        isUp = true;
    }

    public void CloseShop(){
        isUp = false;
    }
}
