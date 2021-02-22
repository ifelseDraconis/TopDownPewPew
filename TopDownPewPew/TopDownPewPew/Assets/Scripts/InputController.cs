using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Animator thisAnimate;

    private float goesUp;
    private float goesRight;
    private Vector3 goesYander;
    private Vector3 begetsYander;
    private Vector3 begetsTsundere;
    // Start is called before the first frame update
    void Start()
    {
        thisAnimate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // converts the local transform into a global modifier
        begetsYander = transform.InverseTransformDirection(Vector3.forward);
        begetsTsundere = transform.InverseTransformDirection(Vector3.right);

        goesUp = Input.GetAxis("Vertical");
        goesRight = Input.GetAxis("Horizontal");

        // multiplies the conversion to the inputs to produce movement that reflects button press rather than facing
        goesYander = new Vector3(goesUp * begetsTsundere.x, 0.0f, goesRight * begetsYander.z);

        // declares the float result to the animation controller
        thisAnimate.SetFloat("Forward", goesYander.x);
        thisAnimate.SetFloat("Right", goesYander.z);
    }
}
