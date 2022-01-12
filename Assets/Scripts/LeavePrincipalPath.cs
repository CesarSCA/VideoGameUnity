using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeavePrincipalPath : MonoBehaviour
{
    public static event Action OnForestEnter;
    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            OnForestEnter?.Invoke();
            Destroy(gameObject);
        }
    }
}
