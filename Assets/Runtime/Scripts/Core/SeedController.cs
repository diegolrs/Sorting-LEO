using UnityEngine;

public class SeedController : MonoBehaviour
{
    int _seed;

    public int Seed
    {
        get { return _seed; }
        set {_seed = value; Random.InitState(_seed);}
    }
}
