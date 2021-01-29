using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    FirstPersonAIO fp;
    public float DashStrength, DashCooldown, DashDuration;
    bool dashReady = true;
    // Start is called before the first frame update
    void Start()
    {
        fp = gameObject.GetComponent<FirstPersonAIO>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.LeftShift) && dashReady)
        {
           fp.walkSpeed = DashStrength;
           StartCoroutine(DashCD());  
        }
    }


    IEnumerator DashCD()
    {
        yield return new WaitForSeconds(DashDuration);
        dashReady = false;
        fp.walkSpeed = 10;
        yield return new WaitForSeconds(DashCooldown);
        dashReady = true;
        
    }
}
