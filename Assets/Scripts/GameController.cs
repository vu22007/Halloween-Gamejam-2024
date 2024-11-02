using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Shop shop;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] PopUpText popUpText;
    List<Enemy> enemies;
    List<Bullet> bullets;
    int highscore;
    int currentWave;
    bool running;
    List<Coin> coins = new List<Coin>();

    float minDistanceForAttack = 1.0f;
    private float knockbackDuration;
    private bool isKnockedBack;


    void Start()
    {
        highscore = 0;
        knockbackDuration = 0.2f;
        isKnockedBack = false;
        enemies = new List<Enemy>();
        bullets = new List<Bullet>();
        shop.SetupShop();
        NewGame();
    }

    void Update()
    {


        if(running){
            Bullet newBullet = player.PlayerUpdate();
            if (newBullet != null) {
                bullets.Add(newBullet);
            }
            foreach (Enemy enemy in enemies)
            {
                if (Vector3.Distance(enemy.transform.position, player.transform.position) <= minDistanceForAttack) {
                    PlayerTakeDamage(enemy.damageDealt, enemy.transform.position);
                }
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
        //Testing
        if(Input.GetKeyDown(KeyCode.X) && !shop.isUp){
            running = false;
            shop.StartShop();
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

    private IEnumerator KnockBack(Vector3 knockbackForce) {
        isKnockedBack = true;
        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration) { 
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.AddForce(knockbackForce, ForceMode2D.Force);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isKnockedBack = false;
    }

    bool WaveOver(){
        return enemies.Count == 0;
    }

    void PlayerTakeDamage(float damage, Vector3 enemyPos){
        if (!isKnockedBack) {
            Vector3 relativePosition = enemyPos - player.transform.position;
            Vector3 knockbackForce = (- relativePosition).normalized * damage * 2;
            StartCoroutine(KnockBack(knockbackForce));
        }
        player.TakeDamage(damage);
        if(player.dead){
            GameOver();
        }
    }

    void GameOver(){
        isKnockedBack = false;
        running = false;
        UpdateGameOverText();
        gameOverScreen.SetActive(true);
    }

    public void NewGame(){
        gameOverScreen.SetActive(false);
        player.PlayerStart();
        currentWave = 0;
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies = new List<Enemy>();
        NewWave();
    }

    public void Quit(){
        Application.Quit();
    }

    public void Continue(){
        shop.CloseShop();
        running = true;
        NewWave();
    }

    public void KillEnemy(Enemy enemy){
        enemies.Remove(enemy);
        Destroy(enemy);
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
        StartCoroutine(popUpText.QueuePopUp("Wave " + currentWave, 0.1f, Color.white));
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
