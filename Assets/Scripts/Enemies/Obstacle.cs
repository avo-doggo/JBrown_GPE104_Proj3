using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (LevelManager.instance != null)
        {
            LevelManager.instance.AddObstacle(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
