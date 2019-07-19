using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBetweenTwoPoints : MonoBehaviour
{
    public Vector3 targetEulerAngles;

    private Quaternion start;
    private Quaternion end;
    public float spd = 0.1f;

    bool reverse;

    private Quaternion startRotation;
    public  float progress = 0;
    void Start()
    {
        start = transform.localRotation;
        end = Quaternion.Euler(targetEulerAngles);
    }

    void Update()
    {
        transform.localRotation = Quaternion.Lerp(start, end, progress);

        progress += spd * Time.fixedDeltaTime;
        if (progress >= 1)
        {
            reverse = true;
            progress = 0;
            end = start;
            start = transform.localRotation;
        }
    }
}
