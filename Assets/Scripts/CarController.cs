using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject carBody;
    public AudioClip collisionSound;

    private AudioSource audioSource;
    private float currentSpeed;
    private float pitchCar;

    private float horizontalInput, verticalInput;
    private float currentBreakForce, currentSteerAngle;
    private bool isBreaking;

    private Chrono chronoScript;
    private Rigidbody rb;

    [SerializeField] private float motorForce, breakForce, maxSteerAngle, minSpeedSound, maxSpeedSound, maxSpeed, minPitch, maxPitch;

    [SerializeField] private WheelCollider frontWheelLeftCollider, frontWheelRightCollider, backWheelLeftCollider, backWheelRightCollider;
    [SerializeField] private Transform frontWheelLeftTransform, frontWheelRightTransform, backWheelLeftTransform, backWheelRightTransform;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Renderer carRenderer = carBody.GetComponent<Renderer>();
        chronoScript = GameObject.Find("Controller").GetComponent<Chrono>();
        rb = GetComponent<Rigidbody>();

        switch(PlayerPrefs.GetString("Color"))
        {
            case "Yellow": carRenderer.material = Resources.Load<Material>("Materials/Car_Colors/YellowCar_Mat");
                break;
            case "Blue": carRenderer.material = Resources.Load<Material>("Materials/Car_Colors/BlueCar_Mat");
                break;
            case "Red": carRenderer.material = Resources.Load<Material>("Materials/Car_Colors/RedCar_Mat");
                break;
            case "Gray": carRenderer.material = Resources.Load<Material>("Materials/Car_Colors/GrayCar_Mat");
                break;
            case "Purple": carRenderer.material = Resources.Load<Material>("Materials/Car_Colors/PurpleCar_Mat");
                break;
        }   
    }

    void FixedUpdate()
    {
        if(!chronoScript.GetFinishedRace())
        {
            GetInput();
        }

        Motor();
        Steering();
        UpdateAllWheels();
        EngineSound();
    }

    
    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }
   
    void Motor()
    {
        float speed = verticalInput * motorForce;

        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
            speed = 0f;
        }

        frontWheelLeftCollider.motorTorque = speed;
        frontWheelRightCollider.motorTorque = speed;
        backWheelLeftCollider.motorTorque = 1;
        backWheelRightCollider.motorTorque = 1;
        currentBreakForce = isBreaking ? breakForce : 0f;
        Break();
    }
    
    void Break()
    {
       frontWheelLeftCollider.brakeTorque = currentBreakForce;
       frontWheelRightCollider.brakeTorque = currentBreakForce;
       backWheelLeftCollider.brakeTorque = currentBreakForce;
       backWheelRightCollider.brakeTorque = currentBreakForce; 
    }

    void Steering()
    {
       currentSteerAngle = maxSteerAngle * horizontalInput;
       frontWheelLeftCollider.steerAngle = currentSteerAngle;
       frontWheelRightCollider.steerAngle = currentSteerAngle;  
    }

    void EngineSound()
    {
        currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        pitchCar = GetComponent<Rigidbody>().velocity.magnitude / 50f;

        if(currentSpeed <= minSpeedSound)
        {
            audioSource.pitch = minPitch;
        }

        if(currentSpeed > minSpeedSound && currentSpeed < maxSpeedSound)
        {
            audioSource.pitch = minPitch + pitchCar;
        }

        if(currentSpeed >= maxSpeedSound)
        {
            audioSource.pitch = maxPitch;
        }
    }

    void UpdateAllWheels()
    {
        UpdateWheel(frontWheelLeftCollider, frontWheelLeftTransform);
        UpdateWheel(frontWheelRightCollider, frontWheelRightTransform);
        UpdateWheel(backWheelLeftCollider, backWheelLeftTransform);
        UpdateWheel(backWheelRightCollider, backWheelRightTransform);        
    }

    private void UpdateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
       Vector3 position;
       Quaternion rotation;
       wheelCollider.GetWorldPose(out position, out rotation);
       wheelTransform.rotation = rotation;
       wheelTransform.position = position;
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactForce = collision.relativeVelocity.magnitude;


        if (impactForce > 10f)
        {
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(collisionSound);
        }

    }
}
