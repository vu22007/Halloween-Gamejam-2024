using UnityEngine;

public class Coin : MonoBehaviour
{
    public float destroyDistance = 2.0f;
    private BoxCollider2D coinCollider;

    void Start() {
        coinCollider = gameObject.GetComponent<BoxCollider2D>();
        coinCollider.isTrigger = true;
    }
    public bool DestroyCoin(Player player) {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= destroyDistance) {
            player.GetMoney(1);
            Destroy(gameObject);
            return true;
        } else {
            return false;
        }
    }
    
}
