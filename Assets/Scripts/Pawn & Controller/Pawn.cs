using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Controller controller;
    
    public abstract void Move(Vector3 moveDirection);
    public abstract void Yaw(float yawAmount);
    public abstract void Pitch( float pitchAmount);
    public abstract void Roll(float rollAmount);
    public abstract void Shoot();
    public abstract void Turbo();


}
