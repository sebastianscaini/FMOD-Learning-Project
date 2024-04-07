using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [Header("Goal")]
    public float timeToWaitBeforeReload;

    [Header("FMOD")]
    public string winSFXPath;

    private bool over = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!over)
            {
                over = true;
                StartCoroutine(EndSequence());
            }
        }
    }

    private IEnumerator EndSequence()
    {
        //Create an instance of the SFX.
        FMOD.Studio.EventInstance winSFX = FMODUnity.RuntimeManager.CreateInstance(winSFXPath);
        //Attach the instance to this GameObject.
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(winSFX, this.transform);
        //Play SFX
        winSFX.start();

        yield return new WaitForSeconds(timeToWaitBeforeReload);

        SceneManager.LoadScene(0);
    }
}
