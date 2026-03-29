using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource footstepSource;
    public AudioSource sfxSource;

    [Header("Footstep Clips")]
    public AudioClip walkClip;
    public AudioClip runClip;

    [Header("Other Clips")]
    public AudioClip shootClip;
    public AudioClip reloadClip;

    [Header("Footstep Settings")]
    [Range(0f, 1f)] public float walkVolume = 0.25f;
    [Range(0f, 1f)] public float runVolume = 0.35f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (footstepSource != null)
        {
            footstepSource.loop = true;
            footstepSource.playOnAwake = false;
            footstepSource.Stop();
        }

        if (sfxSource != null)
        {
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
            sfxSource.Stop();
        }
    }

    void Update()
    {
        HandleFootsteps();
    }

    void HandleFootsteps()
    {
        if (footstepSource == null) return;

        float moveAmount = 0f;

        if (rb != null)
            moveAmount = rb.velocity.magnitude;
        else
            moveAmount = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).magnitude;

        bool isMoving = moveAmount > 0.1f;
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        if (!isMoving)
        {
            if (footstepSource.isPlaying)
                footstepSource.Stop();

            return;
        }

        AudioClip targetClip = isRunning ? runClip : walkClip;
        float targetVolume = isRunning ? runVolume : walkVolume;

        if (footstepSource.clip != targetClip)
        {
            footstepSource.Stop();
            footstepSource.clip = targetClip;
            footstepSource.volume = targetVolume;

            if (targetClip != null)
                footstepSource.Play();
        }
        else
        {
            if (!footstepSource.isPlaying && targetClip != null)
                footstepSource.Play();

            footstepSource.volume = targetVolume;
        }
    }

    public void PlayShootSound()
    {
        if (sfxSource == null || shootClip == null) return;

        sfxSource.Stop();
        sfxSource.PlayOneShot(shootClip);
    }

    public void PlayReloadSound()
    {
        if (sfxSource == null || reloadClip == null) return;

        sfxSource.Stop();
        sfxSource.PlayOneShot(reloadClip);
    }
}