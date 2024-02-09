using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostitPlane : MonoBehaviour,IRaycastPointGetter
{

    PostitPhysicsController controller;
    private void Awake()
    {
        controller = FindObjectOfType<PostitPhysicsController>();
    }

    public void SetRaycastPoint(Vector3 raycastPoint)
    {
        controller.SetRaycastPointOnPlane(raycastPoint);
    }
}
