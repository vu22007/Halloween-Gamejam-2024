using System.Collections.Generic;
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
    public List<Coin> TakeDamage(int damage, Vector3 playerLocation){
        Vector3 enemyPos = gameObject.transform.position;
        //Vector3 relativePosition = (playerLocation - enemyPos).normalized;
        //gameObject.transform.position += - relativePosition * damage/100;
        health -= damage;
        List<Coin> coins = new List<Coin>();
        coins = DispenseCoins(enemyPos);

        return coins;
    }

    public List<Coin> DispenseCoins(Vector3 enemyPos){
        List<Coin> coins = new List<Coin>();
        if (DeadCheck()){
            PrefabFactory generate = new PrefabFactory();
            coins = generate.SpawnCoins(coinPrefab, enemyPos, coinsDrop);
        }
        return coins;
    }

    bool DeadCheck(){
        if(health <= 0){
            return true;
        }
        return false;
    }
}
