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
        //�C���v�b�g���擾
        m_playerInputReciever.RecieveKeyInput();

        //�C���v�b�g����
        m_playerInputExecuter.InputExecute();
    }
}
