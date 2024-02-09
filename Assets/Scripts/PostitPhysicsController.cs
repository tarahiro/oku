using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ReportControllerView;

public class PostitPhysicsController : MonoBehaviour
{
    const float postitSizeY = .04f;
    const float merginY = .02f;


    ReportController m_reportController;
    ReportControllerView m_reportControllerView;

    Vector3 m_raycastPoint;

    private void Awake()
    {
        m_reportController = FindObjectOfType<ReportController>();
        m_reportControllerView = FindObjectOfType<ReportControllerView>();
    }

    public void NoticeRaycast(PostitPhysics postitPhysics)
    {
        if (StaticVariableCollector.tTouchState.IsTouchDown)
        {
            switch (postitPhysics.state)
            {
                case PostitPhysicsState.Standby:
                    //‘¼‚É“®‚¢‚Ä‚¢‚é‚à‚Ì‚ª‚È‚¯‚ê‚ÎA“®‚©‚·
                    if (!m_reportControllerView.reportViewList.Any(x => x.postitPhysics.state == PostitPhysicsState.Moving))
                    {
                        postitPhysics.Move(m_raycastPoint);
                    }
                    break;
            }
        }
        else if (StaticVariableCollector.tTouchState.IsTouchIn)
        {
            switch (postitPhysics.state)
            {
                case PostitPhysicsState.Standby:
                    if (m_reportControllerView.reportViewList.Any(x => x.postitPhysics.state == PostitPhysicsState.Moving))
                    {
                        SwitchReport(m_reportControllerView.reportViewList.First(x => x.postitPhysics.state == PostitPhysicsState.Moving).postitPhysics, postitPhysics);
                    }
                    break;
            }
        }
    }

    public void ReflectMouseInput()
    {
        for (int i = 0; i < m_reportControllerView.reportViewList.Count; i++)
        {
            switch (m_reportControllerView.reportViewList[i].postitPhysics.state)
            {
                case PostitPhysicsState.Moving:
                    if (StaticVariableCollector.tTouchState.IsTouchIn)
                    {
                        m_reportControllerView.reportViewList[i].postitPhysics.SetPositionFromRaycast(m_raycastPoint);
                    }
                    else if (StaticVariableCollector.tTouchState.IsTouchUp)
                    {
                        m_reportControllerView.reportViewList[i].postitPhysics.Standby(PostitNewtralLocalPosition(i));
                    }
                    break;
            }
        }
    }

    public void SetRaycastPointOnPlane(Vector3 raycastPoint)
    {
        m_raycastPoint = raycastPoint;
    }

    public void SwitchReport(PostitPhysics movingPostit, PostitPhysics standbyPostit)
    {
        for (int i = 0; i < m_reportControllerView.reportViewList.Count; i++)
        {
            if (m_reportControllerView.reportViewList[i].postitPhysics == movingPostit)
            {
                for (int j = 0; j < m_reportControllerView.reportViewList.Count; j++)
                {
                    if (m_reportControllerView.reportViewList[j].postitPhysics == standbyPostit)
                    {
                        m_reportController.SwitchReport(i, j);
                        return;
                    }
                }
            }
        }
    }

    public void SetPostitPosition(int index)
    {
        m_reportControllerView.reportViewList[index].postitPhysics.SetPosition(PostitNewtralLocalPosition(index));
    }

    public enum PostitPhysicsState
    {
        None,
        Standby,
        Moving
    }

    Vector3 PostitNewtralLocalPosition(int reportCountId)
    {
        return Vector3.down * reportCountId * (postitSizeY + merginY);
    }

}
