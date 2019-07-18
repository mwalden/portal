using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{

    public AudioSource source;

    public int idx;

    public AudioClip[] clips;

    //daft,dmx,baby,jungle
    public float timeBeforeStarting;

    public bool started;

    public bool canChangeTrack;




    // Start is called before the first frame update
    void Start()
    {
        source.clip = clips[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
            return;
        timeBeforeStarting -= Time.deltaTime;
        if (timeBeforeStarting < 0)
        {
            started = true;
            source.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canChangeTrack = true;
    }
}
