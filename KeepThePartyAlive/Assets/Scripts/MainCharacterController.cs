using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public float walkSpeed = 20;
    public float jumpForce = 500;
    public GameObject pizza;
    public GameObject beer;
    public bool isHoldingItem = false;
    public bool hasBeer = false;
    public bool hasPizza = false;
    public GameObject bananaSuit;
    public GameController gameController;
    public bool isDancing = false;
    bool isMoving = false;
    bool isRunning = false;

    bool dancedOnTable = false;
    bool isOnTable = false;
    bool hasBounced = false;

    bool nearMicrowave = false;
    bool nearFridge = false;
    bool nearNPC = false;
    bool nearBanana = false;
    bool isJumping = false;
    Animator animationController;
    GameObject NPC;

    public GameObject yaySoundPlayer;

    public GameObject bananaInCloset;

    bool goneThroughBananaGate = false;

    // Start is called before the first frame update
    void Start()
    {
        bananaSuit.SetActive(false);
        pizza.SetActive(false);
        beer.SetActive(false);
        animationController = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Run") && !isRunning){
            isRunning = true;
        } 
        
        if(Input.GetButtonUp("Run")) {
            isRunning = false;
        }

        Vector3 Movement = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if(((Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0) && !isJumping))
        {   
            animationController.SetInteger("State",1);

            isDancing = false;
            if(isRunning){
                animationController.SetInteger("State",3);
            } else {
            }
        } else if(!isJumping && !isDancing)
        { 
            animationController.SetInteger("State",0);
            isDancing = true;
            if(isOnTable && !dancedOnTable){
                yaySoundPlayer.GetComponent<AudioSource>().Play();
                gameController.incrementHype(20);

                dancedOnTable = true;
            }   
        }

        Vector3 camF = Camera.main.transform.forward;
        Vector3 camR = Camera.main.transform.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        Movement = Vector3.ClampMagnitude(Movement,1);

        if(isRunning){
            this.transform.position += (camF * Movement.y + camR * Movement.x) * (walkSpeed + 3f) * Time.deltaTime;
        } else {
            this.transform.position += (camF * Movement.y + camR * Movement.x) * walkSpeed * Time.deltaTime;
        }
        
        if(Mathf.Abs( Input.GetAxis("Vertical")) > 0  || Mathf.Abs( Input.GetAxis("Horizontal")) > 0){
            transform.rotation = Quaternion.LookRotation((camF * Movement.y + camR * Movement.x));
        }

        if (Input.GetButtonDown("Interact"))
        {
            if(nearMicrowave && !isHoldingItem){
                isHoldingItem = true;
                pizza.SetActive(true);
                hasPizza = true;
            } else if(nearFridge && !isHoldingItem){
                isHoldingItem = true;
                beer.SetActive(true);
                hasBeer = true;
            }

            if(nearBanana){
                bananaSuit.SetActive(true);
                bananaInCloset.SetActive(false);
            }

            if(nearNPC && isHoldingItem){
                NPCBehavior nPCBehavior = NPC.GetComponent<NPCBehavior>();
                nPCBehavior.interact();
                if(nPCBehavior == null){
                    Debug.Log("npc behavior is null");
                }
                isHoldingItem = false;
                hasPizza = false;
                hasBeer = false;
                pizza.SetActive(false);
                beer.SetActive(false);
                gameController.incrementHype(20);   
            }
        }

        if(Input.GetButtonDown("Jump") && !isJumping){
            isJumping = true;
            this.GetComponent<Rigidbody>().AddForce(transform.up * 5, ForceMode.Impulse);
            Debug.Log("Jump");
            animationController.SetInteger("State",2);
            isDancing = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Microwave"){
            nearMicrowave = true;
        } else if(other.gameObject.tag == "Fridge"){
            nearFridge = true;
        } else if(other.gameObject.tag == "NPC"){
            nearNPC = true;
            NPC = other.gameObject;
        } else if(other.gameObject.tag == "Banana"){
            nearBanana = true;
        } else if(other.gameObject.tag == "BananaGate" && bananaSuit.activeInHierarchy == true && !goneThroughBananaGate){
            goneThroughBananaGate = true;
            yaySoundPlayer.GetComponent<AudioSource>().Play();
            gameController.incrementHype(20);
        } else if(other.gameObject.tag == "DanceArea"){
            isOnTable = true;
        } else if(other.gameObject.tag == "Bounce" && Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > 0){
            GetComponent<Rigidbody>().AddForce(Vector3.up * 600);
            Debug.Log("Bounced");
            animationController.SetInteger("State",2);
            if(!hasBounced){
                hasBounced = true;
                yaySoundPlayer.GetComponent<AudioSource>().Play();
                gameController.incrementHype(20);
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Microwave"){
            nearMicrowave = false;
        } else if(other.gameObject.tag == "Fridge"){
            nearFridge = false;
        } else if(other.gameObject.tag == "NPC"){
            nearNPC = false;
            // NPC = null;
        } else if(other.gameObject.gameObject.tag == "DanceArea"){
            isOnTable = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(isJumping && collision.gameObject.tag == "Ground"){
            isJumping = false;
            animationController.SetInteger("State",0);
            Debug.Log("Landed");
        }
    }
}
