using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class PlayerUI : MonoBehaviour
{
    public Player player;

    [SerializeField, Tooltip("This is the text for this player.")]
    private Text textHealth;

    [SerializeField, Tooltip("This is the text for this player.")]
    private Slider healthFloat;

    [SerializeField, Tooltip("These are the lives the player has.")]
    private Text textLives;

    [SerializeField, Tooltip("These are the lives the player has.")]
    private Image currentWeaponGraphic;

    [SerializeField, Tooltip("These are the lives the player has.")]
    private Sprite[] replacementGraphics;

    [SerializeField, Tooltip("This is the pause menu.")]
    private GameObject pauseMenu;

    [SerializeField, Tooltip("This is the game over menu.")]
    private GameObject gameOverMenu;

    private void Awake()
    {
        if (currentWeaponGraphic.sprite == null)
        {
            currentWeaponGraphic.enabled = false;
        }
    }

    // fetches the relevant data whenever the player is killed before updating
    private void Update()
    {
        if (player != null)
        {
            textHealth.text = string.Format("Health: {0}%", Mathf.RoundToInt(player.health.Percent * 100f));
            textLives.text = string.Format("Lives: {0}", GameManager.thisGameManager.LifeCount);
            healthFloat.value = player.health.Percent * 100f;
            if (player.equippedWeapon != null)
            {
                if (player.equippedWeapon.getWeaponDes() == WeaponClass.WeaponName.Blaster || player.equippedWeapon.getWeaponDes() == WeaponClass.WeaponName.MachineGune)
                {
                    if (player.equippedWeapon.getWeaponDes() == WeaponClass.WeaponName.Blaster)
                    {
                        currentWeaponGraphic.sprite = replacementGraphics[0];
                    }
                    else if (player.equippedWeapon.getWeaponDes() == WeaponClass.WeaponName.MachineGune)
                    {
                        currentWeaponGraphic.sprite = replacementGraphics[1];
                    }
                    else
                    {
                        currentWeaponGraphic.sprite = null;
                    }
                }
            }            
            else
            {
                currentWeaponGraphic.enabled = false;
            }
            if (currentWeaponGraphic.sprite != null)
            {
                currentWeaponGraphic.enabled = true;
            }
        }
        else
        {
            GameObject thisPlayer = GameObject.FindGameObjectWithTag("PlayerContainer");
            if (thisPlayer != null)
            {
                player = thisPlayer.GetComponent<Player>();
            }
        }
    }

    public void enablePauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void disablePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void enableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void disableGameOverMenu()
    {
        gameOverMenu.SetActive(false);
    }

    public void SetTarget(GameObject newPlayer)
    {
        player = newPlayer.GetComponent<Player>();
    }

}
