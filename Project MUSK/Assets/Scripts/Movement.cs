using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip rocketThrust;

    Rigidbody rb;
    AudioSource audioSrc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation so that we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            if (!audioSrc.isPlaying)
            {
                audioSrc.PlayOneShot(rocketThrust);
            }
        }
        else
        {
            audioSrc.Stop();
        }
    }
}
