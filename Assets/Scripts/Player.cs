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
    Rigidbody2D body;
    float moveLimiter = 0.7f;
    float coolDownMax = 2f;
    float coolDownTimer = 0f;

    void Start (){
        body = GetComponent<Rigidbody2D>(); 
        equippedGun = new Gun();
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
            Vector3 direction = (Input.mousePosition - gameObject.transform.position).normalized;
            return equippedGun.Use(bulletPrefab, gameObject.transform.position, direction);
        } else {
            coolDownTimer -= Time.deltaTime;
            return null;
        }
    }
    
    public void TakeDamage(int damage){
        health -= damage;
        if (GameOverCheck()){
            //game over stuff
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

    bool GameOverCheck(){
        if(health <= 0){
            return true;
        }
        return false;
    }

    public void IncreaseSpeed(float amount){
        speed += amount;
    }

    public void IncreaseMaxHealth(float amount){
        maxHealth += amount;
    }
}
