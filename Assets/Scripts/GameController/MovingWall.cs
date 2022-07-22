using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    private Vector3 start = new Vector3(-25.0f, 1.0f, -2.69f);
    private Vector3 end = new Vector3(25.0f, 1.0f, -2.69f);
    private Vector3 velocity = new Vector3(1.0f, 0.0f, 0.0f);
    private bool moveForward = true;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(0, 1.0f, -2.69f);
    }

    // Update is called once per frame
    void Update()
    {
        moveToEnd();
    }

    void moveToEnd()
    {
        if (this.transform.position.x >= end.x)
        {
            moveForward = !moveForward;
        }

        if (this.transform.position.x <= start.x)
        {
            moveForward = !moveForward;
        }

        if (moveForward)
        {
            this.transform.position += (velocity * Time.deltaTime);
        }
        else
        {
            this.transform.position += (-velocity * Time.deltaTime);
        }
    }
}