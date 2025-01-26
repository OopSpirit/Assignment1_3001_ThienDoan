using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource musicBackground;
    void Start()
    {
        DontDestroyOnLoad(musicBackground);
        musicBackground.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
