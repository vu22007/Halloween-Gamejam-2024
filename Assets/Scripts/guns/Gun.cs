using UnityEngine;

public class Gun
{
    SpriteRenderer spriteRenderer;

    //TODO: Hardcoded Damage. Set to a variable value depending on Gun strenth
    int damage = 15;

    public int Damage{
        get{return damage;}
    }

    public Bullet Use(GameObject bulletSprite, Vector3 playerPosition, Vector3 attackDirection){
        return PrefabFactory.SpawnBullet(bulletSprite, playerPosition, attackDirection);
    }
}
