using UnityEngine;

abstract public class Weapon
{
    SpriteRenderer spriteRenderer;
    int damage;

    public int Damage{
        get{return damage;}
    }

    abstract public void Use(vector3 playerPosition);
}
