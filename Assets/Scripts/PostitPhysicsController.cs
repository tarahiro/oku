using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PostitPhysicsController : MonoBehaviour
{
    List<PostitPhysics> m_postitPhysicsList = new List<PostitPhysics>();
    Vector3 m_raycastPoint;

    public void NoticeRaycast(PostitPhysics postitPhysics, PlayerInputReciever.TTouchState ttouchState)
    {
        if (ttouchState.IsTouchDown)
        {
            switch (postitPhysics.state)
            {
                case PostitPhysicsState.Standby:
                    //他に動いているものがなければ、動かす
                    if (!m_postitPhysicsList.Any(x => x.state == PostitPhysicsState.Moving))
                    {
                        postitPhysics.Move(m_raycastPoint);
                    }
                    else
                    {
                        //TODO 他に動いているものがあれば、それと交換
                    }
                    break;
            }
        }
        else
        {
            if (ttouchState.IsTouchIn)
            {
                switch (postitPhysics.state)
                {
                    case PostitPhysicsState.Moving:
                        postitPhysics.SetPosition(m_raycastPoint);
                        break;

                }
            }
        }
    }

    public void SetRaycastPointOnPlane(Vector3 raycastPoint)
    {
        m_raycastPoint = raycastPoint;
    }

    public enum PostitPhysicsState
    {
        None,
        Standby,
        Moving
    }
}
