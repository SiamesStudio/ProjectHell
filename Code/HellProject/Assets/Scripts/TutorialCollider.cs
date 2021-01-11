using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    [SerializeField] TutorialHolder tutorialHolder;
    [SerializeField] private Collider boxCollider;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        TutorialStep step = tutorialHolder.steps.Find((x) => x.trigger == boxCollider);
        if (step == null) return;
        tutorialHolder.DisplayText(step);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        TutorialStep step = tutorialHolder.steps.Find((x) => x.trigger == boxCollider);
        if (step == null) return;
        tutorialHolder.HideText(step);
    }
}
