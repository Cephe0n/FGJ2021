using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemies
{

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
            base.Attack();
    }
}
