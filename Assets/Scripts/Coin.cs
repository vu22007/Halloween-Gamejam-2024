using UnityEngine;

public class Coin : MonoBehaviour
{
    public float destroyDistance = 2.0f;
    private BoxCollider2D coinCollider;

    void Start() {
        coinCollider = gameObject.GetComponent<BoxCollider2D>();
        coinCollider.isTrigger = true;
    }
    
}
