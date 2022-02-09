using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    private GameObject playerObject;
    private GameManager gameManager;
    private Rigidbody sheepRigidBody;
    public float range;
    public float force;
    public bool isSafe;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sheepRigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive) 
        {
            if (IsPlayerNear())
            {
                //Player near and not safe
                var awayFromPlayer = transform.position - playerObject.transform.position;
                //var adjustedDirection = AdjustDirectionToBorder(awayFromPlayer);
                if (isSafe)
                {
                    sheepRigidBody.AddForce(awayFromPlayer * (force / 10));
                }
                else
                {
                    sheepRigidBody.AddForce(awayFromPlayer * force);
                }
                //transform.Translate(adjustedDirection * speed * Time.deltaTime);
            }

            if (gameManager.IsPosInFence(gameObject.transform.position))
            {
                isSafe = true;
            }
            else
            {
                isSafe = false;
            }
        }
    }

    Vector3 AdjustDirectionToBorder(Vector3 awayFromPlayer)
    {
        var border = playerObject.GetComponent<PlayerController>().border;

        if (awayFromPlayer.x > 0 && transform.position.x >= border)
            awayFromPlayer.x = -3f;
        if (awayFromPlayer.x < 0 && transform.position.x <= -border)
            awayFromPlayer.x = 3f;

        if (awayFromPlayer.z > 0 && transform.position.z >= border)
            awayFromPlayer.z = -3f;
        if (awayFromPlayer.z < 0 && transform.position.z <= -border)
            awayFromPlayer.z = 3f;

        awayFromPlayer.y = 0;
        return awayFromPlayer;

    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("Wall"))
        //{
        //    isSafe = true;
        //}
    }

    bool IsPlayerNear()
    {
        var distance = Vector3.Distance(transform.position, playerObject.transform.position);
        //Debug.Log("Player Distance: " + distance);
        if (distance < range)
        {
            Debug.Log("Player near.");
            return true;
        }
        else
        {
            return false;
        }
    }
}
