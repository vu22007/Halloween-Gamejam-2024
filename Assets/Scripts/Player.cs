using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Weapon equippedWeapon;
    List<PowerUp> powerUps;
    public float health;
    float maxHealth;
    float speed = 10;
    public int money;
    float counPickupDistance;
    Rigidbody2D body;
    float moveLimiter = 0.7f;
    float coolDownMax = 2f;
    float coolDownTimer = 0f;
    public bool dead;

    public void PlayerStart(){
        powerUps = new List<PowerUp>();
        body = GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector3(0f,0f);
        speed = 10f;
        maxHealth = 30f;
        health = maxHealth;
        money = 30;
        dead = false;
    }

    public void PlayerUpdate(){
        PlayerMovement();
        PlayerAttack();
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

    public void PlayerAttack(){
        if (Input.GetMouseButton(0) && (coolDownTimer <= 0)) {
            // equippedWeapon.Use();
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

    public void Heal(int amount){
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
