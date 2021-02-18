using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioplay_background : MonoBehaviour
{
    public AudioClip backgroundlv1;
    public AudioSource soundSource;
    // Start is called before the first frame update
    void Start()
    {
        soundSource.clip = backgroundlv1;
        soundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {


    }
}
