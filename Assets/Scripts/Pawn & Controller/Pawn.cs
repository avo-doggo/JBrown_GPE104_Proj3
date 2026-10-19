using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Controller controller;
    
    public abstract void Move(Vector3 moveDirection);
    public abstract void Yaw(float yawAmount);
    public abstract void Pitch( float pitchAmount);
    public abstract void Roll(float rollAmount);
    public abstract void Shoot();

    //seek out a target object or posistion
    public abstract void Seek(Transform target);
    public abstract void Seek(Vector3 position);
    public abstract void Seek(GameObject target);

}
