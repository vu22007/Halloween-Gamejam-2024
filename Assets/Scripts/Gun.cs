using UnityEngine;

abstract public class Gun
{
    public void Use(vector3 playerPosition){
      PrefabFactory.SpawnBullet();
    }
}
