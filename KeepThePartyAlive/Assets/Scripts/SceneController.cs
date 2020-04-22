using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject instructions;

    void Awake(){
        if((SceneManager.GetActiveScene().buildIndex == 0) && (instructions != null)){
            instructions.SetActive(false);
        }
    }

    public void PlayGame(){
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Playing");
        Time.timeScale =  1;
        SceneManager.LoadScene(1);
    }

    public void Exit(){
        Application.Quit();
    }

    public void LoadMenu(){
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale =  1;
        SceneManager.LoadScene(0);
    }

}
