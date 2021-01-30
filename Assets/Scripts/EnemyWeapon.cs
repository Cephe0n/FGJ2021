using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int Damage;
    public float Knockback;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        other.GetComponent<PlayerHealth>().TakeDamage(Damage);
        other.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position) * Knockback * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
