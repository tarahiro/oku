using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] MainManager m_mainManager;
    [SerializeField] GameObject m_gameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        m_gameOver.SetActive(true);
        m_mainManager.gameObject.SetActive(false);
    }

    enum GameState
    {
        None,
        Start,
        Main,
        End
    }
}
