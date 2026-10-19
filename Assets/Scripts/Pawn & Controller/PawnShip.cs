using UnityEngine;

public class PawnShip : Pawn
{
    public float moveForce;
    public float rotateSpeed;
    public float turboSpeed;

    private Spawner sh;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        sh = GetComponent<Spawner>();


    }
    public override void Move(Vector3 moveDirection)
    {
        //push in the direction passed in, with moveForce force
        Vector3 pushForce = moveDirection * moveForce;
        rb.AddForce(pushForce);
    }

    public override void Yaw(float direction)
    {
        //Rotate around the Y axis <turns left and right>
        transform.Rotate(0, direction * rotateSpeed * Time.deltaTime, 0);
    }

    public override void Pitch(float direction)
    {
        //Rotate around the X axis <turns up and down>
        transform.Rotate(direction * rotateSpeed * Time.deltaTime, 0, 0);
    }

    public override void Roll(float direction)
    {
        //Rotate around the Z axis <rolls left and right>
        transform.Rotate(0, 0, direction * rotateSpeed * Time.deltaTime);
    }

    public override void Shoot()
    {
        //shoots bullets from the spawner if it exists
        if (sh != null)
        {
            sh.Shoot();
        }
    }

    public override void Seek(Transform target)
    {
        Seek(target.position);
    }

    public override void Seek(Vector3 position)
    {
        //rotate to look at position
        RotateTowards(position);

        //Move forward
        Move(transform.forward);
    }

    public override void Seek(GameObject target)
    {
        Seek(target.transform);
    }

    public void RotateTowards(Vector3 targetPoint)
    {
        //find the vector that points toward target point
        Vector3 vectorToTarget = targetPoint - transform.position;
        //find the rotation instructions Quaternion
        Quaternion lookRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        //rotate a little bit closer
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
    }
}
