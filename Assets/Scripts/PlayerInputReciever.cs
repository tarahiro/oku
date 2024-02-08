using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInputReciever : MonoBehaviour
{
    readonly List<KeyCode> m_useKeyList = new List<KeyCode>()
    {
        KeyCode.Z,
        KeyCode.Escape,
        KeyCode.Mouse0
    };

    PlayerControllerView m_controllerView;

    List<RaycastHit> m_raycastHitList;
    
    TTouchState tTouchState;

    public List<KeyCode> m_keyDownKeyList { get; private set; }
    public List<KeyCode> m_keyInKeyList { get; private set; }
    public List<KeyCode> m_keyUpKeyList { get; private set; }

    private void Awake()
    {
        Debug.Log("PlayerInputReciever");
        tTouchState = new TTouchState(null);
    }

    private void FixedUpdate()
    {
        StaticVariableCollector.SetMousePosition(Input.mousePosition);

        //スマホ対応する時に変える
        Ray ray = Camera.main.ScreenPointToRay(StaticVariableCollector.mousePosition);
        m_raycastHitList = Physics.RaycastAll(ray, 100f).ToList();
    }


    public void RecieveKeyInput()
    {
        tTouchState = new TTouchState(tTouchState);
        m_keyDownKeyList = m_useKeyList.FindAll(x => Input.GetKeyDown(x));
        m_keyInKeyList = m_useKeyList.FindAll(x => Input.GetKey(x));
        m_keyUpKeyList = m_useKeyList.FindAll(x => Input.GetKeyUp(x));
    }

    public void RayCastExecute()
    {
        if (m_raycastHitList != null)
        {
            //優先して処理するものを対応
            for (int i = m_raycastHitList.Count - 1; i >= 0; i--)
            {
                if (m_raycastHitList[i].transform.GetComponent<IRaycastPointGetter>() != null)
                {
                    m_raycastHitList[i].transform.GetComponent<IRaycastPointGetter>().SetRaycastPoint(m_raycastHitList[i].point);
                    //パフォーマンスのため削除
                    m_raycastHitList.RemoveAt(i);
                }
            }

            //その他の処理
            for (int i = m_raycastHitList.Count - 1; i >= 0; i--)
            {
                if (m_raycastHitList[i].transform.GetComponent<IRaycastReciever>() != null)
                {
                    m_raycastHitList[i].transform.GetComponent<IRaycastReciever>().RaycastAct(tTouchState);
                }
            }
        }
    }

    public class TTouchState
    {
        public bool IsTouchDown;
        public bool IsTouchIn;
        public bool IsTouchUp;

        public TTouchState(TTouchState m_prevTouchState)
        {
            if (Input.GetMouseButton(0)) Debug.Log("MouseButtonIn");
            if(m_prevTouchState == null)
            {
                IsTouchDown = false;
                IsTouchIn = false;
                IsTouchUp = false;
            }
            else
            {
                if (!m_prevTouchState.IsTouchIn)
                {
                    IsTouchDown = Input.GetMouseButton(0);
                    IsTouchIn = Input.GetMouseButton(0);
                    IsTouchUp = false;
                }
                else
                {
                    IsTouchDown = false;
                    IsTouchIn = Input.GetMouseButton(0);
                    IsTouchUp = !Input.GetMouseButton(0);
                }
            }
        }
    }
}
