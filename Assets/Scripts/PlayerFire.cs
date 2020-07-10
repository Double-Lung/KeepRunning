using System;
using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject arm;
    public float skillDuration;
    public float skillCooldown;
    public float accuracy;
    private bool isFiring;
    private bool hit;
    private float nextFireTime;
    private PlayerInput playerInput;
    private PlayerHealth playerHealth;
    private Coroutine fireRoutine;
    private Coroutine manaRoutine;

    public event Action onFire;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerHealth = GetComponent<PlayerHealth>();
        playerInput.onFire += Fire;
        playerHealth.OnDie += StopFire;
        playerInput.onPause += OnPause;
        isFiring = false;
    }

    private void Start() {
        UIManager.instance.initializeMana(skillCooldown);
    }

    void Fire() {
        if (!isFiring && Time.time> nextFireTime) {
            hit = false;
            accuracy = Mathf.Clamp(accuracy,0,1);
            if (accuracy > UnityEngine.Random.value) {
                hit = true;
                onFire?.Invoke();
            }
            nextFireTime = Time.time + skillCooldown;
            fireRoutine = StartCoroutine(SkillRoutine());
            manaRoutine = StartCoroutine(RecoveryRoutine());
        }
    }
    void StopFire() {
        playerInput.onFire -= Fire;
        if (fireRoutine != null) {
            StopCoroutine(fireRoutine);
            AudioManager.instance.Stop("orgafire");
            AudioManager.instance.Stop("accurate");
            arm.SetActive(false);
            playerHealth.OnDie -= StopFire;
        }
        if (manaRoutine != null) {
            StopCoroutine(manaRoutine);
        }
    }
    IEnumerator SkillRoutine() {
        arm.SetActive(true);
        AudioManager.instance.Play("orgafire");
        // effects for car
        yield return new WaitForSeconds(skillDuration);
        arm.SetActive(false);
        if (hit) {
            AudioManager.instance.Play("accurate");
        }
    }

    IEnumerator RecoveryRoutine() {
        float mana = 0;
        UIManager.instance.updateMana(mana);
        while (mana < skillCooldown) {
            mana += Time.deltaTime;
            UIManager.instance.updateMana(mana);
            yield return null;
        }
        
    }
    void OnPause() {
        playerInput.onFire -= Fire;
        StopFire();
        playerInput.onPause -= OnPause;
    }
}
