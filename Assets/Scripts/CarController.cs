using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject carBody;

    private AudioSource audioSource;
    private float currentSpeed;
    private float pitchCar;

    private float horizontalInput, verticalInput;
    private float currentBreakForce, currentSteerAngle;
    private bool isBreaking;

    [SerializeField] private float motorForce, breakForce, maxSteerAngle, minSpeed, maxSpeed, minPitch, maxPitch;

    [SerializeField] private WheelCollider frontWheelLeftCollider, frontWheelRightCollider, backWheelLeftCollider, backWheelRightCollider;
    [SerializeField] private Transform frontWheelLeftTransform, frontWheelRightTransform, backWheelLeftTransform, backWheelRightTransform;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Renderer carRenderer = carBody.GetComponent<Renderer>();

        switch(PlayerPrefs.GetString("Color"))
        {
            case "Yellow": carRenderer.material = Resources.Load<Material>("Materials/Car_Colors/YellowCar_Mat");
                break;
            case "Blue": carRenderer.material = Resources.Load<Material>("Materials/Car_Colors/BlueCar_Mat");
                break;
            case "Red": carRenderer.material = Resources.Load<Material>("Materials/Car_Colors/RedCar_Mat");
                break;
        }   
    }

    void FixedUpdate()
    {
        GetInput();
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

        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        frontWheelLeftCollider.motorTorque = speed;
        frontWheelRightCollider.motorTorque = speed;
        backWheelLeftCollider.motorTorque = speed;
        backWheelRightCollider.motorTorque = speed;
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

        if(currentSpeed <= minSpeed)
        {
            audioSource.pitch = minPitch;
        }

        if(currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            audioSource.pitch = minPitch + pitchCar;
        }

        if(currentSpeed >= maxSpeed)
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
}
