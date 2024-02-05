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
        m_savotageText.text = "�T�{�蒆";
    }

    public void SetExaust(bool isExaust)
    {
        if (isExaust)
        {
            m_savotageText.text = "�x�e��...";
        }
        else
        {
            m_savotageText.text = "Z : �T�{��";
        }

    }
}
