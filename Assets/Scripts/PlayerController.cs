using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] int velocity;
    Vector2 movement;
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 currentPosition = myRigidbody.position;        
        Vector3 inputPosition = new Vector3(movement.x, 0, movement.y);
        Vector3 newPosition = currentPosition + inputPosition * (velocity * Time.fixedDeltaTime);
        float xClampValue =  Mathf.Clamp(newPosition.x, -2.2f, 2.2f);
        float zClampValue =  Mathf.Clamp(newPosition.z, -1.3f, .05f);

       

        myRigidbody.MovePosition(new Vector3(xClampValue, 0, zClampValue));
    }

    public void Movement(InputAction.CallbackContext context) {

        movement = context.ReadValue<Vector2>();        
    }
}
