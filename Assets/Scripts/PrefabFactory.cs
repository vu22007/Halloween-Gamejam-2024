using UnityEngine;


public class PrefabFactory
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SpawnCoins(GameObject prefab, Vector3 spawnPosition, int amount) {
    // Check if the prefab is assigned
            // Instantiate a new sprite at the specified position and default rotation
            for(int i= 0; i < amount; i++) {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-2.0f, 2.0f),
                    Random.Range(-2.0f, 2.0f)
                );
                GameObject Coin = Object.Instantiate(prefab, spawnPosition + randomOffset, Quaternion.identity);

            }
    }
}