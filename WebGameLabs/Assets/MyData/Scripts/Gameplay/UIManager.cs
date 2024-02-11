using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IObserver<PlayerEvents>
{
    [SerializeField] private Image healthbar;

    [SerializeField] private GameObject gameoverPanel;

    private PlayerMovements player;
    private HealthManager playerHealthManager;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerMovements>();
        playerHealthManager = player.GetComponent<HealthManager>();
    }

    private void OnEnable()
    {
        player.subject.AddObserver(this);
        playerHealthManager.OnHealthChange += OnPlayerHealthChanged;
        playerHealthManager.OnDie += OnDie;
    }

    private void OnDie()
    {
        gameoverPanel.SetActive(true);
    }

    private void OnPlayerHealthChanged(float health)
    {
        healthbar.fillAmount = health / playerHealthManager.MaxHealth;
    }

    private void OnDisable()
    {
        player.subject.RemoveObserver(this);
        playerHealthManager.OnHealthChange -= OnPlayerHealthChanged;
        playerHealthManager.OnDie -= OnDie;
    }

    public void OnNotify(PlayerEvents events)
    {
        switch (events)
        {
            case PlayerEvents.Alive: Debug.Log("Player is Alive"); break;

            case PlayerEvents.Dead: Debug.Log("Player is Dead"); break;
        }
    }
}
