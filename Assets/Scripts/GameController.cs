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
    List<Enemy> enemies;
    GameObject[] bullets;
    int highscore;
    int currentWave;
    bool running;

    float minDistanceForAttack = 1.0f;
    private float knockbackDuration = 0.2f;
    private bool isKnockedBack = false;
    Vector2 screenLeftBottom;
    Vector2 screenTopRight;


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

    void Update()
    {

        if(running){
            player.PlayerUpdate();
            foreach (Enemy enemy in enemies)
            {
                if(enemy.dead){
                    KillEnemy(enemy);
                }
                if (Vector3.Distance(enemy.transform.position, player.transform.position) <= minDistanceForAttack) {
                    PlayerTakeDamage(enemy.damageDealt, enemy.transform.position);
                }
                enemy.EnemyMovement(player.gameObject.transform.position);
            }
            
            bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bullet in bullets) {
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null) {
                    bulletScript.BulletUpdate();
                }
            }
            if(WaveOver()){
                running = false;
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
