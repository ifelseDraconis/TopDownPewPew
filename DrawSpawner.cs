using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("Spawn area.")]
    private Vector3 scale;

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
        Gizmos.color = Color.Lerp(Color.red, Color.clear, 0.5f);
        Gizmos.DrawCube (Vector3.up * scale.y / 2f, scale);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Vector3.zero, Vector3.forward * 0.4f);
    }
}
