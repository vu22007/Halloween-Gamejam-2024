using UnityEngine;

abstract public class Weapon
{
    int damage;

    public int Damage{
        get{return damage;}
    }

    abstract public void Use();
}
