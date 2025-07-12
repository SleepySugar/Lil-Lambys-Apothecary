using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class npcManager : MonoBehaviour
{
    public List<Transform> wayPoints = new List<Transform>();
    public int waypointIndex;
    public bool isMoving;
    public float moveSpeed;
    public bool customerHelped;
    public TMP_InputField npcChat;
    public Image chatBackground;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcChat.readOnly = true;
        StartMoving();
        customerHelped = false;
    }

    public void StartMoving()
    {
        waypointIndex = 0;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            return;
        }

        if (!customerHelped)
        {
            moveTo0();
            if (transform.position == wayPoints[0].position)
            {
                chatOn();
                npcChat.text = "Quick! I need a medicinal potion!";
            }
        }

        if (customerHelped)
        {
            npcChat.text = "Thank you Lamby!";
            moveTo1();
            if (transform.position == wayPoints[1].position)
            {
                chatOff();
            }
        }
        
    }

    public void chatOn()
    {
        chatBackground.gameObject.SetActive(true);
        npcChat.gameObject.SetActive(true);
    }

    public void chatOff()
    {
        chatBackground.gameObject.SetActive(false);
        npcChat.gameObject.SetActive(false);
    }

    public void moveTo0()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[0].position, Time.deltaTime * moveSpeed);
    }

    
    public void moveTo1()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[1].position, Time.deltaTime * moveSpeed);

    }
    
}
