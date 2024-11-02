using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<Bullet> bullets;
    [SerializeField] Shop shop;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI highscoreText;
    List<Enemy> enemies;
    int highscore;
    int currentWave;
    bool running;
    List<Coin> coins = new List<Coin>();



    void Start()
    {
        highscore = 0;
        NewGame();
    }

    void Update()
    {

        if(running){
            player.PlayerUpdate(bullets);
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

        if (coins != null) {
            for (int i = coins.Count - 1; i >= 0; i--) {
                Coin coin = coins[i];
                bool coin_destroyed = coin.DestroyCoin(player);
                if (coin_destroyed) {
                    coins.RemoveAt(i);
                }
            }
        }
    }

    bool WaveOver(){
        return enemies.Count == 0;
    }

    void PlayerTakeDamage(float amount){
        player.TakeDamage(amount);
        if(player.dead){
            GameOver();
        }
    }

    void GameOver(){
        running = false;
        UpdateGameOverText();
        gameOverScreen.SetActive(true);
    }

    public void NewGame(){
        gameOverScreen.SetActive(false);
        player.PlayerStart();
        currentWave = 0;
        enemies = new List<Enemy>();
        NewWave();
    }

    public void Quit(){
        Application.Quit();
    }

    void NewWave(){
        currentWave += 1;
        NewHighscoreCheck();
        int randomness = (int)Random.Range(0f, 3f) * currentWave;
        int numberEnemies = 5 * currentWave + randomness;
        for (int i = 0; i < numberEnemies; i++)
        {
            Enemy enemy = PrefabFactory.SpawnEnemy(enemyPrefab, GenerateSpawnLocation(), currentWave);
            enemies.Add(enemy);
        }
        running = true;
    }

    void NewHighscoreCheck(){
        if (currentWave > highscore){
            highscore = currentWave;
        }
    }

    void UpdateGameOverText(){
        highscoreText.text = "you did " + currentWave +" waves, the highscore is " + highscore;
    }

    Vector3 GenerateSpawnLocation(){
        float randomOne = Random.Range(-10f, 10f);
        float randomTwo = Random.Range(-10f, 10f);
        return new Vector3(randomOne,randomTwo);
    }

   

}
