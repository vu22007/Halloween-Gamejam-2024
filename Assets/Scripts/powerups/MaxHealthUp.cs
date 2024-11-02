using UnityEngine;

abstract public class MaxHealthUp : StatUp
{
    void apply(Player player){
      player.maxHealth += magnitude;
    }
}
