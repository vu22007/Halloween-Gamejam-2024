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
}