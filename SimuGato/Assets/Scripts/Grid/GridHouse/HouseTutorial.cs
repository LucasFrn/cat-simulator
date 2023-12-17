using UnityEngine;
using UnityEngine.UI;

public class HouseTutorial : MonoBehaviour
{
    [SerializeField] private string[] explaning;
    [SerializeField] private Text tutorialText;

    int index = 0;

    void Start()
    {
        tutorialText.text = explaning[index];
    }


    public void NextStep()
    {
        index++;
        if (explaning.Length <= index)
        {
            
            tutorialText.enabled = false;
            this.enabled = false;
        }
        else
        {
            tutorialText.text = explaning[index];
        }
        
    }
}
