using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterScript : MonoBehaviour
{
    public MonsterScript()
    {
        
    }

    public MonsterScript(string Name = "NoName", float Hp = 10, float MaxHP = 10, 
        float Attack = 1, float Def = 1)
    {
        this.m_MonsterName = Name;
        this.m_HP = Hp;
        this.m_MaxHP = MaxHP;
        this.m_Attack = Attack;
        this.m_Def = Def;
    }
    //Default setting
    public string m_MonsterName;
    public float m_HP = 10;
    public float m_MaxHP = 10;
    public float m_Attack = 1;
    public float m_Def = 1;

    private void Update()
    {
        if (m_HP <= 0)
        {
            Destroy(this.gameObject);
            GameObject player = GameObject.Find("Player");
            PlayerScript script = player.GetComponent<PlayerScript>();
            script.m_Score += 300;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            m_HP -= player.m_Attack;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            m_HP -= player.m_Attack;
        }
    }
}
