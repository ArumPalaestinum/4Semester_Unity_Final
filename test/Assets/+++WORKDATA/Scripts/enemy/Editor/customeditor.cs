using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//has access to the field of view script?
[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    //method that work as ui
    private void OnSceneGUI()
    {
        //ref for fieldofview
        FieldOfView fov = (FieldOfView)target;

        //color
        Handles.color = Color.white;

        //wireac for radius
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    //viewing angle
    private Vector3 DirectionFromAngle(float eu1erY, float angleInDegrees)
    {
        angleInDegrees += eu1erY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}