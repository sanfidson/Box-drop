using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private float min_X = -2.2f, max_X = 2.2f;

    private bool canMove;
    private float moveSpeed = 2f;

    private Rigidbody2D myBody;

    private bool gameOver;

    private bool ignoreCollision;
    private bool ignoreTrigger;


    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.gravityScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;

        if (UnityEngine.Random.Range(0, 2) > 0)
        {
            moveSpeed *= -1;
        }

        GameplayController.instance.currentBox = this;
    }

    // Update is called once per frame
    void Update()
    {
        moveBox();
    }

    void moveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;

            temp.x += moveSpeed * Time.deltaTime;

            if (temp.x > max_X)
            {
                moveSpeed *= -1;
            }
            else if (temp.x < min_X)
            {
                moveSpeed *= -1;
            }

            transform.position = temp;
        }
    }

    public void DropBox()
    {
        canMove = false;
        myBody.gravityScale = UnityEngine.Random.Range(2, 4);
    }

    void Landed()
    {
        if (gameOver) 
            return;
        ignoreCollision = true;
        ignoreTrigger = true;

        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.MoveCamera();

    }

    void RestartGame()
    {
        GameplayController.instance.RestartGame();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;

        if (target.gameObject.tag == "Platform" || target.gameObject.tag == "Box") 
        {
            Invoke("Landed", 1f);
            ignoreCollision = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
            return;
        if (target.tag == "GameOver")
        {
            Console.WriteLine("GameOver");
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;

            Invoke("RestartGame", 1f);
        }
    }
}
