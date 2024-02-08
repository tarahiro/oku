using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostitPhysics : MonoBehaviour, IRaycastReciever
{
    PostitPhysicsController controller;
    [SerializeField] Transform rootTransform;
    public PostitPhysicsController.PostitPhysicsState state { get;private set; }
    public Report m_report;

    private void Awake()
    {
        controller = FindObjectOfType<PostitPhysicsController>();
        state = PostitPhysicsController.PostitPhysicsState.Standby;
    }

    public void RaycastAct(PlayerInputReciever.TTouchState ttouchState)
    {
        controller.NoticeRaycast(this,ttouchState);
        Debug.Log("Raycast");
    }

    public void Move(Vector3 t_raycastPoint)
    {
        SetPosition(t_raycastPoint);
        state = PostitPhysicsController.PostitPhysicsState.Moving;
        Debug.Log("Moving");
    }

    public void SetPosition(Vector3 t_raycastPoint)
    {
        rootTransform.position = t_raycastPoint;
        Debug.Log("SetPosition");
    }
}
