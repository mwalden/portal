using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;


#if UNITY_EDITOR

#endif
using input = GoogleARCore.InstantPreviewInput;
public class ARController : MonoBehaviour
{
    private List<TrackedPlane> planes = new List<TrackedPlane>();
    public GameObject grid;
    public GameObject door;
    public bool enabledDoor;
    public GameObject arCamera;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      


        if (!Session.Status.Equals(SessionStatus.Tracking))
        {
            return;
        }
        Session.GetTrackables<TrackedPlane>(planes, TrackableQueryFilter.New);

        foreach (TrackedPlane plane in planes)
        {
            GameObject go = Instantiate(grid, Vector3.zero, Quaternion.identity, transform);
            go.GetComponent<GridVisualizer>().Initialize(plane);
        }
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began){
            return;
        }
        TrackableHit hit;

        if (Frame.Raycast(touch.position.x,touch.position.y, TrackableHitFlags.PlaneWithinPolygon,out hit))
        {
            enabledDoor = true;
            door.SetActive(true);

            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

            door.transform.position = hit.Pose.position;
            door.transform.rotation = hit.Pose.rotation;

            Vector3 cameraPosition = arCamera.transform.position;
            //portal should rotate around y axis

            cameraPosition.y = hit.Pose.position.y;

            door.transform.LookAt(cameraPosition, door.transform.up);

            door.transform.parent = anchor.transform;
        }
    }
}
