using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TMP_Glow : MonoBehaviour
{
    public float glowPower = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().fontSharedMaterial.SetFloat("_GlowPower", Mathf.PingPong(Time.time * 0.15f, glowPower));
        GetComponent<TextMeshProUGUI>().UpdateMeshPadding();
    }
}
