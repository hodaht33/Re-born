using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartClassroom : MonoBehaviour
{
    public void onClick()
    {
        SceneManager.LoadScene("classroom");
    }
}
