using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    Gun equippedGun;
    float health;
    float maxHealth;
    float speed = 10f;
    int money;
    float counPickupDistance;
    Rigidbody2D body;
    float moveLimiter = 0.7f;
    float coolDownMax = 2f;
    float coolDownTimer = 0f;
    public bool dead;

    public void PlayerStart(){
        body = GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector3(0f,0f);
        equippedGun = new Gun();
        maxHealth = 30f;
        health = maxHealth;
        money = 0;
        dead = false;
    }

    public void PlayerUpdate(List<Bullet> bullets){
        PlayerMovement();
        PlayerAttack(bullets);
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

    public void PlayerAttack(List<Bullet> bullets){
        if (Input.GetMouseButton(0) && (coolDownTimer <= 0)) {
            Vector3 direction = Input.mousePosition - gameObject.transform.position;
            bullets.Add(equippedGun.Use(bulletPrefab, gameObject.transform.position, direction));
            coolDownTimer = coolDownMax;
        } else {
            coolDownTimer -= Time.deltaTime;
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
