using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishStage : MonoBehaviour
{
    [SerializeField] private StageController stageController;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitForAnimationToEnd(col));
        }
    }
    
    IEnumerator WaitForAnimationToEnd(Collider2D col)
    {
        animator.SetBool("Finishing", true);
        PlayerInput playerInput = col.gameObject.GetComponent<PlayerInput>();
        playerInput.DeactivateInput();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        stageController.CallQuota();
    }
}
