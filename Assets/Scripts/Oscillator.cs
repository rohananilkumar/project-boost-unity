using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    // [SerializeField] [Range(0,1)] float movementFactor; //Range property gives slider controls on unity editor
    float movementFactor;
    [SerializeField] float period = 10f; //Range property gives slider controls on unity editor

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        movementFactor = 10f;
        startingPosition = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) return;     // Avoid divide by zero error
        float cycles = Time.time / period;  // Continually growing over time
        
        const float tau = Mathf.PI * 2;     // Constant value
        float rawSinWave = Mathf.Sin(cycles * tau);     // Going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;    // recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor * rawSinWave;
        transform.position = startingPosition + offset; // Udpating the position
    }
}
