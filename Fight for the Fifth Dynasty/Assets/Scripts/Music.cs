using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public AudioSource track1;
    public AudioSource track2;
    public AudioSource track3;

    float checkTimer = 5;

    void Update()
    {
        if (!track1.isPlaying && !track2.isPlaying && !track3.isPlaying){

            int selectedTrack = Random.Range(1,4);

            if (selectedTrack == 1){
                track1.Play();
            }
            else if (selectedTrack == 2){
                track2.Play();
            }
            else if (selectedTrack == 3){
                track3.Play();
            }
        }
    }
}
