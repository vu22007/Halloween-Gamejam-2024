using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private AudioClip coinCollect;
    [SerializeField] private AudioClip playerShoot;
    Gun equippedGun;
    List<PowerUp> powerUps;
    public float health;
    float maxHealth;
    float speed;
    public int money;
    float counPickupDistance;
    Rigidbody2D body;
    float moveLimiter = 0.7f;
    float attackCoolDownMax = 2f;
    float attackCoolDownTimer = 0f;
    public bool dead;
    Camera cam;
    public bool invincible;
    float invincibilityTimer = 0.5f;
    Vector2 screenLeftBottom;
    Vector2 screenTopRight;
    SpriteRenderer spriteRenderer;
    Vector2 spriteSize;
    Vector2 spriteHalfSize;
    float damage;
    float bulletSpeed;

    public void PlayerStart(Vector2 leftBottom, Vector2 topRight){
        powerUps = new List<PowerUp>();
        body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.None;
        gameObject.transform.position = new Vector3(0f,0f);
        equippedGun = new Gun(0);
        cam = Camera.main;
        maxHealth = 30f;
        health = maxHealth;
        money = 30;
        speed = 10f;
        dead = false;
        damage = 10f;
        bulletSpeed = 30f;
        screenLeftBottom = leftBottom;
        screenTopRight = topRight;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteSize     = spriteRenderer.sprite.bounds.size;
        spriteHalfSize = spriteRenderer.sprite.bounds.extents;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Coin")) {
            Coin coin = other.GetComponent<Coin>();
            if (coin != null) {
                SFXPlaying.instance.playSFXCllip(coinCollect, transform, 1f);
                this.GetMoney(1);
                Destroy(other.gameObject);
            }
        }
    }

    public Gun getGun{
        get {return equippedGun;}
    }

    public void RefreshPlayer(){
        Heal(20f);
        gameObject.transform.position = new Vector3(0f,0f);
    }

    public void PlayerUpdate(){
        PlayerMovement();
        CheckPlayerBordered();
        PlayerAttack();
    }

    void PlayerMovement(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical"); 
        if (horizontal != 0 && vertical != 0) {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        Vector3 direction = (worldPoint - gameObject.transform.position).normalized;
        Quaternion wantedRotation = Quaternion.LookRotation(transform.forward, direction);
        gameObject.transform.rotation = wantedRotation;
    }

    void PlayerAttack(){
        if (Input.GetMouseButton(0) && (attackCoolDownTimer <= 0)) {
            attackCoolDownTimer = attackCoolDownMax / equippedGun.GetFireRateMultiplier();
            Vector2 mousePos = Input.mousePosition;
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
            Vector3 direction = (worldPoint - gameObject.transform.position).normalized;
            equippedGun.Use(bulletPrefab, gameObject.transform.position, direction, damage, bulletSpeed);
            SFXPlaying.instance.playSFXCllip(playerShoot, transform, 1f);
        } else {
            attackCoolDownTimer -= Time.deltaTime;
        }
    }

    void CheckPlayerBordered(){
        float spriteLeft   = transform.position.x - spriteHalfSize.x;
        float spriteRight  = transform.position.x + spriteHalfSize.x;
        float spriteBottom = transform.position.y - spriteHalfSize.y;
        float spriteTop    = transform.position.y + spriteHalfSize.y;
        Vector3 clampedPosition = transform.position;
        if(spriteLeft < screenLeftBottom.x)
        {
            clampedPosition.x = screenLeftBottom.x + spriteHalfSize.x;
        }
        else if(spriteRight > screenTopRight.x)
        {
            clampedPosition.x = screenTopRight.x - spriteHalfSize.x;
        }
        if(spriteBottom < screenLeftBottom.y)
        {
            clampedPosition.y = screenLeftBottom.y + spriteHalfSize.y;
        }
        else if(spriteTop > screenTopRight.y)
        {
            clampedPosition.y = screenTopRight.y - spriteHalfSize.y;
        }
        transform.position = clampedPosition;
    }

    IEnumerator IsHurting() {
        if (invincible == true) {
            yield return new WaitForSeconds(invincibilityTimer);
        }
        invincible = false;
    }
    
    public void TakeDamage(float damage){
        if (!invincible) {
            
            health -= damage;
            invincible = true;
            StartCoroutine(IsHurting());
            if(DeadCheck()){
                body.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                dead = true;
            }
        }
    }

    public void AddPowerUp(PowerUp powerUp){
        powerUps.Add(powerUp);
        switch (powerUp.Effect)
        {
            case "MaxHealth":
                maxHealth += powerUp.Magnitude;
                health += powerUp.Magnitude;
                break;
            case "Speed":
                speed += powerUp.Magnitude;
                break;
            default:
                break;
        }
    }

    public void Heal(float amount){
        health += amount;
        if(health > maxHealth){
            health = maxHealth;
        }
    }

    public void GetMoney(int amount){
        money += amount;
    }
    public bool SpendMoney(int amount){
        if(money < amount){
            return false;
        }
        money -= amount;
        return true;
    }

    public void IncreaseSpeed(float amount){
        speed += amount;
    }

    public void IncreaseMaxHealth(float amount){
        maxHealth += amount;
    }

     bool DeadCheck(){
        if(health <= 0){
            return true;
        }
        return false;
    }
}
