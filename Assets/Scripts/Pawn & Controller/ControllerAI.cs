using UnityEngine;

public class AIController : Controller
{

    public GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MakeDecisions();
    }

    public void MakeDecisions()
    {
        //Chase the player
        Pawn.Seek(target);
    }
}
