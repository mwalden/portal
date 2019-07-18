using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterChanger : MonoBehaviour
{
    public AudioScript audioScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        audioScript.canChangeTrack = true;
    }
}
