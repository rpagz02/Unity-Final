using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Player : MonoBehaviour
{
    #region Variables
    [Header("Player Stats")]
    [SerializeField]
    private float m_Health_Current;
    [SerializeField]
    private float m_Health_Max;
    [SerializeField]
    private float m_Level;
    [SerializeField]
    private float m_XP_Current;
    [SerializeField]
    private float m_XP_Max;
    #endregion Variables

    // Start is called before the first frame update
    void Start()
    {
        m_Health_Current = 100;
        m_Health_Max = 100;
        m_XP_Current = 0;
        m_XP_Max = 500;
        m_Level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
