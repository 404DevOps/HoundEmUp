using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    private GameObject playerObject;
    private GameManager gameManager;
    private Rigidbody sheepRigidBody;

    private AudioSource squeeqAudio;
    private Animator sheepAnimator;

    public float speed;
    public bool isSafe;

    Vector3 moveTo;
    Vector3 startPos;
    public float distance;

    private bool moveSheep;
    Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        sheepAnimator = GetComponentInChildren<Animator>();
        squeeqAudio = GetComponentInChildren<AudioSource>();
        playerObject = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sheepRigidBody = GetComponent<Rigidbody>();

        squeeqAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        if (gameManager.isGameActive)
        {
            isSafe = gameManager.IsPosInFence(gameObject.transform.position);

            if (isSafe)
            {
                gameManager.UpdateScore(100);
                gameManager.SpawnSheep();
                gameObject.SetActive(false);
            }
            //transform.Translate(moveTo);
            if (moveSheep) {
                if (isPlayerInRange())
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                }
                else 
                {
                    moveSheep = false;
                }
            }
        }
        if (transform.position != lastPosition)
        {
            lastPosition = transform.position;
            sheepAnimator.SetFloat("Speed_f", 1.5f);
        }
        else {
            sheepAnimator.SetFloat("Speed_f", 0f);
        }
    }

    bool isPlayerInRange()
    {
        return playerObject.GetComponent<PlayerController>().IsPosInRange(transform.position);
    }
    public void SetMovePosition()
    {
        var awayFromPlayer = transform.position - playerObject.transform.position;
        transform.LookAt(transform.position + awayFromPlayer); 

        //moveTo = gameObject.transform.position + awayFromPlayer;
        //startPos = gameObject.transform.position;
        moveSheep = true;
    }


}
