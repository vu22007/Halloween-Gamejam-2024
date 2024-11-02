using UnityEngine;

abstract public class SpeedUp : StatUp
{
    void apply(Player player){
      player.speed += magnitude;
    }
}
