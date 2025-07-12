using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class waypointManager : MonoBehaviour
{
    public npcManager npc;

    public List<Transform> wayPoints = new List<Transform>();
    public int waypointIndex;

    public bool isMoving;
    public float moveSpeed;

    public GameObject herbButton, flaskButton, cauldronButton;

    public GameObject holdHerb, holdEmptyPotion, holdFilledPotion;
    public GameObject cauldronLiquid;

    public bool herbStatus, emptyPotionStatus, filledPotionStatus, potFilled;

    public TextMeshProUGUI fillPotion, dropHerbs;

    public GameObject fire1, fire2, fire3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveTo0();
        StartMoving();
        herbStatus = false;
        emptyPotionStatus = false;
        filledPotionStatus = false;
        potFilled = false;
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

        interact();


    }

    public void interact()
    {
        if (transform.position == wayPoints[0].position) //if player in forward wp
        {
            customerInteract();

            if (Input.GetKeyDown(KeyCode.S))
            {
                moveTo1();
            }
        }



        if (transform.position == wayPoints[1].position) //if player in mid wp
        {

            cauldronInteract();


            if (Input.GetKeyDown(KeyCode.W))
            {
                moveTo0();
                fillPotion.gameObject.SetActive(false);
                dropHerbs.gameObject.SetActive(false);
                cauldronButton.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                moveTo2();
                fillPotion.gameObject.SetActive(false);
                dropHerbs.gameObject.SetActive(false);
                cauldronButton.gameObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                moveTo3();
                fillPotion.gameObject.SetActive(false);
                dropHerbs.gameObject.SetActive(false);
                cauldronButton.gameObject.SetActive(false);
            }
        }



        if (transform.position == wayPoints[2].position) //if player in right wp
        {
            herbButton.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                getHerb();
            }


            if (Input.GetKeyDown(KeyCode.A))
            {
                moveTo1();
                herbButton.gameObject.SetActive(false);
            }
        }

        if (transform.position == wayPoints[3].position) //if player in left wp
        {
            flaskButton.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                getFlask();
            }



            if (Input.GetKeyDown(KeyCode.D))
            {
                moveTo1();
                flaskButton.gameObject.SetActive(false);
            }
        }
    }


    public void customerInteract()
    {
        if (filledPotionStatus)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                potionOff();
                npc.customerHelped = true;
                Debug.Log("helped customer");
            }
        }
    }

    public void cauldronInteract()
    {
        if (herbStatus)
        {
            cauldronButton.gameObject.SetActive(true);
            dropHerbs.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                herbOff();
                dropHerbs.gameObject.SetActive(false);
                cauldronButton.gameObject.SetActive(false);
                cauldronLiquid.gameObject.SetActive(true);
                potFilled = true;
                fireOn();
            }
        }

        if (emptyPotionStatus && potFilled)
        {
            cauldronButton.gameObject.SetActive(true);
            fillPotion.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                flaskOff();
                fillPotion.gameObject.SetActive(false);
                cauldronButton.gameObject.SetActive(false);
                cauldronLiquid.gameObject.SetActive(false);
                potionOn();
                fireOff();
            }
        }
    }

    public void potionOn()
    {
        holdFilledPotion.gameObject.SetActive(true);
        filledPotionStatus = true;
    }
    public void potionOff()
    {
        holdFilledPotion.gameObject.SetActive(false);
        filledPotionStatus = false;
    }

    public void getHerb()
    {
        if (herbStatus)
        {
            herbOff();
        }
        else
        {
            herbOn();
        }
    }

    public void getFlask()
    {
        if (emptyPotionStatus)
        {
            flaskOff();
        }
        else
        {
            flaskOn();
        }
    }
    public void flaskOn()
    {
        holdEmptyPotion.gameObject.SetActive(true);
        emptyPotionStatus = true;
    }
    public void flaskOff()
    {
        holdEmptyPotion.gameObject.SetActive(false);
        emptyPotionStatus = false;
    }


    public void herbOn()
    {
        holdHerb.gameObject.SetActive(true);
        herbStatus = true;
    }
    public void herbOff()
    {
        holdHerb.gameObject.SetActive(false);
        herbStatus = false;
    }

    public void fireOn()
    {
        fire1.gameObject.SetActive(true);
        fire2.gameObject.SetActive(true);
        fire3.gameObject.SetActive(true);
    }
    public void fireOff()
    {
        fire1.gameObject.SetActive(false);
        fire2.gameObject.SetActive(false);
        fire3.gameObject.SetActive(false);
    }


    //  MOVEMENT TO WAYPOINTS
    public void moveTo0() //forward WP
    {
        while (transform.position != wayPoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[0].position, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void moveTo1() //middle WP
    {
        while (transform.position != wayPoints[1].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[1].position, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void moveTo2() //right WP
    {
        while (transform.position != wayPoints[2].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[2].position, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
    public void moveTo3() //left WP
    {
        while (transform.position != wayPoints[3].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[3].position, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
    }

}
