using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject health;
    [SerializeField] Player player;
    public void UpdatePlayerHealthBar(){
        if(player.health == 0){
            health.transform.localScale = new Vector3(0, 1);
        }
        else{
            health.transform.localScale = new Vector3(player.health/player.maxHealth, 1);
        }
    }
}
