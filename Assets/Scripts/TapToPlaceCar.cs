using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlaceCar : MonoBehaviour
{
    public GameObject carPrefab;

    private GameObject spawnedObject;
    private ARRaycastManager arRaycastManager;
    private Vector2 touchPos;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPos))
        {
            return;
        }

        if (arRaycastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(carPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObject.transform.position = hitPose.position;
            }
        }
    }

    private bool TryGetTouchPosition(out Vector2 touchPos)
    {
        if(Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }
        else
        {
            touchPos = default;
            return false;
        }
    }
}
