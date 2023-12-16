using UnityEngine.SceneManagement;
using UnityEngine;

public class DeletarDepois : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("Grid");
        }
    }
}
