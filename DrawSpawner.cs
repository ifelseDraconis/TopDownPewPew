using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("Spawn area.")]
    private Vector3 scale;

    [SerializeField, Tooltip("This is the color choice for the spawner.")]
    private Color thisColorChoice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = Color.Lerp(thisColorChoice, Color.clear, 0.5f);
        Gizmos.DrawCube (Vector3.up * scale.y / 2f, scale);
        Gizmos.color = thisColorChoice;
        Gizmos.DrawRay(Vector3.zero, Vector3.forward * 0.4f);
    }
}
