using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    [Tooltip("The number of seconds between each shot.")]
    public float rateOfFire;
    public Transform bulletSpawn;

    [Header("FMOD")]
    public string shootSFXPath;

    private void Start()
    {
        StartCoroutine(FireBullet());
    }

    private IEnumerator FireBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(rateOfFire);

            GameObject instBullet = Instantiate(bulletPrefab);
            instBullet.transform.position = bulletSpawn.position;

            //Create an instance of the SFX.
            FMOD.Studio.EventInstance shootSFX = FMODUnity.RuntimeManager.CreateInstance(shootSFXPath);
            //Attach the instance to this GameObject.
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(shootSFX, this.transform);
            //Play SFX
            shootSFX.start();
        }
    }
}
