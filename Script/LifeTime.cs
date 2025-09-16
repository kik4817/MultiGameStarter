using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float time = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, time);
    }
}
