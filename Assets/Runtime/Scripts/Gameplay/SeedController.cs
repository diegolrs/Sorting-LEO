using UnityEngine;

public class SeedController : MonoBehaviour
{
    [SerializeField] int _initialSeed;
    int _seed;

    private void Awake() => _seed = _initialSeed;

    public int Seed
    {
        get { return _seed; }
        set {_seed = value;}
    }
}
