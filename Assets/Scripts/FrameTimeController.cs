using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameTimeController : MonoBehaviour
{
    PlayerInputReciever m_playerInputReciever;
    PlayerInputExecuter m_playerInputExecuter;
    // Start is called before the first frame update
    private void Awake()
    {
        m_playerInputReciever = FindObjectOfType<PlayerInputReciever>();
        m_playerInputExecuter = FindObjectOfType<PlayerInputExecuter>();

    }

    // Update is called once per frame
    void Update()
    {
        //インプットを取得
        m_playerInputReciever.RecieveKeyInput();

        //インプット処理
        m_playerInputExecuter.InputExecute();
    }
}
