using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image healthbar;

    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject pausePanel;

    private PlayerMovements player;
    private HealthManager playerHealthManager;

    private PlayerInputActions inputActions;
    private Coroutine healthBarTween;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        player = FindAnyObjectByType<PlayerMovements>();
        playerHealthManager = player.GetComponent<HealthManager>();

        gameoverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();

        playerHealthManager.OnHealthChange += OnPlayerHealthChanged;
        playerHealthManager.OnDie += OnDie;

        inputActions.UI.Pause.performed += Pause_performed;
    }

    private void OnDisable()
    {
        inputActions?.UI.Disable();

        playerHealthManager.OnHealthChange -= OnPlayerHealthChanged;
        playerHealthManager.OnDie -= OnDie;

        inputActions.UI.Pause.performed -= Pause_performed;
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        bool isPaused = !pausePanel.activeInHierarchy;
        pausePanel.SetActive(isPaused);
        Cursor.lockState =  isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    private void OnDie()
    {
        inputActions.UI.Pause.performed -= Pause_performed;
        StartCoroutine(PlayDeathAnims());
    }

    private IEnumerator PlayDeathAnims()
    {
        yield return new WaitForSeconds(1);
        pausePanel.SetActive(false);
        gameoverPanel.SetActive(true);
    }

    private void OnPlayerHealthChanged(float health)
    {
        if (healthBarTween != null)
            StopCoroutine(healthBarTween);
        healthBarTween = StartCoroutine(TweenHealthbar(health));
    }

    private IEnumerator TweenHealthbar(float health)
    {
        float target = health / playerHealthManager.MaxHealth;
        while(healthbar.fillAmount != target)
        {
            healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount, target, Time.deltaTime * 2);
            yield return null;
        }    
    }

    public void OnResumeBtn()
    {
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnRestartBtn()
    {
        GameManager.LoadScene(Scenes.Gameplay);
    }

    public void OnMainmenuBtn()
    {
        GameManager.LoadScene(Scenes.Mainmenu);
    }    
}
