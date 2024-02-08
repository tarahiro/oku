using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostitPhysics : MonoBehaviour, IRaycastReciever
{
    ReportControllerView controller;
    [SerializeField] Transform rootTransform;
    public ReportControllerView.PostitPhysicsState state { get;private set; }
    public Report m_report;

    private void Awake()
    {
        controller = FindObjectOfType<ReportControllerView>();
        state = ReportControllerView.PostitPhysicsState.Standby;
    }

    public void RaycastAct()
    {
        controller.NoticeRaycast(this);
    }

    public void Move(Vector3 t_raycastPoint)
    {
        SetPositionFromRaycast(t_raycastPoint);
        state = ReportControllerView.PostitPhysicsState.Moving;
    }


    public void SetPosition (Vector3 localPosition)
    {
        rootTransform.localPosition = localPosition;
    }

    public void SetPositionFromRaycast(Vector3 t_raycastPoint)
    {
        rootTransform.position = t_raycastPoint;
    }
}
