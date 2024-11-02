using UnityEngine;

public class Player : MonoBehaviour
{
    Weapon equippedWeapon;
    int health;
    int maxHealth;
    float speed;
    int money;

    public void PlayerMovement(){
        //use speed
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
