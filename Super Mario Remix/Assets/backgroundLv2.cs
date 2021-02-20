using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundLv2 : MonoBehaviour
{
    public AudioClip backgroundlv2;
    public AudioSource soundSource;
    // Start is called before the first frame update
    void Start()
    {
        soundSource.clip = backgroundlv2;
        soundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
