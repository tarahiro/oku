using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_dateTimeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTime(DateTime dateTime)
    {
        m_dateTimeText.text = dateTime.ToString("yyyy/MM/dd HH:mm");
    }
}
