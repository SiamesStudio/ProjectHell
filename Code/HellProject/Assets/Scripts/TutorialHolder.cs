using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHolder : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] Text screenText;
    [SerializeField] string[] strings;
    [SerializeField] List<Collider> triggers = new List<Collider>();
    public List<TutorialStep> steps = new List<TutorialStep>();

    private void Start()
    {
        
        image.gameObject.SetActive(false);
        for(int i=0; i< strings.Length; i++)
        {
            steps.Add(new TutorialStep(strings[i], triggers[i]));
        }
    }

    public void DisplayText(TutorialStep step)
    {
        screenText.text = step.text;
        image.gameObject.SetActive(true);
        screenText.gameObject.SetActive(true);
    }

    public void HideText(TutorialStep step)
    {
        image.gameObject.SetActive(false);
        screenText.gameObject.SetActive(false);
    }

    
}

public class TutorialStep
{
    public string text;
    public Collider trigger;

    public TutorialStep(string _text, Collider _trigger)
    {
        text = _text; trigger = _trigger;
    }
}
