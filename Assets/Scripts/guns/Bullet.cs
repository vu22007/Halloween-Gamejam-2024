using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    Rigidbody2D body;
    Vector3 direction;
    float health;
    float speed;

    public void OnCreated(Vector3 startDirection, float bulletHealth, float bulletSpeed){
        body = GetComponent<Rigidbody2D>();
        direction = startDirection;
        health = bulletHealth;
        speed = bulletSpeed;
        Debug.Log(health);
        Debug.Log(speed);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            
            if (enemy != null) {
                enemy.TakeDamage(health, enemy.transform.position - gameObject.transform.position);
                Destroy(gameObject);
            }
        }
    }

    public void BulletUpdate(){
        gameObject.transform.position += direction * speed * Time.deltaTime;
    }
}
