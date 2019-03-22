using UnityEngine;
using System.Collections;

public class Explore : MonoBehaviour {

    public GameObject shellExplosion;
    public AudioClip shellExplosionAudio;


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Tank")
        {
            collider.SendMessage("GetHit");
        }
        AudioManager.Instance.Play(shellExplosionAudio);
        GameObject.Instantiate(shellExplosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}
