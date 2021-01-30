using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class GameControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MasterAudio.PlaySound("begin");
        MasterAudio.PlaySound("AmbientWindLoop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
