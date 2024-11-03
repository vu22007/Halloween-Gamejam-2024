using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D body;
    Vector3 velocity;
    float health;

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
                float damageTaken = Mathf.Max(health, enemy.Health);
                enemy.TakeDamage(damageTaken, enemy.transform.position - gameObject.transform.position);
                health -= damageTaken;
            }
        }
    }

    public bool CheckDead() {
        if (health <= 0){
            return true;
        } else {
            return false;
        }
    }

    public void BulletUpdate(){
        gameObject.transform.position += velocity * Time.deltaTime;
    }
}
