using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singlenton<GameManager>
{

    [SerializeField] public bool onPause = false;
    public bool[] finals = new bool[4] { false, false, false, false };
    
   
    private void Start()
    {
        onPause = false;
    }
}
