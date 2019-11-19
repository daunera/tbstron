using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);        
    }

    private void Start()
    {
        SceneManager.LoadScene(1);
    }
}
