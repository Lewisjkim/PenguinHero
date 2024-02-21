using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    private GameObject monster;
    private bool m_NextStage;
    private PlayerScript playerscript;
    private float m_time = 0f;
    public int m_extraHP = 0;
    
    void Start()
    {
        playerscript = GameObject.Find("Player").GetComponent<PlayerScript>();
        monster = GetComponent<GameObject>();
        monster = Resources.Load<GameObject>("Bunny");
        if(null == monster)
        {
            Debug.Log("prefab not found");
        }
        
        GameManager.m_Instance.ShowRetryPanel(false);
        //SpawnMonster();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player").GetComponent<PlayerScript>().m_gameStart)
        {
            m_time += Time.deltaTime;
            if (m_NextStage)
            {
                m_time = 0f;
                GameManager.m_Instance.ShowNextStagePanel(true);
                SpawnMonster();
                m_NextStage = false;
            }
            if (m_time > 1f)
            {
                GameManager.m_Instance.ShowNextStagePanel(false);
            }
        }
        
    }

    public void SpawnMonster()
    {
        Vector3 position = new Vector3(8, -2.2f, 0f);
        monster.layer = 3;
        GameObject prefab = Instantiate(monster, position, Quaternion.identity);
        prefab.GetComponent<MonsterScript>().m_HP += m_extraHP;
        prefab.GetComponent<MonsterScript>().m_MaxHP += m_extraHP;
        m_extraHP += 150;
    }

    public void SetNextStage(bool isNextStage)
    {
        m_NextStage = isNextStage;
    }

}
