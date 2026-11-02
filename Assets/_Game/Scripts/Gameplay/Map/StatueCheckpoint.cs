using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class StatueCheckpoint : MonoBehaviour
{
    [SerializeField] private InputActionReference interactActionReference;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Collider2D triggerCollider;
    [SerializeField] private StatueCheckpointUIAnim anim;

    private bool isPlayerInRange = false;
    private bool isActivated = false;

    private void OnEnable()
    {
        RestartStatueState();

        if (interactActionReference != null && interactActionReference.action != null)
        {
            interactActionReference.action.performed += OnInteractPerformed;
            interactActionReference.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (interactActionReference != null && interactActionReference.action != null)
        {
            interactActionReference.action.performed -= OnInteractPerformed;
        }
    }

    private void RestartStatueState()
    {
        isActivated = false;
        isPlayerInRange = false;

        if (triggerCollider != null)
        {
            triggerCollider.enabled = true;
        }
        if (anim != null) anim.Hide();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (isPlayerInRange)
        {
            ActivateStatue();
        }
    }

    private void ActivateStatue()
    {
        isActivated = true;
        if (anim != null) anim.Hide();

        MainCharacterController.Ins.SetLatestSpawnPoint(spawnPoint);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isPlayerInRange = true;
        if (anim != null) anim.Show();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPlayerInRange = false;
        if (anim != null) anim.Hide();
    }
}