using UnityEngine;

public class Player : MonoBehaviour
{
    Weapon equippedWeapon;
    int health;
    int maxHealth;
    float speed;
    int money;
    Rigidbody2D body;
    float horizontal;
    float vertical;
    float runSpeed = 20f;
    float moveLimiter = 0.7f;

    void Start (){
        body = GetComponent<Rigidbody2D>(); 
    }

    public void PlayerMovement (){
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
    }

    private void FixedUpdate(){
        if (horizontal != 0 && vertical != 0) {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.linearVelocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public void DealDamage(Enemy enemy){
        enemy.TakeDamage(equippedWeapon.Damage);
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
}
