using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstepAudioSource; // Assign this in the Inspector
    public AudioClip footstepWoodClip;      // Assign the footstep sound clip for wooden floor
    public AudioClip footstepGrassClip;     // Assign the footstep sound clip for grass
    public AudioClip jumpClip;              // Assign the jump sound clip

    private bool isMoving = false;
    private AudioClip currentFootstepClip;

    void Update()
    {
        // Movement and footstep logic
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            if (!isMoving)
            {
                StartFootsteps();
            }
            else
            {
                UpdateFootstepClip();
            }
        }
        else
        {
            if (isMoving)
            {
                StopFootsteps();
            }
        }

        // Jumping logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayJumpSound();
        }
    }

    void StartFootsteps()
    {
        isMoving = true;
        currentFootstepClip = DetermineFootstepClip();
        footstepAudioSource.clip = currentFootstepClip;
        footstepAudioSource.loop = true;
        footstepAudioSource.Play();
    }

    void StopFootsteps()
    {
        isMoving = false;
        footstepAudioSource.Stop();
    }

    void UpdateFootstepClip()
    {
        AudioClip newClip = DetermineFootstepClip();
        if (currentFootstepClip != newClip)
        {
            currentFootstepClip = newClip;
            footstepAudioSource.clip = newClip;
            footstepAudioSource.Play();
        }
    }

    AudioClip DetermineFootstepClip()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.tag == "Indoors")
                return footstepWoodClip;
            else if (hit.collider.tag == "Outdoors")
                return footstepGrassClip;
        }
        return footstepWoodClip; // Default to wood sound if no surface is detected
    }

    void PlayJumpSound()
    {
        footstepAudioSource.PlayOneShot(jumpClip);
    }
}
