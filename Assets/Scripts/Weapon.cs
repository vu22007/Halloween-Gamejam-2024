using UnityEngine;

abstract public class Weapon
{
    SpriteRenderer spriteRenderer;
    int damage;

    public int Damage{
        get{return damage;}
    }

    abstract public void Use(Vector3 playerPosition, Vector3 attackDirection);
}
