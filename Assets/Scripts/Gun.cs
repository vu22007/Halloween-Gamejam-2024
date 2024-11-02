using UnityEngine;

abstract public class Gun : Weapon
{
    public void Use(Vector3 playerPosition, Vector3 attackDirection){
      
      PrefabFactory.SpawnBullet(prefab, playerPosition, attackDirection);
    }
}
