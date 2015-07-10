using UnityEngine;
using System.Collections;

public class SLaserTurret : LaserTurret
{
    Animator laserBlast;
    // Use this for initialization
    void Start()
    {
        BaseTurretStart();
        laserBlast = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        BaseTurretUpdate();

       //if (charging)
       //{
       //    charging = false;
       //   
       //    FireLaser();
       //}
    }

    public override void ShootProjectile()
    {
        laserBlast.SetTrigger("Charge");
        Invoke("InstantiateProjectile", 0.16f);
    }

    void InstantiateProjectile()
    {
        FireLaser();
    }
}
