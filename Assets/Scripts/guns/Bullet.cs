using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 direction;
    float speed = 10f;
    Rigidbody2D body;

    public void OnCreated(Vector3 startDirection){
        body = GetComponent<Rigidbody2D>();
        direction = startDirection;
    }

    public void BulletUpdate(){
        gameObject.transform.position += direction * speed * Time.deltaTime;
    }
}
