using UnityEngine;

public class Player : MonoBehaviour
{
    int health;
    int maxHealth;
    float speed;
    int money;
    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    void Start (){
        body = GetComponent<Rigidbody2D>(); 
    }

    void Update (){
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
    }

    private void FixedUpdate(){
        if (horizontal != 0 && vertical != 0) 
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void TakeDamage(int damage){
        health -= damage;
        if (GameOverCheck()){
            //game over stuff
        }
    }

    void GetMoney(int amount){
        money += amount;
    }

    bool GameOverCheck(){
        if(health <= 0){
            return true;
        }
        return false;
    }
}
