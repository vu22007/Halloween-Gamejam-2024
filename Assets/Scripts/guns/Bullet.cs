using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    Vector3 direction;
    float speed = 10f;
    Rigidbody2D body;

    int bulletDamage;

    public void OnCreated(Vector3 startDirection){
        body = GetComponent<Rigidbody2D>();
        direction = startDirection;
    }

    public void SetDamage(int damage) {
        bulletDamage = damage;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            
            if (enemy != null) {
                enemy.TakeDamage(bulletDamage, enemy.transform.position - gameObject.transform.position);
                Destroy(gameObject);
            }
        }
    }

    public void BulletUpdate(){
        gameObject.transform.position += direction * speed * Time.deltaTime;
    }
}
