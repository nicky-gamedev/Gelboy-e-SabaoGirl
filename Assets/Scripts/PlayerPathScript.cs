﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerPathScript : MonoBehaviour
{
    private Seeker seeker;
    private CharacterController controller;
    public Path path;

    public float speed = 2;
    public float nextWaypointDistance = 3;
    public bool reachedEndOfPath;
    private int currentWaypoint = 0;

    public void Awake()
    {
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
    }

    public void ResetPath()
    {
        path = null;
        speed = 2;
        nextWaypointDistance = 3;
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Caminho calculado, erros: " + p.error);

        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void NewPath(Vector3 newTarget)
    {
        seeker.StartPath(transform.position, newTarget, OnPathComplete);
    }

    public void Update()
    {
        if (path == null)
        {
            return;
        }

        reachedEndOfPath = false;
        float distanceToWaypoint;
        while (true)
        {
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Vector3 velocity = dir * speed * speedFactor;
        controller.SimpleMove(velocity);
    }
}
