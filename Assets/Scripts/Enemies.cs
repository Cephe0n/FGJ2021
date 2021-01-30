using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DarkTonic.MasterAudio;

public class Enemies : MonoBehaviour
{
    public int Health;

    public float AttackCD, AttackDistance;

    public GameObject Target, WeaponHitbox;

    protected Animator anim;

    protected NavMeshAgent nav;
    protected bool attacking;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected virtual void Attack()
    {
        nav.isStopped = true;
        attacking = true;
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttack());
    }

    protected IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(AttackCD);
        attacking = false;
    }

    public void TakeDamage(int pDmg)
    {
        Health -= pDmg;
        if (Health <= 0)
            Death();

    }

    void Death()
    {
        MasterAudio.PlaySound3DAtTransformAndForget("DeathMeleeEnemy", transform);
        Destroy(gameObject, 1f);
    }
}
