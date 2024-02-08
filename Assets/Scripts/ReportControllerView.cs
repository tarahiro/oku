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

    public void NoticeRaycast(PostitPhysics postitPhysics, PlayerInputReciever.TTouchState ttouchState)
    {
        if (ttouchState.IsTouchDown)
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
                        //TODO ‘¼‚É“®‚¢‚Ä‚¢‚é‚à‚Ì‚ª‚ ‚ê‚ÎA‚»‚ê‚ÆŒðŠ·
                    }
                    break;
            }
        }
        else
        {

        }
    }

    public void SetRaycastPointOnPlane(Vector3 raycastPoint)
    {
        Debug.Log("a");
        m_raycastPoint = raycastPoint;
        foreach (ReportView p in m_reportViewList.FindAll(x => x.postitPhysics.state == PostitPhysicsState.Moving))
        {
            p.postitPhysics.SetPosition(raycastPoint);
        }
    }

    public ReportView AddReport(string reportName, DateTime deadLine, Color color)
    {
        m_reportViewList.Add(Instantiate(reportViewPrefab, transform));

        Transform addedReportTransform = m_reportViewList[m_reportViewList.Count - 1].transform;
        addedReportTransform.localPosition = Vector3.down * (m_reportViewList.Count - 1) * (postitSizeY + merginY);

        m_reportViewList[m_reportViewList.Count - 1].InitializeReport(reportName, deadLine, color);

        return m_reportViewList[m_reportViewList.Count - 1];
    }

    public enum PostitPhysicsState
    {
        None,
        Standby,
        Moving
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Clear()
    {
        m_reportViewList.RemoveAt(0);
    }
}
