using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAdjustment : MonoBehaviour
{
    public AudioSource source;


    public void OnTriggerEnter(Collider other)
    {
        source.volume = 1f;
    }
}
