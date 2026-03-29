using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    public AudioSource zombieAudioSource;
    public float soundRange = 8f;

    private Transform player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        if (zombieAudioSource == null)
        {
            zombieAudioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (zombieAudioSource == null || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= soundRange)
        {
            if (!zombieAudioSource.isPlaying)
                zombieAudioSource.Play();
        }
        else
        {
            if (zombieAudioSource.isPlaying)
                zombieAudioSource.Stop();
        }
    }
}