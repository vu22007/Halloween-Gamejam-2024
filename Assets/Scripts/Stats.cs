using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] TextMeshProUGUI maxHealthText;
    [SerializeField] TextMeshProUGUI speedText;

    [SerializeField] TextMeshProUGUI damageText;
    
    public void UpdateTexts(){
        maxHealthText.text = "max health: " + player.maxHealth;
        speedText.text = "speed: " + player.speed;
        damageText.text = "damage: " + player.damage;
    }
}
