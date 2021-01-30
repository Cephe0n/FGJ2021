using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using DarkTonic.MasterAudio;

public class Dash : MonoBehaviour
{
    FirstPersonAIO fp;
    Volume PPVol;
    Vignette vignette;
    public float DashStrength, DashCooldown, DashDuration;
    bool dashReady = true;
    // Start is called before the first frame update
    void Start()
    {
        PPVol = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Volume>();
        VolumeProfile profile = PPVol.sharedProfile;
        PPVol.profile.TryGet(out vignette);

        fp = gameObject.GetComponent<FirstPersonAIO>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashReady)
        {
            vignette.color.value = Color.cyan;

            Sequence seq = DOTween.Sequence();
            seq.Append(DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.5f, 0.2f));
            seq.Append(DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0f, 1f));

            fp.walkSpeed = DashStrength;
            MasterAudio.PlaySound3DAtTransformAndForget("PlayerDash", transform);
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
