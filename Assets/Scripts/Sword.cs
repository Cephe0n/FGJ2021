using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class Sword : MonoBehaviour
{

    Animator anim;

    public ParticleSystem EnemyHit, WallHit;

    public int Damage;

    PlayerHealth playerHP;


    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.FindObjectOfType<PlayerHealth>();
        anim = transform.GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SwordBounceObj"))
        {
            WallHit.Play();
            MasterAudio.PlaySound3DAtTransformAndForget("SwordClash", transform);
            anim.SetTrigger("HitBounce");
        }
        else if (other.gameObject.CompareTag("EnemyHitSpot"))
        {
            EnemyHit.Play();
            MasterAudio.PlaySound3DAtTransformAndForget("SwordHit1", transform);
            other.GetComponent<Enemies>().TakeDamage(Damage);
            if (playerHP.Health < playerHP.maxHealth)
                playerHP.Health += 1;
        }

    }
}
