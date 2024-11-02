using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] List<Enemy> enemies;
    [SerializeField] Shop shop;
    [SerializeField] GameObject pauseMenu;
    int wave;
    bool running;


    void Start()
    {
        wave = 0;
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
        //spawn enemies scaling with wave number
        running = true;
    }

}
