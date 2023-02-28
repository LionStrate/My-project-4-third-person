using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float bobSpeed = 0.5f;
    public float bobHeight = 0.5f;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        // Rotate the object
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Bob the object up and down
        float bobOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = startPosition + Vector3.up * bobOffset;

    }
}
