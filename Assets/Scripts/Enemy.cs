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

    float invincibilityTimer = 1.5f;

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
        Vector3 enemyPos = gameObject.transform.position;
        Vector3 relativePosition = (playerLocation - enemyPos).normalized;
        Vector3 movement = relativePosition * speed * Time.deltaTime;
        gameObject.transform.position += movement;
    }

    private IEnumerator KnockBack(Vector3 relativePosition) {
        isKnockedBack = true;
        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration) {
            Vector3 knockbackForce = (- relativePosition).normalized * damage/50;
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(knockbackForce, ForceMode2D.Impulse);
            yield return null;
        } 
        isKnockedBack = false;
    }

    IEnumerator IsHurting() {
        if (invincible == true) {
            yield return new WaitForSeconds(invincibilityTimer);
        }
        invincible = false;
    }

    public List<Coin> TakeDamage(int damage, Vector3 playerLocation){
        Vector3 enemyPos = gameObject.transform.position;
        Vector3 relativePosition = playerLocation - enemyPos;
        List<Coin> coins = new List<Coin>();

        if (!isKnockedBack) {
            StartCoroutine(KnockBack(relativePosition));
        }

        if (!invincible) {
            health -= damage;
            invincible = true;
            StartCoroutine(IsHurting());
            coins = DispenseCoins(enemyPos);
        }

        return coins;
    }

    public List<Coin> DispenseCoins(Vector3 enemyPos){
        List<Coin> coins = new List<Coin>();
        if (DeadCheck()){
            coins = PrefabFactory.SpawnCoins(coinPrefab, enemyPos, coinsDrop);
        }
        return coins;
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
