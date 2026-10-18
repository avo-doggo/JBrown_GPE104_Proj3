using UnityEngine;

public class DeathPawn : Death
{
    private Controller controller;

    public override void Die()
    {
        // Tell the LevelManager that the player lost a life
        if (LevelManager.instance != null)
        {
            LevelManager.instance.LoseLife();
        }
    }

    public override void Start()
    {
        controller = GetComponent<Controller>();
    }

    public override void Update()
    {
    }
}