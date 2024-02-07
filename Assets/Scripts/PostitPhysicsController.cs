using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PostitPhysicsController : MonoBehaviour
{
    List<PostitPhysics> m_postitPhysicsList = new List<PostitPhysics>();

    public void NoticeRaycast(PostitPhysics postitPhysics, PlayerInputReciever.TTouchState ttouchState)
    {
        if (ttouchState.IsTouchDown)
        {
            switch (postitPhysics.state)
            {
                case PostitPhysicsState.Standby:
                    //���ɓ����Ă�����̂��Ȃ���΁A������
                    if (!m_postitPhysicsList.Any(x => x.state == PostitPhysicsState.Moving))
                    {
                        postitPhysics.Move();
                    }
                    else
                    {
                        //TODO ���ɓ����Ă�����̂�����΁A����ƌ���
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
                        postitPhysics.SetPosition();
                        break;

                }
            }
        }
    }

    public enum PostitPhysicsState
    {
        None,
        Standby,
        Moving
    }
}
