using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    SpriteRenderer spriteRenderer;
    public float speed;
    int health;
    int maxHealth;
    int damage;
    int coinsDrop;

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
