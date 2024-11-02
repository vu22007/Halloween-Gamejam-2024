using UnityEngine;

public class Bullet
{
    Vector3 direction;
    float speed;
    Rigidbody2D body;

    public void OnCreate(Vector3 startDirection){
        direction = startDirection;
    }

    public void BulletUpdate(){
        body.linearVelocity = direction * speed;
    }
}
