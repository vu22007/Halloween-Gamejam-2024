using UnityEngine;


public static class PrefabFactory
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void SpawnCoins(GameObject prefab, Vector3 spawnPosition, int amount) {
    // Check if the prefab is assigned
            // Instantiate a new sprite at the specified position and default rotation
            for(int i= 0; i < amount; i++) {
                Vector3 randomOffset = new Vector3(
                    Random.Range(-2.0f, 2.0f),
                    Random.Range(-2.0f, 2.0f)
                );
                GameObject coin = Object.Instantiate(prefab, spawnPosition + randomOffset, Quaternion.identity);

            }
    }

    public static Bullet SpawnBullet(GameObject prefab, Vector3 spawnPosition, Vector3 moveDirection){
        GameObject instantiatedBullet = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
        Bullet newBullet = instantiatedBullet.GetComponent<Bullet>();
        newBullet.OnCreated(moveDirection);
        return newBullet;
    }

    public static Enemy SpawnEnemy(GameObject prefab, Vector3 spawnPosition, int wave){
        GameObject enemy = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
        Enemy newEnemy = enemy.GetComponent<Enemy>();
        newEnemy.OnCreated(CalculateMaxHealth(wave), CalculateDamage(wave), CalculateSpeed(wave));
        return newEnemy;
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