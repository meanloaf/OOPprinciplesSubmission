using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float negxbound = -3.71f;
    private float posxbound = 2.65f;
    private float negybound = -2.78f;
    private float posybound = 6.74f;
    private float zpos = -10f;
    private GameObject playerKnight;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        playerKnight = GameObject.Find("Knight");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        CheckBounds();
    }

    void CheckBounds()
    {
        float xpos = transform.position.x;  //gets current x position
        float ypos = transform.position.y;  //gets current y position
        //checks if the positions are out of bounds and sets them to bounds if they are
        if (xpos < negxbound)
        {
            if (ypos > negybound && ypos < posybound)
            {
                transform.position = new Vector3(negxbound, ypos, zpos);
            }
            else
            {
                YBound(-1, ypos);
            }
            
        }
        else if (xpos > posxbound)
        {
            if (ypos > negybound && ypos < posybound)
            {
                transform.position = new Vector3(posxbound, ypos, zpos);
            }
            else
            {
                YBound(1, ypos);
            }
        }
        if (ypos < negybound)
        {
            if (xpos > negxbound && xpos < posxbound)
            {
                transform.position = new Vector3(xpos, negybound, zpos);
            }
            else
            {
                XBound(-1, xpos);
            }
        }
        else if (ypos > posybound)
        {
            if (xpos > negxbound && xpos < posxbound)
            {
                transform.position = new Vector3(xpos, posybound, zpos);
            }
            else
            {
                XBound(1, xpos);
            }
        }
    }

    //Corrects Y-bound if both x and y position out of bounds
    void YBound(int boundpass, float ypos)
    {
        float xpos;
        if(boundpass == -1)
        {
            xpos = negxbound;
        }
        else
        {
            xpos = posxbound;
        }
        if (ypos > posybound)
        {
            transform.position = new Vector3(xpos, posybound, zpos);
        }
        else
        {
            transform.position = new Vector3(xpos, negybound, zpos);
        }
    }
    
    //Corrects X-bound if both x and y positions out of bounds
    void XBound(int boundpass, float xpos)
    {
        float ypos;
        if (boundpass == -1)
        {
            ypos = negybound;
        }
        else
        {
            ypos = posybound;
        }
        if (xpos > posxbound)
        {
            transform.position = new Vector3(posxbound, ypos, zpos);
        }
        else
        {
            transform.position = new Vector3(negxbound, ypos, zpos);
        }
    }

    void FollowPlayer()
    {
        float xtrack = playerKnight.transform.position.x;
        float ytrack = playerKnight.transform.position.y;
        transform.position = new Vector3(xtrack, ytrack, zpos);
    }
}
