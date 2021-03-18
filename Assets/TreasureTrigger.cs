using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Ship" ) {
            // Play treasure collect sound.

            // Add to player treasure score.
            Score score = collider.GetComponent<Score>();

            if(score) {
                score.AddScore();
            }

            // Destroy chest.
            Destroy(gameObject);
        }
    }

}
