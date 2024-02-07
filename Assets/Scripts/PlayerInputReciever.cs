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

    IRaycastReciever raycastReciever = null;
    TTouchState tTouchState;

    public List<KeyCode> m_keyDownKeyList { get; private set; }
    public List<KeyCode> m_keyInKeyList { get; private set; }
    public List<KeyCode> m_keyUpKeyList { get; private set; }

    private void Awake()
    {
        tTouchState = new TTouchState(null);
    }

    private void FixedUpdate()
    {
        raycastReciever = null;
        StaticVariableCollector.SetMousePosition(Input.mousePosition);

        //ƒXƒ}ƒz‘Î‰ž‚·‚éŽž‚É•Ï‚¦‚é
        Ray ray = Camera.main.ScreenPointToRay(StaticVariableCollector.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100)) {
            if (hit.transform.GetComponent<IRaycastReciever>() != null)
            {
                raycastReciever = hit.transform.GetComponent<IRaycastReciever>();
            }
        }

    }


    public void RecieveKeyInput()
    {
        m_keyDownKeyList = m_useKeyList.FindAll(x => Input.GetKeyDown(x));
        m_keyInKeyList = m_useKeyList.FindAll(x => Input.GetKey(x));
        m_keyUpKeyList = m_useKeyList.FindAll(x => Input.GetKeyUp(x));
        tTouchState = new TTouchState(tTouchState);
    }

    public void RayCastExecute()
    {
        if(raycastReciever != null) raycastReciever.RaycastAct(tTouchState);
    }

    public class TTouchState
    {
        public bool IsTouchDown;
        public bool IsTouchIn;
        public bool IsTouchUp;

        public TTouchState(TTouchState m_prevTouchState)
        {
            if(m_prevTouchState == null)
            {
                IsTouchDown = false;
                IsTouchIn = false;
                IsTouchUp = false;
            }
            else
            {
                if (!IsTouchIn)
                {
                    IsTouchDown = Input.GetMouseButton(0);
                    IsTouchIn = Input.GetMouseButton(0);
                    IsTouchUp = false;
                }
                else
                {
                    IsTouchDown = false;
                    IsTouchIn = Input.GetMouseButton(0);
                    IsTouchUp = Input.GetMouseButton(0);
                }
            }
        }
    }
}
