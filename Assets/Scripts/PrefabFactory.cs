using System.Collections.Generic;
using UnityEngine;


public static class PrefabFactory
{

    public static List<Coin> SpawnCoins(GameObject prefab, Vector3 spawnPosition, int amount) {
         List<Coin> coins = new List<Coin>();
            // Instantiate a new sprite at the specified position and default rotation
            for(int i= 0; i < 3; i++) {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-2.0f, 2.0f),
                    Random.Range(-2.0f, 2.0f)
                );
                GameObject coin = Object.Instantiate(prefab, spawnPosition + randomOffset, Quaternion.identity);
                Coin coinClass = coin.GetComponent<Coin>();
                coins.Add(coinClass);

            }
            return coins;
    }

    public static void SpawnBullet(GameObject prefab, Vector3 spawnPosition){
        GameObject _bullet = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    public static Enemy SpawnEnemy(GameObject prefab, Vector3 spawnPosition, int wave){
        GameObject enemy = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
        Enemy enemyClass = enemy.GetComponent<Enemy>();
        enemyClass.OnCreated(CalculateMaxHealth(wave), CalculateDamage(wave), CalculateSpeed(wave));
        return enemyClass;
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