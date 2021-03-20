using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    
    private Vector3 mousePos;
    private float lastAngle;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // this code snippet makes the player character look at the mouse, this only exists during the player existance

        // produce a raycast towards the mouse location
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        // distance is kind of a misnomer since it is actually a magnitude rather than an actual direction in and of itself
        float distance;

        // detects the hit location of the raycast if it hits the plane from the mouse position
        if (plane.Raycast(ray, out distance))
        {
            // this gets us the actual location of the hit

            Vector3 target = ray.GetPoint(distance);
            // direction is where its at, this tells us the vector we need to reach the goal

            Vector3 direction = target - transform.position;
            // this converts it to degrees since Radians don't compute too well in Quaternions

            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // this last part alters the rotation to match the y rotation
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

    }
}
