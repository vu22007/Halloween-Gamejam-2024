using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] List<Enemy> enemies;
    void Start()
    {
        
    }

    void Update()
    {
        player.PlayerUpdate();
        foreach (Enemy enemy in enemies)
        {
            enemy.EnemyMovement(player.gameObject.transform.position);
        }
    }
}
