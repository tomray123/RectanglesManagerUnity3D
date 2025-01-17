﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour, IPooledObject
{
    // Sprite of the object.
    private SpriteRenderer sprite;
    // Dictionary which contains connection lines and their points connected to the rectangle.
    public Dictionary<Line, int> connections;

    void Start()
    {
        connections = new Dictionary<Line, int>();
    }

    // OnObjectSpawn is called when the object is spawned.
    public void OnObjectSpawn()
    {
        // Get SpriteRenderer component and initialize the sprite.
        sprite = GetComponent<SpriteRenderer>();
        // Create and set new color to the sprite
        Color newColor = new Color(Random.Range(0.0f, 1f), Random.Range(0.0f, 1f), Random.Range(0.0f, 1f));
        sprite.color = newColor;
    }

    // Update is called once per frame.
    public void Update()
    {
        // Updating coordinates and collider positions of attached lines.
        foreach (Line line in connections.Keys)
        {
            line.UpdateLinePointPosition(connections[line], transform.position);
            line.DrawCollider();
        }
    }
}
