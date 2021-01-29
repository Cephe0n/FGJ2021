using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public Animator SwordAnim;

    public float SwingCD;

    BoxCollider swordCollider;

    bool swingReady = true;

    TrailRenderer swordTrail;


    private void Start()
    {
        swordCollider = transform.GetChild(0).GetComponent<BoxCollider>();
        swordTrail = transform.GetChild(0).GetComponent<TrailRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && swingReady)
        {
            swingReady = false;
            SwordAnim.SetTrigger("HorSwing");
            
            StartCoroutine(ResetAnim());
        }
    }

    IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(SwingCD);
        swingReady = true;

    }

    void ToggleHitBox()
    {
        swordCollider.enabled = !swordCollider.enabled;
        swordTrail.emitting = !swordTrail.emitting;
    }

}
