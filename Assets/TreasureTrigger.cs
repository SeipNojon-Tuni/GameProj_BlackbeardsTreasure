using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureTrigger : MonoBehaviour
{   
    public AudioSource auso;
    private bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        if(!auso) {
            auso = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Ship" && alive) {
            // Play treasure collect sound.

            // Add to player treasure score.
            Score score = collider.GetComponent<Score>();

            if(score) {
                score.AddScore();
            }
            transform.localScale = new Vector3(0, 0, 0);
            alive = false;
            StartCoroutine(Death());

        }
    }

    IEnumerator Death() {
        if(auso) {
            auso.Play();
            yield return new WaitForSeconds(auso.clip.length);
        }

        // Destroy chest.
        Destroy(gameObject);
    }

}
