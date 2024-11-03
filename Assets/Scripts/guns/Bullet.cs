using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    Rigidbody2D body;
    Vector3 velocity;
    float health;
    float speed;

    public void OnCreated(Vector3 startDirection, float bulletHealth, float speed){
        body = GetComponent<Rigidbody2D>();
        velocity = startDirection * speed;
        velocity.z = 0;
        health = bulletHealth;
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
        gameObject.transform.position += velocity * Time.deltaTime;
    }
}
