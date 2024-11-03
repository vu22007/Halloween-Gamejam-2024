using UnityEngine;

public class Gun
{
    SpriteRenderer spriteRenderer;
    public float damageWeighting = 0.1f;
    public float fireRateWeighting = 0.1f;
    public float bulletSpeedWeighting = 0.1f;
    public float maxDamageMultiplier = 10f;
    public float maxFireRateMultiplier = 10f;
    public float maxBulletSpeedMultiplier = 10f;
    float damageMultiplier;
    float fireRateMultiplier;
    float bulletSpeedMultiplier;

    public float GetDamageMultiplier(){
        return damageMultiplier;
    }

    public float GetFireRateMultiplier(){
        return fireRateMultiplier;
    }

    public float GetBulletSpeedMultiplier(){
        return bulletSpeedMultiplier;
    }

    public Gun(int wave){
        float possibleDamageMultiplier = 1 + damageWeighting * Random.Range(1, wave + 1);
        damageMultiplier = Mathf.Min(possibleDamageMultiplier, maxDamageMultiplier);
        float possibleFireRateMultiplier = 1 + fireRateWeighting * Random.Range(1, wave + 1);
        fireRateMultiplier = Mathf.Min(possibleFireRateMultiplier, maxFireRateMultiplier);
        float possibleBulletSpeedMultiplier = 1 + bulletSpeedWeighting * Random.Range(1, wave + 1);
        bulletSpeedMultiplier = Mathf.Min(possibleBulletSpeedMultiplier, maxBulletSpeedMultiplier);
    }

    public Bullet Use(GameObject bulletSprite, Vector3 playerPosition, Vector3 attackDirection, float bulletHealth, float bulletSpeed){
        return PrefabFactory.SpawnBullet(bulletSprite, playerPosition, attackDirection, bulletHealth * damageMultiplier, bulletSpeed * bulletSpeedMultiplier);
    }
}
