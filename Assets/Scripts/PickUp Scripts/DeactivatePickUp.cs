using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class attached the Bomb and Fruit prefabs.
public class DeactivatePickUp : MonoBehaviour
{
    #region Destroying Pickups 
    void Start()
    {
        Invoke("Deactivate", Random.Range(6f, 9f));
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    #endregion

}
