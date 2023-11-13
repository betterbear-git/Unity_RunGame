using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    enum enDirection { North, East, West}; // North : 0, East : 1, West : 2

    CharacterController characterController;
    Vector3 playerVector; // Accounts for player's direction
    enDirection playerDirection = enDirection.North;
    enDirection playerNextDirection = enDirection.North;

    float playerStartSpeed = 1.0f;
    float playerSpeed;
    float gValue = 10.0f; //player 중력
    float translationFactor= 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = playerStartSpeed;
        characterController = this.GetComponent<CharacterController>();
        playerVector = new Vector3(0,0,1) * playerSpeed * Time.deltaTime; // Same as : new Vector3(0,0,playerSpeed)
    }

    // Update is called once per frame
    void Update()
    {
        //The Character Controller is mainly used for third-person or first-poerson player control that doew not make use of Rigidbody physice
        PlayerLogic();
    }

    void PlayerLogic()
    {
        //playerSpeed += 0.005f * Time.deltaTime; 

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch(playerDirection)
            {
                case enDirection.North:
                    playerNextDirection = enDirection.East;
                    this.transform.rotation = Quaternion.Euler(0,90,0);
                    break;
                case enDirection.East:
                    playerNextDirection = enDirection.North;
                    this.transform.rotation = Quaternion.Euler(0,0,0);
                    break;
            }
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            switch(playerDirection)
            {
                case enDirection.North:
                    playerNextDirection = enDirection.West;
                    this.transform.rotation = Quaternion.Euler(0,-90,0);
                    break;
                case enDirection.East:
                    playerNextDirection = enDirection.North;
                    this.transform.rotation = Quaternion.Euler(0,0,0);
                    break;
            }
        }

        playerDirection = playerNextDirection;

        if(playerDirection == enDirection.North)
        {
            playerVector = Vector3.forward * playerSpeed * Time.deltaTime;
        }else if(playerDirection == enDirection.East){
            playerVector = Vector3.right * playerSpeed * Time.deltaTime;
        }else if(playerDirection == enDirection.West){
            playerVector = Vector3.left * playerSpeed * Time.deltaTime;
        }

        //Horizontal movement of the player
        /*switch(playerDirection)
        {
            case enDirection.North:
                playerVector.x = Input.GetAxisRaw("Horizontal") * translationFactor * Time.deltaTime;
                break;
            case enDirection.East:
                playerVector.z = -Input.GetAxisRaw("Horizontal") * translationFactor * Time.deltaTime;
                break;
            case enDirection.West:
                playerVector.z = Input.GetAxisRaw("Horizontal") * translationFactor * Time.deltaTime;
                break;
        }*/

        if(characterController.isGrounded){
            playerVector.y = -0.2f;
        }else{
            playerVector.y -=  (gValue * Time.deltaTime);
        }
        characterController.Move(playerVector);
    }
}
