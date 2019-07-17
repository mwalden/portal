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
    // Start is called before the first frame update
    void Start()
    {
        start = transform.rotation;
        end = Quaternion.Euler(targetEulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(start, end, progress);

        progress += spd * Time.fixedDeltaTime;
        //if you reached the end of the pass
        if (progress >= 1)
        {
            reverse = true;
            progress = 0;
            end = start;
            start = transform.rotation;
        }
        //go back to the start
    }
}
