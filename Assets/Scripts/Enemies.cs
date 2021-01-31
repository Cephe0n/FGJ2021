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
        Target = GameObject.FindGameObjectWithTag("Player");
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

    protected virtual void Death()
    {
        GameControl.Instance.EnemiesKilled++;
        MasterAudio.StopAllSoundsOfTransform(transform);
        MasterAudio.PlaySound3DAtTransformAndForget("DeathMeleeEnemy", transform);
        gameObject.SetActive(false);
    }
}
