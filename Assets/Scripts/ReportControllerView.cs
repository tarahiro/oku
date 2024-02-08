using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReportControllerView : MonoBehaviour
{
    const float postitSizeY = .04f;
    const float merginY = .02f;

    [SerializeField] ReportView reportViewPrefab;

    Vector3 m_raycastPoint;
    List<ReportView> m_reportViewList;

    private void Awake()
    {
        m_reportViewList = new List<ReportView>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void NoticeRaycast(PostitPhysics postitPhysics)
    {
        if (StaticVariableCollector.tTouchState.IsTouchDown)
        {
            switch (postitPhysics.state)
            {
                case PostitPhysicsState.Standby:
                    //‘¼‚É“®‚¢‚Ä‚¢‚é‚à‚Ì‚ª‚È‚¯‚ê‚ÎA“®‚©‚·
                    if (!m_reportViewList.Any(x => x.postitPhysics.state == PostitPhysicsState.Moving))
                    {
                        postitPhysics.Move(m_raycastPoint);
                    }
                    else
                    {
                        SwitchReport(m_reportViewList.First(x => x.postitPhysics.state == PostitPhysicsState.Moving).postitPhysics, postitPhysics);
                    }
                    break;
            }
        }
        else
        {

        }
    }

    public void ReflectMouseInput()
    {
        for (int i = 0; i < m_reportViewList.Count; i++)
        {
            switch (m_reportViewList[i].postitPhysics.state)
            {
                case PostitPhysicsState.Moving:
                    if (StaticVariableCollector.tTouchState.IsTouchIn)
                    {
                        m_reportViewList[i].postitPhysics.SetPositionFromRaycast(m_raycastPoint);
                    }
                    else if (StaticVariableCollector.tTouchState.IsTouchUp)
                    {
                        m_reportViewList[i].postitPhysics.Standby(PostitNewtralLocalPosition(i));
                    }
                    break;
            }
        }
    }

    public void SetRaycastPointOnPlane(Vector3 raycastPoint)
    {
        m_raycastPoint = raycastPoint;
    }

    public ReportView AddReport(string reportName, DateTime deadLine, Color color)
    {
        m_reportViewList.Add(Instantiate(reportViewPrefab, transform));
        m_reportViewList[m_reportViewList.Count - 1].postitPhysics.SetPosition(PostitNewtralLocalPosition(m_reportViewList.Count - 1));
        m_reportViewList[m_reportViewList.Count - 1].InitializeReport(reportName, deadLine, color);

        return m_reportViewList[m_reportViewList.Count - 1];
    }

    public void SwitchReport(PostitPhysics movingPostit, PostitPhysics standbyPostit)
    {
        for(int i = 0; i < m_reportViewList.Count;i++)
        {
            if (m_reportViewList[i].postitPhysics == movingPostit)
            {
                for (int j = 0; j < m_reportViewList.Count; j++)
                {
                    if (m_reportViewList[j].postitPhysics == standbyPostit)
                    {
                        ReportView t_reportView = m_reportViewList[j];
                        m_reportViewList[j] = m_reportViewList[i];
                        m_reportViewList[j].postitPhysics.Standby(PostitNewtralLocalPosition(j));

                        m_reportViewList[i] = t_reportView;
                        m_reportViewList[i].postitPhysics.Standby(PostitNewtralLocalPosition(i));

                        return;
                    }
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

    Vector3 PostitNewtralLocalPosition(int reportCountId)
    {
        return Vector3.down * reportCountId * (postitSizeY + merginY);
    }


    public void Clear()
    {

        m_reportViewList.RemoveAt(0);
    }
}
