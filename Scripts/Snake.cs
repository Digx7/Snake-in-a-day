using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : CustomMonoBehaviorWrapper
{
    [SerializeField] private int ID;
    [SerializeField] private Color color;
    [SerializeField] private Gradient trailGradient;
    [SerializeField] private List<Transform> allSegments;
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private GameObject deathParticlesPrefab;

    [SerializeField] private VoidEventChannelSO growRequestChannelSo;
    [SerializeField] private IntEventChannelSO playerDeathChannelSo;

    private void Awake()
    {
        allSegments.Add(this.transform);
        this.GetComponent<SpriteRenderer>().color = color;

        this.GetComponent<TrailRenderer>().colorGradient = trailGradient;

        this.GetComponent<CollisionDetector>().OnHit.AddListener(Kill);
    }

    private void OnEnable()
    {
        growRequestChannelSo.OnEventRaised += () => AddSegment();
    }

    private void OnDisable()
    {
        growRequestChannelSo.OnEventRaised -= () => AddSegment();
    }

    public void AddSegment()
    {
        // Instatiate segement
        GameObject newSegment = Instantiate(segmentPrefab, allSegments[allSegments.Count - 1].Find("Tail").transform.position, Quaternion.identity);

        // Make it follow the last segment
        newSegment.GetComponent<Follower>().objectToFollow = allSegments[allSegments.Count - 1].Find("Tail");

        // Set it to the correct color
        newSegment.GetComponent<SpriteRenderer>().color = color;
        newSegment.GetComponent<TrailRenderer>().colorGradient = trailGradient;

        // Add it to the tail
        allSegments.Add(newSegment.transform);
    }

    private void Kill()
    {
        Log("I died");
        
        foreach (Transform segment in allSegments)
        {
            SpawnDeathParticles(segment.transform.position);
            Destroy(segment.gameObject);
        }

        playerDeathChannelSo.RaiseEvent(ID);

        SpawnDeathParticles(this.transform.position);
        Destroy(this.gameObject);
    }

    private void SpawnDeathParticles(Vector3 position)
    {
        GameObject deathParticles = Instantiate(deathParticlesPrefab, position, Quaternion.identity);
            ParticleSystem.MainModule mainModule = deathParticles.GetComponent<ParticleSystem>().main;
            mainModule.startColor = color;
    }

}
