using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Player player;
    void Start()
    {
        
    }

    void Update()
    {
        player.PlayerMovement();
    }
}
