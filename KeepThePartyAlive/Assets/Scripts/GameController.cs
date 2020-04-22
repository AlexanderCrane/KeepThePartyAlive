using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float HYPE = 50.0f;
    float defaultHypeChangeRate = -0.01f;
    float hypeChangeRate;
    public int maxNeeds = 5;
    public int totalNeeds = 0;

    public float rotateSpeed = 5;
    public RectTransform hypeBar;

    public GameObject WinCanvas;
    public GameObject LoseCanvas;
    public GameObject crowdSounds;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        hypeChangeRate = defaultHypeChangeRate;
        WinCanvas.SetActive(false);
        LoseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(HYPE <= 0){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            hypeChangeRate = 0;
            Time.timeScale =  0;
            LoseCanvas.SetActive(true);
            Camera.main.GetComponent<MusicPlayer>().enabled = false;
            Camera.main.GetComponent<AudioSource>().Stop();
            crowdSounds.GetComponent<AudioSource>().Stop();
            //lose game
        } else if(HYPE >= 100) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            hypeChangeRate = 0;
            Time.timeScale =  0;
            WinCanvas.SetActive(true);
            WinCanvas.GetComponent<AudioSource>().Play();
            Camera.main.GetComponent<MusicPlayer>().enabled = false;
            Camera.main.GetComponent<AudioSource>().Stop();
            crowdSounds.GetComponent<AudioSource>().Stop();
            //win game
        }

        HYPE += hypeChangeRate;

        hypeBar.sizeDelta = new Vector2(hypeBar.sizeDelta.x, 860f / (100f/HYPE));
        
    }

    public void incrementHypeChangeRate(float newRate){
        hypeChangeRate += newRate;
    }

    public void resetHypeChangeRate(){
        hypeChangeRate = defaultHypeChangeRate;
    }

    public void incrementHype(float increase){
        HYPE += increase;
    }
}
