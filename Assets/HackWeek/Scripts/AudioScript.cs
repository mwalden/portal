using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{

    public AudioSource source;

    public int idx;

    public AudioClip[] clips;

    //daft,dmx,baby,jungle
    public float timeBeforeStarting;

    public float minPauseBetweenSongs;

    public float timeRemainingBetweenSongs = -1;

    public bool started;
    //changing value in EnterChanger.cs
    public bool canChangeTrack;

    public bool playJungle;

    public bool startedPlayingJungle;

    public bool spotLightsOn;

    public bool areaLightOn;

    public Text triggerText;

    public Light areaLight;

    public Light[] spotLights;


    public float timeBeforeSpotLights;
    public float timeBeforeAreaLights;


    // Start is called before the first frame update
    void Start()
    {
        source.clip = clips[0];
        foreach (Light l in spotLights)
        {
            l.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        triggerText.text = canChangeTrack.ToString() + " :: " + timeRemainingBetweenSongs + " :: " + idx;
        if (startedPlayingJungle)
        {
            if (timeBeforeSpotLights > 0)
            {
                timeBeforeSpotLights -= Time.deltaTime;
                return;
            }
            if (!spotLightsOn)
            {
                spotLightsOn = true;
                foreach (Light l in spotLights)
                {
                    l.gameObject.SetActive(true);
                }
            }
            if (timeBeforeAreaLights > 0)
            {
                timeBeforeAreaLights -= Time.deltaTime;
                return;
            }
            if (!areaLightOn)
            {
                areaLight.gameObject.SetActive(true);
                areaLightOn = true;
            }

            return;
        }

        if (playJungle)
        {
            foreach (Light l in spotLights)
            {
                l.gameObject.SetActive(false);

            }
            areaLight.gameObject.SetActive(false);
            startedPlayingJungle = true;
        }
        if (timeRemainingBetweenSongs > 0)
            timeRemainingBetweenSongs -= Time.deltaTime;

        if (Input.touchCount == 1 && timeRemainingBetweenSongs < 0 && canChangeTrack)
        {



            if (clips.Length - 1 > idx)
            {
                timeRemainingBetweenSongs = minPauseBetweenSongs;
                idx++;
                source.clip = clips[idx];
                source.Play();
            }
            if (clips[idx].Equals(clips[3]))
            {
                playJungle = true;
            }
        }





        if (started)
            return;
        timeBeforeStarting -= Time.deltaTime;
        if (timeBeforeStarting < 0)
        {
            started = true;
            source.Play();
        }
    }

}
