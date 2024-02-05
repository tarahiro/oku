using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerView : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI m_savotageText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Savotage()
    {
        m_savotageText.text = "サボり中";
    }

    public void Exaust()
    {
        m_savotageText.text = "休憩中...";
    }

    public void StartReport()
    {
        SetTextNeutral();
    }

    public void Rest()
    {
        SetTextNeutral();
    }

    void SetTextNeutral()
    {
        m_savotageText.text = "Z:サボる";

    }
}
