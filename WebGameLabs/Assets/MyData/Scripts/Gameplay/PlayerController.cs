using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject playerCam;
    [SerializeField] private ParticleSystem deathParticle;

    private HealthManager healthManager;

    private void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    private void OnEnable()
    {
        healthManager.OnDie += OnDie;
    }

    private void OnDisable()
    {
        healthManager.OnDie -= OnDie;
    }

    private void OnDie()
    {
        playerCam.SetActive(false);
        deathParticle.transform.SetParent(null, true);
        deathParticle.Play();
        gameObject.SetActive(false);
    }
}
