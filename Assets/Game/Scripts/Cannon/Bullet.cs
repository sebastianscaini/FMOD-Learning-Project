using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Movement")]
    public Vector3 speed;

    [Header("FMOD")]
    public string bulletDestroySFXPath;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        //Create an instance of the SFX.
        FMOD.Studio.EventInstance destroySFX = FMODUnity.RuntimeManager.CreateInstance(bulletDestroySFXPath);
        //Attach the instance to this GameObject.
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(destroySFX, this.transform);
        //Play SFX
        destroySFX.start();
    }
}
