using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInputExecuter : MonoBehaviour
{
    PlayerInputReciever m_reciever;
    MainManager m_mainManagerCache;
    PostitPhysicsController m_postitPhysicsController;
    DebugManager m_debugManager;

    private void Awake()
    {
        m_mainManagerCache = FindObjectOfType<MainManager>();
        m_reciever = GetComponent<PlayerInputReciever>();
        m_postitPhysicsController = FindObjectOfType<PostitPhysicsController>();
        m_debugManager = FindObjectOfType<DebugManager>();  
    }

    public void InputExecute()
    {
        //FixedUpdateの情報をUpdateで使っていたらスキップ
        if (!m_reciever.isFixedUpdated)
        {
            //レイキャスト系処理
            m_reciever.RayCastExecute();

            //マウスカーソル系処理
            ExecuteMouseInput();
        }

        //キーボード系処理
        ExecuteKeyInput();
    }

    void ExecuteMouseInput()
    {
        m_postitPhysicsController.ReflectMouseInput();
    }

    void ExecuteKeyInput()
    {

        if (m_reciever.m_keyInKeyList.Exists(x => x == KeyCode.Z))
        {
            if (StaticVariableCollector.mainState != MainManager.MainState.Savotage
                && StaticVariableCollector.mainState != MainManager.MainState.Exhausted) Savotage();
        }
        else
        {
            if (StaticVariableCollector.mainState == MainManager.MainState.Savotage) RemoveSavotage();
        }

        if (m_reciever.m_keyDownKeyList.Exists(x => x == KeyCode.Tab))
        {
            m_debugManager.SkipToNextDay();
        }

        if (m_reciever.m_keyDownKeyList.Exists(x => x == KeyCode.Escape))
        {
            m_debugManager.SkipToSpecificDayWithoutReport();
        }
    }

    void Savotage()
    {
        m_mainManagerCache.Savotage();
    }

    void RemoveSavotage()
    {
        m_mainManagerCache.RemoveSavotage();
    }

}
