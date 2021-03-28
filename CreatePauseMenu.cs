using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePauseMenu : MonoBehaviour
{
    [SerializeField, Tooltip("This UI for pausing.")]
    private GameObject thisPauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject createPauseMenu()
    {
        return Instantiate(thisPauseMenu, transform);
    }
}
