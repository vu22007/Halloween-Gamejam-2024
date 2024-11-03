using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    SpriteRenderer spriteRenderer;
    public float speed;
    float health;
    float maxHealth;
    float damage;
    int coinsDrop;
    private float knockbackDuration = 0.2f;
    private bool isKnockedBack = false;
    public bool invincible;

    float invincibilityTimer = 0.25f;

    public float damageDealt {
        get {return damage;}
    }

    public void OnCreated(int wave){
        this.maxHealth = CalculateMaxHealth(wave);
        health = maxHealth;
        this.damage = CalculateDamage(wave);
        this.speed = CalculateSpeed(wave);
        coinsDrop = CalculateCoinsDrop();
    }

    int CalculateCoinsDrop(){
        int fromHealth = (int)maxHealth / 10;
        int fromDamage = (int)damage / 5;
        return 1 + fromHealth + fromDamage;
    }

    public void EnemyMovement(Vector3 playerLocation){
        //just go towards the player
        if (!isKnockedBack) {
            Vector3 enemyPos = gameObject.transform.position;
            Vector3 relativePosition = (playerLocation - enemyPos).normalized;
            Vector3 movement = relativePosition * speed * Time.deltaTime;
            gameObject.transform.position += movement;
        }
    }

    private IEnumerator KnockBack(Vector3 bulletDirection) {
        isKnockedBack = true;
        Vector3 knockbackForce = bulletDirection.normalized * damage*100;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(knockbackForce, ForceMode2D.Force);
        yield return new WaitForSeconds(knockbackDuration);
        rb.linearVelocity = Vector3.zero;
        isKnockedBack = false;
    }

    IEnumerator IsHurting() {
        if (invincible == true) {
            yield return new WaitForSeconds(invincibilityTimer);
        }
        invincible = false;
    }

    public void TakeDamage(int damage, Vector3 bulletDirection){
        Vector3 enemyPos = gameObject.transform.position;

        if (!isKnockedBack) {
            StartCoroutine(KnockBack(bulletDirection));
        }

        if (!invincible) {
            health -= damage;
            invincible = true;
            StartCoroutine(IsHurting());
            DispenseCoins(enemyPos);
        }
    }

    public void DispenseCoins(Vector3 enemyPos){
        if (DeadCheck()){
            PrefabFactory.SpawnCoins(coinPrefab, enemyPos, coinsDrop);
        }
    }

    bool DeadCheck(){
        if(health <= 0){
            return true;
        }
        return false;
    }

    static float CalculateMaxHealth(int wave){
        float randomness = Random.Range(0f, 5f * wave);
        return 20f + randomness + wave * 6;
    }
    static float CalculateDamage(int wave){
        float randomness = Random.Range(0f, 2f * wave);
        return 5f + randomness + wave * 2;
    }
    static float CalculateSpeed(int wave){
        return 1f + 0.01f * wave;
    }
}
