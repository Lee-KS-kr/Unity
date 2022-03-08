using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float horsePower = 10.0f;
    [SerializeField]private float speed;
    [SerializeField]private float turnSpeed = 25.0f;
    private int onGround;
    private float rpm;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;
    [SerializeField] private GameObject centerOfMass;
    [SerializeField] private TextMeshProUGUI speedoMeter;
    [SerializeField] private TextMeshProUGUI rpmText;
    [SerializeField] private List<WheelCollider> allWheels;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    void FixedUpdate()
    {
        // Move the vehicle forward
        forwardInput = Input.GetAxis("Vertical");
        // Turn the vehicle left and right
        horizontalInput = Input.GetAxis("Horizontal");

        if (IsOnGround())
        {
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
            playerRb.AddRelativeForce(Vector3.forward * forwardInput * horsePower);
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 3.7f);
            speedoMeter.text = $"Speed : {speed}km/h";

            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.text = $"RPM : {rpm}";
        }
    }

    private bool IsOnGround()
    {
        foreach(WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                return true;
            }
        }

        return false;
    }
}
