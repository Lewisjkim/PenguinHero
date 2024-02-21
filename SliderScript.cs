using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    Slider  m_HPSlider; 
    public static float m_health;
    public PlayerScript m_playerScript;
    void Start()
    {
        m_HPSlider = GetComponent<Slider>();
        m_playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        m_HPSlider.value = m_playerScript.m_HP / m_playerScript.m_MaxHP * 100;
    }


 
}
