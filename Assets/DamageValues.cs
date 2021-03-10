using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageValues : MonoBehaviour
{   
    public int duration = 1;
    public float max_damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getDamage() {
        return Random.Range(0.7f, 1.0f) * max_damage;
    }

    public float getDuration() {
        // No tick amount for single instance damage.
        return Mathf.Floor(Random.Range(0.6f, 1.0f) * duration); // Duration of 1 will result in 0.
    }
}
