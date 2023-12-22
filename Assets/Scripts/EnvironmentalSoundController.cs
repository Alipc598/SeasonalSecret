using UnityEngine;

public class EnvironmentalSoundController : MonoBehaviour
{
    public AudioSource environmentalAudioSource; // Assign in the Inspector
    public float indoorVolume = 0.1f;           // Very low volume for indoors
    public float outdoorVolume = 1.0f;          // Higher volume for outdoors

    private GameObject player;                   // Reference to the player

    void Start()
    {
        // Find the player in the scene
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        RaycastHit hit;
        // Raycast down from the player's position to determine the surface type
        if (Physics.Raycast(player.transform.position, Vector3.down, out hit))
        {
            if (hit.collider.tag == "Indoors")
            {
                // If the player is indoors, set the volume to very low
                environmentalAudioSource.volume = indoorVolume;
            }
            else if (hit.collider.tag == "Outdoors")
            {
                // If the player is outdoors, set the volume to higher
                environmentalAudioSource.volume = outdoorVolume;
            }
        }
    }
}
