using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float range;

    private GameManager gameManager;
    private Rigidbody playerRb;
    private Animator animator;

    private AudioSource barkAudio;

    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;

    public float border = 4.5f;
    // Start is called before the first frame update
    void Start()
    {
        barkAudio = GameObject.Find("BarkAudio").GetComponent<AudioSource>(); ;
        animator = gameObject.GetComponentInChildren<Animator>();
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); ;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (gameManager.isGameActive)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
            MovePlayer();
            if (verticalInput != 0)
            {
                animator.SetFloat("Speed_f", 1.7f);
            }
            else
            {
                animator.SetFloat("Speed_f", 0);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("Speed_f", 2.5f);
                speed = 5;
            }
            else
            {
                speed = 3;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bark();
        }
    }

    public void Bark()
    {
        animator.SetTrigger("BarkTrigg");

        var sheeps = GameObject.FindGameObjectsWithTag("Sheep");
        foreach (var sheep in sheeps)
        {
            if (IsPosInRange(sheep.gameObject.transform.position))
            {
                sheep.GetComponent<SheepController>().SetMovePosition();
            }
        }

        barkAudio.Play();
    }

    void MovePlayer()
    {
        if (gameManager.isGameActive)
        {
            gameObject.transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.Self);
            gameObject.transform.Rotate(Vector3.up * horizontalInput * turnSpeed * Time.deltaTime);
        }
    }

    public bool IsPosInRange(Vector3 pos)
    {
        var distance = Vector3.Distance(gameObject.transform.position, pos);
        return distance < range;
    }
}
