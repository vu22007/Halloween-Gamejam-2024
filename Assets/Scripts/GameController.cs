using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Shop shop;
    [SerializeField] GameObject pauseMenu;
    List<Enemy> enemies;
    int wave;
    bool running;


    void Start()
    {
        wave = 0;
        enemies = new List<Enemy>();
        NewWave();
    }

    void Update()
    {
        if(running){
            player.PlayerMovement();
            foreach (Enemy enemy in enemies)
            {
                enemy.EnemyMovement(player.gameObject.transform.position);
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
        Enemy enemy = PrefabFactory.SpawnEnemy(enemyPrefab, GenerateSpawnLocation(), wave);
        enemies.Add(enemy);
        running = true;
    }

    Vector3 GenerateSpawnLocation(){
        return new Vector3(1f,2f);
    }

}
