using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class RangedEnemy : Enemies
{
    public Transform ProjectileSpawn;

    public float ChargeTime;
    public int RangedDamage;

    LineRenderer laser;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        laser = ProjectileSpawn.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!attacking)
            gameObject.transform.LookAt(Target.transform);

        if (Vector3.Distance(Target.transform.position, transform.position) > AttackDistance)
        {
            nav.destination = Target.transform.position;
            nav.isStopped = false;
        }
        else if (!attacking)
        {
            RaycastHit hit;

            if (Physics.Raycast(ProjectileSpawn.localPosition, Target.transform.position, out hit, AttackDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    attacking = true;
                    nav.isStopped = true;
                    StartCoroutine(Attack(hit));
                }
            }

        }

    }

    IEnumerator Attack(RaycastHit pHit)
    {
        laser.positionCount = 2;
        laser.SetPosition(0, ProjectileSpawn.position);
        laser.SetPosition(1, pHit.point);
        laser.startWidth = 0.2f;
        laser.endWidth = 0.2f;
        MasterAudio.PlaySound3DAtTransformAndForget("ChargeFire", ProjectileSpawn.transform);
        yield return new WaitForSeconds(ChargeTime);
        laser.startWidth = 1f;
        laser.endWidth = 1f;

        RaycastHit hit;
        if (Physics.Raycast(laser.GetPosition(0), laser.GetPosition(1), out hit, AttackDistance))
        {
            if (hit.collider.CompareTag("Player"))
                hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(RangedDamage);
        }

        float cd = Random.Range(AttackCD, AttackCD+2f);

        yield return new WaitForSeconds(cd);
        laser.positionCount = 0;
        attacking = false;


    }
}
