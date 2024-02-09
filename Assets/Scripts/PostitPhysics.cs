using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostitPhysics : MonoBehaviour, IRaycastReciever
{
    PostitPhysicsController controller;
    Rigidbody m_rigidbody;
    [SerializeField] Transform rootTransform;
    public PostitPhysicsController.PostitPhysicsState state { get;private set; }
    public Report m_report;

    private void Awake()
    {
        controller = FindObjectOfType<PostitPhysicsController>();
        m_rigidbody = rootTransform.GetComponent<Rigidbody>();
        state = PostitPhysicsController.PostitPhysicsState.Standby;
    }

    public void RaycastAct()
    {
        controller.NoticeRaycast(this);
    }

    public void Move(Vector3 t_raycastPoint)
    {
        SetPositionFromRaycast(t_raycastPoint);
        state = PostitPhysicsController.PostitPhysicsState.Moving;
    }

    public void Standby(Vector3 localPosition)
    {
        SetPosition(localPosition);
        state = PostitPhysicsController.PostitPhysicsState.Standby;
    }


    public void SetPosition (Vector3 localPosition)
    {
        /*
        Vector3 t_localPosition = rootTransform.localPosition;
        rootTransform.localPosition = localPosition;
        Vector3 t_position = rootTransform.TransformPoint(localPosition);
        rootTransform.localPosition = t_localPosition;
        m_rigidbody.position = t_position;
        */
        m_rigidbody.position = rootTransform.parent.TransformPoint(localPosition);
    }

    public void SetPositionFromRaycast(Vector3 t_raycastPoint)
    {
        m_rigidbody.position = t_raycastPoint;
    }
}
