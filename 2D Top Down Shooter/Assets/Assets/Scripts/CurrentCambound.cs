using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCambound : MonoBehaviour
{
    private Collider2D cameraBounds;
    private CinemachineConfiner2D confiner;

    private void Start()
    {
        confiner = FindAnyObjectByType<CinemachineConfiner2D>();
        cameraBounds = GetComponentInChildren<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            confiner.m_BoundingShape2D = cameraBounds;
        }
    }

}
