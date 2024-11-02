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

    public static Bullet SpawnBullet(Bullet prefab, Vector3 spawnPosition, Vector3 moveDirection){
        Bullet newBullet = Bullet(Object.Instantiate(prefab, spawnPosition, Quaternion.identity));
        return newBullet;
    }

    public static Enemy SpawnEnemy(GameObject prefab, Vector3 spawnPosition, int wave){
        GameObject enemy = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
        Enemy enemyClass = enemy.GetComponent<Enemy>();
        enemyClass.OnCreated(wave);
        return enemyClass;
    }
}