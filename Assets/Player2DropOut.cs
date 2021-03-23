using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2DropOut : MonoBehaviour
{   
    public Camera camP1;
    public Camera camP2;
    public Text joinText;

    GameObject p2_manowar;
    GameObject p2_cannonboat;

    bool mow_state;
    bool cb_state;

    // Start is called before the first frame update
    void Start()
    {
        joinText.enabled = true;
        p2_manowar = camP2.GetComponent<CharacterSwap>().manOWar;
        p2_cannonboat = camP2.GetComponent<CharacterSwap>().cannonBoat;

        // Get original values which ship was enabled.
        mow_state = p2_manowar.activeSelf;
        cb_state = p2_cannonboat.activeSelf;

        // Set Player 2 inactive at start
        camP1.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        p2_manowar.SetActive(false);
        p2_cannonboat.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.F2)){

            joinText.enabled = !joinText.enabled;

            if(camP1.rect.width == 1.0f) {
                camP1.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);

                p2_manowar.SetActive(mow_state);
                p2_cannonboat.SetActive(cb_state);
            }
            else {
                camP1.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);

                p2_manowar.SetActive(false);
                p2_cannonboat.SetActive(false);
            }

        }
    }
}
