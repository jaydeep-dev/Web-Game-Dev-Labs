using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerController : MonoBehaviour
{
    [SerializeField] private Transform markerTarget;

    private void LateUpdate()
    {
        if(markerTarget == null) return;

        var targetPos = new Vector3(markerTarget.position.x, transform.position.y, markerTarget.position.z);

        transform.position = targetPos;
        //transform.position  = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
    }
}
