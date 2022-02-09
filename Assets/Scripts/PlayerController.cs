using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private GameManager gameManager;

    public float border = 4.5f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        //keep rotation
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        if (gameManager.isGameActive)
        {
            if (Input.GetKey(KeyCode.W))
            {
                    gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                    gameObject.transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                    gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);    
            }
            if (Input.GetKey(KeyCode.D))
            {
                    gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);   
            }
        }
    }
}
