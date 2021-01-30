using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class Punch : MonoBehaviour
{
    public float KnockbackAmount;
    public int Damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyHitSpot"))
        {
            MasterAudio.PlaySound3DAtTransformAndForget("PunchHit1", transform);
        other.GetComponent<Rigidbody>().AddForce(other.transform.position * KnockbackAmount, ForceMode.Impulse);
        other.GetComponent<Enemies>().TakeDamage(Damage);
        }
    }
}
