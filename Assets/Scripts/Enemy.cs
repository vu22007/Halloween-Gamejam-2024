using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float speed;
    int health;
    int maxHealth;
    int damage;
    int coinsDrop;

    public void EnemyMovement(Vector3 playerLocation){
        //just go towards the player
        Vector3 enemyPos = gameObject.transform.position;
        Vector3 relativePosition = playerLocation - enemyPos;
        Vector3 movement = relativePosition.normalized * speed * Time.deltaTime;
        gameObject.transform.position += movement;
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
