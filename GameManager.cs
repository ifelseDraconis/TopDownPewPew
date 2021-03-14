using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager thisGameManager;

    public AudioClip thisMusic;

    private void Awake()
    {
        if (thisGameManager != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            thisGameManager = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlayMusic(thisMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
