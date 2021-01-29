using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SwordBounceObj"))
        anim.SetTrigger("HitBounce");

        Debug.Log("asdsad");
    }
}
