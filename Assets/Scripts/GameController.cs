using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Shop shop;
    [SerializeField] GameObject pauseMenu;
    List<Enemy> enemies;
    List<Bullet> bullets;
    int wave;
    bool running;


    void Start()
    {
        wave = 0;
        enemies = new List<Enemy>();
        bullets = new List<Bullet>();
        NewWave();
    }

    void Update()
    {
        if(running){
            Bullet newBullet = player.PlayerUpdate();
            if (newBullet) {
                bullets.Add(newBullet);
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.EnemyMovement(player.gameObject.transform.position);
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.BulletUpdate();
            }
            if(WaveOver()){
                running = false;
                shop.StartShop();
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !shop.isUp){
            pauseMenu.SetActive(running);
            running = !running;
        }
        
    }

    bool WaveOver(){
        return enemies.Count == 0;
    }

    void NewWave(){
        wave += 1;
        int randomness = (int)Random.Range(0f, 3f) * wave;
        int numberEnemies = 5 * wave + randomness;
        for (int i = 0; i < numberEnemies; i++)
        {
            Enemy enemy = PrefabFactory.SpawnEnemy(enemyPrefab, GenerateSpawnLocation(), wave);
            enemies.Add(enemy);
        }
        running = true;
    }

    Vector3 GenerateSpawnLocation(){
        float randomOne = Random.Range(-10f, 10f);
        float randomTwo = Random.Range(-10f, 10f);
        return new Vector3(randomOne,randomTwo);
    }

}
