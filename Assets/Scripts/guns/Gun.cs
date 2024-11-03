using UnityEngine;

public class Gun
{
    SpriteRenderer spriteRenderer;
    float damageWeighting = 0.1f;
    float fireRateWeighting = 0.1f;
    float bulletSpeedWeighting = 0.1f;
    public float maxDamageMultiplier = 10f;
    public float maxFireRateMultiplier = 10f;
    public float maxBulletSpeedMultiplier = 10f;
    public float damageMultiplier;
    public float fireRateMultiplier;
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
        float possibleDamageMultiplier = 1f + (damageWeighting * Random.Range(1, wave + 1));
        damageMultiplier = Mathf.Min(possibleDamageMultiplier, maxDamageMultiplier);
        float possibleFireRateMultiplier = 1f + (fireRateWeighting * Random.Range(1, wave + 1));
        fireRateMultiplier = Mathf.Min(possibleFireRateMultiplier, maxFireRateMultiplier);
        float possibleBulletSpeedMultiplier = 1f + (bulletSpeedWeighting * Random.Range(1, wave + 1));
        bulletSpeedMultiplier = Mathf.Min(possibleBulletSpeedMultiplier, maxBulletSpeedMultiplier);
    }

    public void Use(GameObject bulletSprite, Vector3 playerPosition, Vector3 attackDirection, float bulletHealth, float bulletSpeed){
        PrefabFactory.SpawnBullet(bulletSprite, playerPosition, attackDirection, bulletHealth * damageMultiplier, bulletSpeed * bulletSpeedMultiplier);
    }
}
