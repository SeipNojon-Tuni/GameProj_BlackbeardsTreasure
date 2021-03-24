using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{   
    public static int victoryIndex = 1; // 1 for animals, 2 for aliens.
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setVictory(int newIndex) {
        victoryIndex = newIndex;
    }
    public int getVictory() {
        return victoryIndex;
    }

}
