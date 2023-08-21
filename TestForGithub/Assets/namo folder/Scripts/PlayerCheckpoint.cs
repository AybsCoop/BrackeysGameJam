using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	

public class PlayerCheckpoint : MonoBehaviour
{

	public float speed;

    public Transform[] CheckPoints;

    public int CheckpointNum;


    void Update()
    {
	    MovePlayer();
    }

	void MovePlayer(){
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

        transform.Translate(x * Time.deltaTime * speed, y * Time.deltaTime * speed, 0);

	}

	public void TeleportToCheckpoint()
    {
        transform.position = CheckPoints[CheckpointNum].position;
    }


	void OnCollisionEnter2D(Collision2D collision)
	{

		if (collision.gameObject.CompareTag("Obstacle"))
		{

            TeleportToCheckpoint();
        }
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            //disable the circle collider of the check point
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;

            //Checkpoint passed
            CheckpointNum++;

        }
    }



}
