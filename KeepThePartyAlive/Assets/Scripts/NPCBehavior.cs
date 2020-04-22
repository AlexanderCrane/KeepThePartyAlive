using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public bool NPCIsPleased = true;
    bool needsPizza = false;
    bool needsBeer = false;
    float hypeModifier = -0.01f;

    public GameObject pizzaImage;
    public GameObject beerImage;
    public GameController gameController;

    Coroutine waiter;

    public MainCharacterController mainCharacter;
    // Start is called before the first frame update
    void Start()
    {
        pizzaImage.SetActive(false);
        beerImage.SetActive(false);
        waiter = StartCoroutine (waitToBecomeDispleased());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator waitToBecomeDispleased()
     {
        int wait_time = Random.Range (10, 120);
        int need = Random.Range (0, 2);
        yield return new WaitForSeconds (wait_time);
        if(gameController.totalNeeds < gameController.maxNeeds){
            NPCIsPleased = false;
            if(need == 0){
                needsPizza = true;
                pizzaImage.SetActive(true);
                gameController.incrementHypeChangeRate(hypeModifier);
            } else if(need >= 1){
                needsBeer = true;
                beerImage.SetActive(true);
                gameController.incrementHypeChangeRate(hypeModifier);
            }
            gameController.totalNeeds++;
        }
        restartCoroutine();
    }

    void restartCoroutine(){
        StopCoroutine(waiter);
        waiter = StartCoroutine(waitToBecomeDispleased());

    }

    public void interact(){
        Debug.Log("interacting");
        if(mainCharacter.isHoldingItem){
            NPCIsPleased = true;
            if(needsPizza && mainCharacter.hasPizza)
            {
                pizzaImage.SetActive(false);
                needsPizza = false;
                gameController.incrementHypeChangeRate(hypeModifier * -1);
                gameController.totalNeeds--;
            } 
            else if(needsBeer && mainCharacter.hasBeer)
            {
                beerImage.SetActive(false);
                needsBeer = false;
                gameController.incrementHypeChangeRate(hypeModifier * -1);
                gameController.totalNeeds--;
            }
            this.GetComponent<AudioSource>().Play();
            Debug.Log("interacting with item");
        }
    }
}
