﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour, IPooledObject
{
    // LineRenderer instance (just line).
    public LineRenderer line;

    // Elements to which the line is attached.
    public Rectangle begin;
    public Rectangle end;

    // Object to handle line's collider.
    public GameObject lineCollider;


    // OnObjectSpawn is called when the object is spawned.
    public void OnObjectSpawn()
    {
        // Get LineRenderer component and initialize the line.
        line = GetComponent<LineRenderer>();

        // Create and set new color to the line
        Color newColor = new Color(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
        line.startColor= newColor;
        line.endColor = newColor;

        lineCollider = transform.GetChild(0).gameObject;
    }

    // UpdateLinePointPosition changes the position of some point of the line.
    public void UpdateLinePointPosition(int pointIndex, Vector3 newPosition)
    {
        // If the line exists, then set new point position.
        if (line != null)
        {
            line.SetPosition(pointIndex, newPosition);
        }
    }

    // DeleteLinePoint detaching line points from the elements by point index.
    public void DeleteLinePoint(int linePointIndex)
    {
        if (linePointIndex == 0)
        {
            begin.connections.Remove(this);
        }
        if (linePointIndex == 1)
        {
            end.connections.Remove(this);
        }
    }

    // Update line's collider position and rotation.
    public void DrawCollider()
    {
        // Setting scale and position of the collider.
        Vector3 startPos = line.GetPosition(0);
        Vector3 endPos = line.GetPosition(1);
        float lineLength = Vector3.Distance(startPos, endPos);
        lineCollider.transform.localScale = new Vector3(lineLength, line.startWidth, 0f); 
        Vector3 midPoint = (startPos + endPos) / 2;
        lineCollider.transform.position = midPoint;
        
        // Following lines calculate the angle between startPos and endPos.
        float angle = (Mathf.Abs(startPos.y - endPos.y) / Mathf.Abs(startPos.x - endPos.x));
        if ((startPos.y < endPos.y && startPos.x > endPos.x) || (endPos.y < startPos.y && endPos.x > startPos.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);

        lineCollider.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
