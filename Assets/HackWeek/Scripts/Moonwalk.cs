using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moonwalk : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    public float spd = 0.1f;
    public float progress = 0;
    public bool turnAround;
    public bool walking;
    public Quaternion currentRotation;
    public Quaternion destinationRotation;
    public bool backwards;
    // Start is called before the first frame update
    void Start()
    {
        walking = true;
        start = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //walk to direction
        if (walking)
        {
            transform.localPosition = Vector3.Lerp(start, end, progress);

            progress += spd * Time.fixedDeltaTime;
            //if you reached the end of the pass
            if (progress >= 1)
            {
                progress = 0;
                end = start;
                start = transform.localPosition;
                turnAround = true;
                walking = false;
                currentRotation = transform.localRotation;
                float y = !backwards ? 0f : 180f;
                destinationRotation = Quaternion.Euler(0, y,0);
                backwards = !backwards;
            }
        }
        //turn around
        if (turnAround)
        {
            transform.localRotation = Quaternion.Lerp(currentRotation, destinationRotation, progress);

            progress += spd * Time.fixedDeltaTime;
            //if you reached the end of the pass
            if (progress >= 1)
            {
                progress = 0;
                turnAround = false;
                walking = true;
            }
        }

    }
}
