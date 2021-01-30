using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class PlayerAttacks : MonoBehaviour
{
    public Animator SwordAnim, FistAnim;

    public float SwingCD, FistCD;

    public GameObject Sword;

    BoxCollider swordCollider;

    bool swingReady = true;

    TrailRenderer swordTrail;


    private void Start()
    {
        swordCollider = Sword.GetComponent<BoxCollider>();
        swordTrail = Sword.GetComponent<TrailRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (swingReady)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swingReady = false;
                MasterAudio.PlaySound3DAtTransformAndForget("SwordWhoosh1", transform);
                SwordAnim.SetTrigger("HorSwing");

                StartCoroutine(ResetSwing(SwingCD));
            }

            if (Input.GetMouseButtonDown(1))
            {
                swingReady = false;
                MasterAudio.PlaySound3DAtTransformAndForget("SwordWhoosh1", transform);
                SwordAnim.SetTrigger("VerSwing");

                StartCoroutine(ResetSwing(SwingCD));
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                swingReady = false;
                MasterAudio.PlaySound3DAtTransformAndForget("PunchWhoosh1", transform);
                FistAnim.SetTrigger("Punch");
                StartCoroutine(ResetSwing(FistCD));
            }
        }
    }

    IEnumerator ResetSwing(float pCD)
    {
        yield return new WaitForSeconds(pCD);
        swingReady = true;

    }

    void ToggleHitBox()
    {
        swordCollider.enabled = !swordCollider.enabled;
        swordTrail.emitting = !swordTrail.emitting;
    }

}
