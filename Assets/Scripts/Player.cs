using UnityEngine;

public class Player : MonoBehaviour
{
    int health;
    int maxHealth;
    float speed;
    int money;

    public void PlayerMovement(){
        //use speed
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
