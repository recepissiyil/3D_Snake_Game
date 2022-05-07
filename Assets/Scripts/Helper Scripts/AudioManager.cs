using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip pickUpSound, deadSound;

    #region Singleton
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    } 
    #endregion

    #region Sounds when Player touch the wall or fruit
    public void PlaySound(int index)
    {
        if (GamePlayController.instance.isSoundActive)
        {
            switch (index)
            {
                case 0:
                    AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
                    break;
                case 1:
                    AudioSource.PlayClipAtPoint(deadSound, transform.position);
                    break;
                default:
                    break;
            }
        }
    } 
    #endregion
}
