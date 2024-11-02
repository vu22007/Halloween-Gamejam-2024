using UnityEngine;

public class Gun
{
    SpriteRenderer spriteRenderer;
    int damage;

    public int Damage{
        get{return damage;}
    }

    public Bullet Use(Bullet bulletSprite, Vector3 playerPosition, Vector3 attackDirection){
        return PrefabFactory.SpawnBullet(bulletSprite, playerPosition, attackDirection);
    }
}
