
using Mirror;
using UnityEngine;

public class audioHelper : NetworkBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public player player;

    // Update is called once per frame
    [Command]
    public void Play(int num)
    {
        if (player.controller.isGrounded)
        {
            float randomPitch = Random.Range(1f, 2.0f);
            float randomVolume = Random.Range(0.1f, 0.2f);

            // Применяем случайные значения к звуку
            audioSource.pitch = randomPitch;
            audioSource.volume = randomVolume;
            audioSource.PlayOneShot(audioClips[num - 1]);
        }
    }
}
