using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "Create New Powerup")]
public class PowerUp : ScriptableObject
{
    [SerializeField]string itemName;
    [SerializeField]int price;
    [SerializeField]float magnitude;
    [SerializeField]string effect;

    public int Price{
        get{return price;}
    }
    public string ItemName{
        get{return itemName;}
    }
    public float Magnitude{
        get{return magnitude;}
    }
    public string Effect{
        get{return effect;}
    }
}
