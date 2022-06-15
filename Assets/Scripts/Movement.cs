using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float thrustForce = 300f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    
    AudioSource audioSource;

    bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProccessThrust();
        ProccessRotation();
    }


    //Audio source added here
    void ProccessThrust(){
        if(Input.GetKey(KeyCode.Space)){
           StartThrusting();
        } else {
            StopThrusting();
        }
    }

    void StartThrusting(){
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);    //here
        if(!mainEngineParticles.isPlaying)
            mainEngineParticles.Play();
    }

    void StopThrusting(){
            audioSource.Stop();
            mainEngineParticles.Stop();
    }

    void ProccessRotation(){

        if(Input.GetKey(KeyCode.A)){
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D)){
            RotateRight();
        } else {
          StopRotating();
        }
    }

    void RotateLeft(){
        ApplyRotation(rotateSpeed);
            if(!leftThrusterParticles.isPlaying)
                leftThrusterParticles.Play();
    }

    void RotateRight(){
        ApplyRotation(-rotateSpeed);    
            if(!rightThrusterParticles.isPlaying)
                rightThrusterParticles.Play();
    }

    void StopRotating(){
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame){
        rb.freezeRotation=true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation=false; 
    }
}
