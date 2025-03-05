using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReWatch : MonoBehaviour
{
    
    public void RePlay_Next()
    {
        
        Replay.Instance.Next();
        
        
    }

    public void RePlay_Last()
    {
        
        Replay.Instance.Last();
        
        
    }



}
