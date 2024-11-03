using UnityEngine;

public class SFXPlaying : MonoBehaviour
{
    public static SFXPlaying instance;
    [SerializeField] private AudioSource soundFX;
    
    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void playSFXCllip(AudioClip audioClip, Transform spawnTransform, float volume) {
        AudioSource audioSource = Instantiate(soundFX, spawnTransform.position, Quaternion. identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        Debug.Log("Entered");
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
    
}
