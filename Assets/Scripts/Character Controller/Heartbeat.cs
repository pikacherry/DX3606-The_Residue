using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; 


public class ProximityEffectsController : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform targetObject;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private Animator animator; 
    [SerializeField] private float animationTriggerDistance = 5f;

    private bool isEnemyNear = false;

    [Header("Audio Settings")]
    public float minVolume = 0.1f;
    public float maxVolume = 1f;
    public float minPitch = 0.8f;
    public float maxPitch = 2f;

    [Header("Vignette Settings")]
    public Volume postProcessVolume;
    public float minVignetteIntensity = 0.1f;
    public float maxVignetteIntensity = 0.5f;

    private AudioSource audioSource;
    private Vignette vignette;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogError("No AudioSource found on this GameObject.");

        if (postProcessVolume == null)
        {
            Debug.LogError("Post-process Volume reference not set.");
            return;
        }

        if (!postProcessVolume.profile.TryGet(out vignette))
        {
            Debug.LogError("Vignette override not found in Volume profile.");
        }

        if (animator == null)
        {
            Debug.LogError("No Animator found on this GameObject.");

        }
            
    }

    void Update()
    {
        if (targetObject == null || audioSource == null || vignette == null)
            return;

        float distance = Vector3.Distance(transform.position, targetObject.position);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        float proximityFactor = 1f - ((distance - minDistance) / (maxDistance - minDistance));

        // Audio updates
        audioSource.volume = Mathf.Lerp(minVolume, maxVolume, proximityFactor);
        audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, proximityFactor);

        // Vignette update
        vignette.intensity.value = Mathf.Lerp(minVignetteIntensity, maxVignetteIntensity, proximityFactor);

    
        if (distance <= animationTriggerDistance)
        {
            if (!isEnemyNear)
            {
                animator.SetTrigger("EnemyNear"); // your animation trigger name
                isEnemyNear = true;
            }
        }
        else
        {
            isEnemyNear = false;
        }
    }
}