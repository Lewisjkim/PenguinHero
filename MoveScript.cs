using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private float m_dirX, m_dirY;
    public float m_moveSpeed = 5f;
    private Rigidbody2D m_rigidBody;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_dirX = Input.GetAxis("Horizontal");
        m_dirY = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        m_rigidBody.velocity = new Vector2(m_dirX * m_moveSpeed, m_dirY * m_moveSpeed);
    }
}
