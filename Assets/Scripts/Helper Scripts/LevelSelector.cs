using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This class is attached Canvas at scene 11
public class LevelSelector : MonoBehaviour
{
    #region Level scene
    public AudioClip buttonSound;
    public void LevelSelect(int index)
    {
        AudioSource.PlayClipAtPoint(buttonSound, Vector3.zero);
        SceneManager.LoadScene(index);
    } 
    #endregion
}
