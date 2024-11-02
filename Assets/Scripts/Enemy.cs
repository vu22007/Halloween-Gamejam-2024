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
    public void TakeDamage(int damage, Vector3 playerLocation){
        Vector3 enemyPos = gameObject.transform.position;
        Vector3 relativePosition = (playerLocation - enemyPos).normalized;
        gameObject.transform.position += - relativePosition * damage/100;
        health -= damage;
                if (DeadCheck()){
            PrefabFactory.SpawnCoins(coinPrefab, enemyPos, 5);
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
