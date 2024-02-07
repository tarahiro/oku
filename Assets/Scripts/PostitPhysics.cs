using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostitPhysics : MonoBehaviour, IRaycastReciever
{
    PostitPhysicsController controller;
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

    public void Move()
    {
        SetPosition();
        state = PostitPhysicsController.PostitPhysicsState.Moving;
        Debug.Log("Moving");
    }

    public void SetPosition()
    {
        Vector3 correctedMousePosition = StaticVariableCollector.mousePosition;
        correctedMousePosition.z = transform.position.z;
        transform.position = Camera.main.ScreenToWorldPoint(correctedMousePosition);
        Debug.Log("SetPosition");
    }
}
