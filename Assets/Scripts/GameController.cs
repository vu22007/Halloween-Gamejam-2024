using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Shop shop;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Stats stats;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] PopUpText popUpText;
    [SerializeField] private AudioClip playerDamage;
    List<Enemy> enemies;
    int highscore;
    int currentWave;
    bool running;
    float minDistanceForAttack = 1.0f;
    private float knockbackDuration = 0.2f;
    private bool isKnockedBack = false;
    Vector2 screenLeftBottom;
    Vector2 screenTopRight;
    public float enemySpawnDistanceMin = 0f;
    public float enemySpawnDistanceMax = 5f;

    void Start()
    {
        screenLeftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        screenTopRight = Camera.main.ViewportToWorldPoint(Vector3.one);
        highscore = 0;
        isKnockedBack = false;
        enemies = new List<Enemy>();
        shop.SetupShop();
        healthBar.UpdatePlayerHealthBar();
        NewGame();
    }

    private bool CheckObjectOutsideScreen(GameObject gameObject){
        if ((gameObject.transform.position.x < screenLeftBottom.x - 1) ||
            (gameObject.transform.position.x > screenTopRight.x + 1) ||
            (gameObject.transform.position.y < screenLeftBottom.y - 1) ||
            (gameObject.transform.position.y > screenTopRight.y + 1)){
                return true;
            } else{
                return false;
            }
    }

    void Update()
    {

        if(running){
            player.PlayerUpdate();
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                if(enemy.dead){
                    KillEnemy(enemy);
                }
                if (Vector3.Distance(enemy.transform.position, player.transform.position) <= minDistanceForAttack){
                    PlayerTakeDamage(enemy.Damage, enemy.transform.position);
                }
                enemy.EnemyMovement(player.gameObject.transform.position);
            }
            
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bullet in bullets) {
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript == null){
                    continue;
                } else
                bulletScript.BulletUpdate();
                if (CheckObjectOutsideScreen(bullet) || bulletScript.CheckDead()){
                    Destroy(bullet);
                }
            }
            if(WaveOver()){
                running = false;
                healthBar.gameObject.SetActive(false);
                shop.StartShop(currentWave);
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !shop.isUp){
            stats.UpdateTexts();
            pauseMenu.SetActive(running);
            running = !running;
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
        healthBar.UpdatePlayerHealthBar();
        if (!isKnockedBack) {
            Vector3 relativePosition = enemyPos - player.transform.position;
            Vector3 knockbackForce = (- relativePosition).normalized * damage * 2;
            StartCoroutine(KnockBack(knockbackForce));
            SFXPlaying.instance.playSFXCllip(playerDamage, transform, 1f);
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
        player.PlayerStart(screenLeftBottom, screenTopRight);
        currentWave = 0;
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        enemies = new List<Enemy>();
        NewWave();
    }

    public void Quit(){
        Application.Quit();
    }

    public void Continue(){
        shop.CloseShop();
        healthBar.gameObject.SetActive(true);
        running = true;
        NewWave();
    }

    public void KillEnemy(Enemy enemy){
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
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
        player.RefreshPlayer();
        healthBar.UpdatePlayerHealthBar();
        StartCoroutine(popUpText.QueuePopUp("Welcome to Apocalypse Hero!!!", 0.07f, Color.red));
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
        int spawnSide = Random.Range(1, 10);
        if (spawnSide <= 3) {
            // Spawn on top
            float xCoord = Random.Range(screenLeftBottom.x, screenTopRight.x);
            float yCoord = screenTopRight.y + Random.Range(enemySpawnDistanceMin, enemySpawnDistanceMax);
            return new Vector3(xCoord, yCoord);
        } else if (spawnSide <= 6) {
            // Spawn on bottom
            float xCoord = Random.Range(screenLeftBottom.x, screenTopRight.x);
            float yCoord = screenLeftBottom.y - Random.Range(enemySpawnDistanceMin, enemySpawnDistanceMax);
            return new Vector3(xCoord, yCoord);
        } else if (spawnSide <= 8) {
            // Spawn on left
            float xCoord = screenLeftBottom.x - Random.Range(enemySpawnDistanceMin, enemySpawnDistanceMax);
            float yCoord = Random.Range(screenLeftBottom.y, screenTopRight.y);
            return new Vector3(xCoord, yCoord);
        } else {
            // Spawn on right
            float xCoord = screenTopRight.x + Random.Range(enemySpawnDistanceMin, enemySpawnDistanceMax);
            float yCoord = Random.Range(screenLeftBottom.y, screenTopRight.y);
            return new Vector3(xCoord, yCoord);
        }
    }
}
