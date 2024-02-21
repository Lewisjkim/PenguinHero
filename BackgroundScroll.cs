using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.name == "FrontBackground")
        {
            if (this.transform.position.x > -1.36)
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            else
                transform.position = new Vector3(1.1f, -1.01f, 0);
        }
        else if(this.name == "BackBackground")
        {
            if (this.transform.position.x > -1.36)
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            else
                transform.position = new Vector3(1.29f, 1f, 0);
        }
        
    }
}
