using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    Gun equippedGun;
    float health;
>>>>>>> d23d929 (Adding bullet prefab)
    float maxHealth;
    float speed = 10f;
    int money;
    float counPickupDistance;
    Rigidbody2D body;
    float moveLimiter = 0.7f;
    float coolDownMax = 2f;
    float coolDownTimer = 0f;
    public bool dead;
    Camera cam;

    public void PlayerStart(){
        body = GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector3(0f,0f);
        equippedGun = new Gun();
        cam = Camera.main;
        maxHealth = 30f;
        health = maxHealth;
        money = 0;
        dead = false;
    }

    public Bullet PlayerUpdate(){
        PlayerMovement();
        return PlayerAttack();
    }

    public void PlayerMovement(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical"); 
        if (horizontal != 0 && vertical != 0) {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
    }

    public Bullet PlayerAttack(){
        if (Input.GetMouseButton(0) && (coolDownTimer <= 0)) {
            coolDownTimer = coolDownMax;
            Vector2 mousePos = Input.mousePosition;
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
            Vector3 direction = (worldPoint - gameObject.transform.position).normalized;
            return equippedGun.Use(bulletPrefab, gameObject.transform.position, direction);
        } else {
            coolDownTimer -= Time.deltaTime;
            return null;
        }
    }
    
    public void TakeDamage(float damage){
        health -= damage;
        if(DeadCheck()){
            dead = true;
        }
    }

    public void Heal(int amount){
        health += amount;
        if(health > maxHealth){
            health = maxHealth;
        }
    }

    public void GetMoney(int amount){
        money += amount;
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
