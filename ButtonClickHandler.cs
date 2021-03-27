using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{

    [SerializeField, Tooltip("This is the pause menu GameObject")]
    private GameObject pauseMenuObject;

    [SerializeField, Tooltip("This is the game over menu GameObject")]
    private GameObject gameOverMenuObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // executes script for when the resume button is pressed
    public void buttonResume()
    {
        hidePauseMenu();
    }

    // executes code for when the quit button is pressed
    public void buttonQuit()
    {
        Application.Quit();
    }

    // executes code for when the continue button is pressed
    public void buttonContinue()
    {
        //ToDo reload a start screen
    }

    // shows the pause menu
    private void showPauseMenu()
    {
        pauseMenuObject.SetActive(true);
    }

    // hide the pause menu
    private void hidePauseMenu()
    {
        pauseMenuObject.SetActive(false);
    }

    // show the game over menu
    private void showGameOverMenu()
    {
        gameOverMenuObject.SetActive(true);
    }

    // hide the game over menu
    private void hideGameOverMenu()
    {
        gameOverMenuObject.SetActive(false);
    }
}
