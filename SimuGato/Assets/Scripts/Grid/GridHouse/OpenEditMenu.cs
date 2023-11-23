using UnityEngine;
using UnityEngine.UI;

public class OpenEditMenu : MonoBehaviour
{
    GameObject canvas;

    private void OnMouseDown()
    {
        canvas.SetActive(true);
        
    }
}
