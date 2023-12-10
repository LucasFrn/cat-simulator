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
        if (explaning.Length >= index)
        {
            index++;

            tutorialText.text = explaning[index];
        }
        else
        {
            this.enabled = false;
        }
        
    }
}
