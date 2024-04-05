using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float topSpeed;
    public float jumpForce;

    [Header("Body")]
    public Transform body;

    [Header("FMOD")]
    public string jumpSFXPath;
    public string deathSFXPath;

    private Vector3 startingPosition;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, 1f);

        if (Input.GetButtonDown("Jump") && hit.transform != null)
        {
            Jump();
        }

        FaceMovement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y, Input.GetAxis("Vertical") * speed);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, topSpeed);
    }

    private void Jump()
    {
        rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);

        //Create an instance of the SFX.
        FMOD.Studio.EventInstance jumpSFX = FMODUnity.RuntimeManager.CreateInstance(jumpSFXPath);
        //Attach the instance to this GameObject.
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(jumpSFX, this.transform);
        //Play SFX
        jumpSFX.start();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Lava")
        {
            transform.position = startingPosition;

            //Create an instance of the SFX.
            FMOD.Studio.EventInstance deathSFX = FMODUnity.RuntimeManager.CreateInstance(deathSFXPath);
            //Attach the instance to this GameObject.
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(deathSFX, this.transform);
            //Play SFX
            deathSFX.start();
            
        }
    }

    private void FaceMovement()
    {
        body.forward = rb.velocity.normalized;
    }
}
