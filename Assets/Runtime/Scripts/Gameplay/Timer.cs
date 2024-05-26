using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool Enabled {get;set;}
    private float elapsedTime;

    void Start()
    {
        elapsedTime = 0f;
    }

    void Update()
    {
        if(Enabled)
            elapsedTime += Time.deltaTime;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
