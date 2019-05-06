using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FPS_Player : MonoBehaviour
{
    [System.Serializable]
    public class Stats
    {
    }

    #region Variables
    [SerializeField]
    private float m_Health_Current; 
    [SerializeField]
    private float m_Health_Max;
    [SerializeField]
    private bool isDead = false;
    [SerializeField]
    private float m_Level;
    [SerializeField]
    private float m_XP_Current;
    [SerializeField]
    private float m_XP_Max;
    [Space(10)]
    public GameObject dmgFlashIndicator;
    private Color flashColor = new Color(1f,0f,0f,0.1f);
    private bool damaged = false;
    [Space(10)]
    public GameObject CanvasMenu, CanvasHUD;


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
        HealthMonitor();
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            dmgFlashIndicator.GetComponent<Image>().color = flashColor;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            dmgFlashIndicator.GetComponent<Image>().color = Color.Lerp(dmgFlashIndicator.GetComponent<Image>().color, Color.clear, 5 * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }

    private void HealthMonitor()
    {
        if(m_Health_Current <= 0)
        {
            m_Health_Current = 0;
            isDead = true;           
        }

        if(isDead)
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
    private void ArmorMonitor()
    {

    }

    public bool IsDead()
    {
        return isDead;
    }

     // Methods called outside of this calss
    public void DamagePlayer(float damageAmnt)
    {
        m_Health_Current -= damageAmnt;
        damaged = true;
    }

    public float GetHealth()
    {
        return m_Health_Current;
    }
    public float GetMaxHealth()
    {
        return m_Health_Max;
    }
}
