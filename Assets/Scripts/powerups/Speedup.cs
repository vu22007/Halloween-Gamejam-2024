using UnityEngine;

abstract public class SpeedUp : StatUp
{
    void Apply(Player player){
      player.IncreaseSpeed(magnitude);
    }
}
