using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Timeline : MonoBehaviour
{

    private PlayableDirector director;
    public GameObject controlPanel;
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    
    

    private void Director_Played(PlayableDirector obj)
    {
        controlPanel.SetActive(false);
    }

    private void Director_Stopped(PlayableDirector obj)
    {
        controlPanel.SetActive(true);
    }

    public void StartTimeLine()
    {
        director.Play();
    }
    
}
