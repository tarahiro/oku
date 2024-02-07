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
        tTouchState = new TTouchState();
    }

    private void FixedUpdate()
    {
        raycastReciever = null;
        tTouchState = new TTouchState();

        //ƒXƒ}ƒz‘Î‰ž‚·‚éŽž‚É•Ï‚¦‚é
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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

        public TTouchState()
        {
            IsTouchDown = Input.GetKeyDown(KeyCode.Mouse0);
            IsTouchIn = Input.GetKey(KeyCode.Mouse0);
            IsTouchUp = Input.GetKeyUp(KeyCode.Mouse0);
        }
    }
}
