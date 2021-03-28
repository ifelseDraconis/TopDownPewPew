using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 

public class UIManager : MonoBehaviour
{
    public static UIManager thisUIGuru { get; private set; }

    // this is the player health bar UI
    [SerializeField, Tooltip("This is the player Camera UI.")]
    private PlayerUI thePlayerUI;    

    private void Awake()
    {
        if (thisUIGuru == null)
        {
            thisUIGuru = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterThePlayer(GameObject Player)
    {
        thePlayerUI.SetTarget(Player);
    }

    
}
