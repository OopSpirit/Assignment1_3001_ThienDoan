using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] AudioSource musicBackground;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void LoadToScene(int a)
    {
        SceneManager.LoadScene(a);
    }
}
