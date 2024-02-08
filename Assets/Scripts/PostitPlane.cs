using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostitPlane : MonoBehaviour,IRaycastPointGetter
{

    ReportControllerView controller;
    private void Awake()
    {
        controller = FindObjectOfType<ReportControllerView>();
    }

    public void SetRaycastPoint(Vector3 raycastPoint)
    {
        controller.SetRaycastPointOnPlane(raycastPoint);
    }
}
