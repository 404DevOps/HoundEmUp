using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    bool isOpen;
    private GameObject clockHand;
    private GameObject playerObject;
    private GameManager gameManager;
    void Start()
    {
        playerObject = GameObject.Find("Player");
        clockHand = transform.parent.gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveDoor();
        }

        //if (!isOpen && AllSheepSafe())
        //{
        //    gameManager.GameOver();
        //}

    }

    public bool AllSheepSafe()
    {
        var sheep = GameObject.FindObjectsOfType<SheepController>();
        foreach (var s in sheep)
        {
            if (!s.isSafe)
                return false;
        }

        return true;
    }


    void MoveDoor()
    {
        if (IsPlayerNear())
        {
            if (isOpen)
            {
                clockHand.transform.Rotate(0, -90, 0, Space.Self);
                isOpen = false;
            }
            else
            {
                clockHand.transform.Rotate(0, 90, 0, Space.Self);
                isOpen = true;
            }
        }
    }
    bool IsPlayerNear()
    {
        var distance = Vector3.Distance(transform.position, playerObject.transform.position);
        if (distance < 1.5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
