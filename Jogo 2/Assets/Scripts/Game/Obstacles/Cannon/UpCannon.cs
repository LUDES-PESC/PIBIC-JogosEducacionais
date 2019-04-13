using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCannon : Obstacle, IBulletTarget {
    public void OnBulletTouch()
    {
        print("BULLET TOUCHED :: " + name);
    }
    public void ShootBullet()
    {

    }
}
