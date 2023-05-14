using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] musics = new AudioClip[2];
    
    private AudioSource musicSource;
    private bool stageMusicPlayed;
    private bool titleMusicPlayed;
    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = musics[0];
        musicSource.loop = true;
        musicSource.volume = 0.70f;
        titleMusicPlayed = true;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex > 2 && !stageMusicPlayed)
        {
            if(musicSource.isPlaying)
            {
                musicSource.Stop();
                titleMusicPlayed = false;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                musicSource.clip = musics[1];
                musicSource.Play();
                stageMusicPlayed = true;
                
            }
        }
        else if(SceneManager.GetActiveScene().buildIndex <= 2 && !titleMusicPlayed)
        {
            musicSource.clip = musics[0];
            titleMusicPlayed = true;
            stageMusicPlayed = false;
        }
    }
}
