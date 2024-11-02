using UnityEngine;

abstract public class MaxHealthUp : StatUp
{
    void Apply(Player player){
      player.IncreaseMaxHealth(magnitude);
    }
}
