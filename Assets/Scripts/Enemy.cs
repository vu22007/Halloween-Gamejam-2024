using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject enemyPrefab;
    SpriteRenderer spriteRenderer;
    public float speed;
    float health;
    float maxHealth;
    float damage;
    int coinsDrop;

    public void OnCreated(float maxHealth, float damage, float speed){
        this.maxHealth = maxHealth;
        health = maxHealth;
        this.damage = damage;
        this.speed = speed;
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
}
