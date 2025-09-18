using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float MoveDirectionX { get; private set; }
    public float AttackDirectionY { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool JumpHeld { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool Attack2Pressed { get; private set; }
    public bool Attack3Pressed { get; private set; }

    private void Update()
    {
        AttackDirectionY = Input.GetAxisRaw("Vertical");
        MoveDirectionX = Input.GetAxisRaw("Horizontal");
        JumpPressed = Input.GetButtonDown("Jump");
        JumpHeld = Input.GetButton("Jump");
        AttackPressed = Input.GetButtonDown("Fire1"); 
        Attack2Pressed = Input.GetButtonDown("Fire2"); 
        Attack3Pressed = Input.GetButtonDown("Fire3"); 
    }

    public void ClearInputs()
    {
        JumpPressed = false;
        AttackPressed = false;
    }
}
