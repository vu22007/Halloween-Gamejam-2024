using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float speed;
    int health;
    int maxHealth;
    int damage;
    int coinsDrop;

    public void EnemyMovement(Vector3 playerLocation){
        
    }

    public void TakeDamage(int damage){
        health -= damage;
        if (DeadCheck()){
            //spawn coins equal to coinsDrop
        }
    }

    bool DeadCheck(){
        if(health <= 0){
            return true;
        }
        return false;
    }
}
