using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public string m_PlayerName;
    public float m_HP = 1000f;
    public float m_MaxHP = 1000f;
    public float m_Attack = 4f;
    public float m_Speed = 2f;
    public float m_Recovery = 0f;
    public int m_Score = 0;
    public bool m_Clear = false;
    public int m_stageNum = 1;
    public int m_LifeCount = 3;
    private LevelScript m_LevelScript;
    Animator animator;
    public bool m_gameStart = false;

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        m_LevelScript = GameObject.Find("Level").GetComponent<LevelScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_gameStart)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                m_Score += 10000;
            }
            if (m_HP < m_MaxHP)
            {
                m_HP += m_Recovery * m_MaxHP;
            }
            if (m_Clear == false)
            {
                transform.position += Vector3.right * m_Speed * Time.deltaTime;
            }
            if (m_HP <= 0 && m_LifeCount > 0)
            {
                m_LifeCount -= 1;
                //GameObject[] Array = GameObject.FindGameObjectsWithTag("Monster");
                //for (int i = 0; i < Array.Length; i++)
                //{
                //    Destroy(Array[i]);
                //}
                m_HP = m_MaxHP;
                transform.position = new Vector3(-6.75f, -1.71f, 0);
            }
            if (m_HP <= 0 && m_LifeCount == 0)
            {
                //Delete Monster
                GameObject[] Array = GameObject.FindGameObjectsWithTag("Monster");
                for (int i = 0; i < Array.Length; i++)
                {
                    Destroy(Array[i]);
                }
                //Reset Monster info
                GameObject.Find("Level").GetComponent<LevelScript>().m_extraHP = 0;

                //Reset Player info
                transform.position = new Vector3(-6.75f, -1.71f, 0);
                m_HP = 1000f;
                m_MaxHP = 1000f;
                m_Attack = 4f;
                m_Recovery = 0f;
                m_Score = 0;
                m_Speed = 0;
                m_stageNum = 1;

                //End Panel + Retry button
                GameManager.m_Instance.ShowRetryPanel(true);

                //BGM Stop
                GameManager.m_Instance.GetComponent<AudioSource>().Stop();
                //Retry BGM 넣고
                //retry 시 bgm 다시 시작

            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 3)
        {
            animator.SetBool("OnCollision", true);
            MonsterScript monster = other.gameObject.GetComponent<MonsterScript>();
            m_HP -= monster.m_Attack;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            MonsterScript monster = other.gameObject.GetComponent<MonsterScript>();
            m_HP -= monster.m_Attack;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            animator.SetBool("OnCollision", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FinishBox")
        {
            GameObject[] Array = GameObject.FindGameObjectsWithTag("Monster");
            for (int i = 0; i < Array.Length; i++)
            {
                Destroy(Array[i]);
            }
            m_LevelScript.SetNextStage(true);
            m_stageNum += 1;
            GameManager.m_Instance.ShowNextStagePanel(true);
            transform.position = new Vector3(-6.75f, -1.71f, 0);
        }
    }
}
