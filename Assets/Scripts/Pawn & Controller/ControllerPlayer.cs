using UnityEngine;
using UnityEngine.InputSystem;
public class ControllerPlayer : Controller
{


    public Key moveForwardKey;
    public Key moveBackwardKey;
    public Key rotateClockwiseKey;
    public Key rotateCounterClockwiseKey;
    public Key pitchUpKey;
    public Key pitchDownKey;
    public Key rollClockwiseKey;
    public Key rollCounterClockwiseKey;
    public Key turboKey;
    public Key shootKey;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LevelManager.instance != null)
        {
            LevelManager.instance.controllerPlayer =this;
            LevelManager.instance.pawnShip = Pawn as PawnShip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void ProcessInputs()
    {
        // TODO: Process Controls

        if (Keyboard.current[moveForwardKey].isPressed)
        {
            Pawn.Move(Pawn.transform.forward);
        }

        if (Keyboard.current[moveBackwardKey].isPressed)
        {
            Pawn.Move(-Pawn.transform.forward);
        }

        if (Keyboard.current[pitchUpKey].isPressed)
        {
            Pawn.Pitch(-1);
        }

        if (Keyboard.current[pitchDownKey].isPressed)
        {
            Pawn.Pitch(1);
        }

        if (Keyboard.current[rotateClockwiseKey].isPressed)
        {
            Pawn.Yaw(1);
        }

        if (Keyboard.current[rotateCounterClockwiseKey].isPressed)
        {
            Pawn.Yaw(-1);
        }

        if (Keyboard.current[rollClockwiseKey].isPressed)
        {
            Pawn.Roll(-1);
        }

        if (Keyboard.current[rollCounterClockwiseKey].isPressed)
        {
            Pawn.Roll(1);
        }

        if (Keyboard.current[shootKey].wasPressedThisFrame)
        {
            Pawn.Shoot();
        }
    }
}
