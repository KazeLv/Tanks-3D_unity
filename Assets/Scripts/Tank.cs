using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tank : MonoBehaviour {

    public float speed=3f;
    public float shellSpeed=15;
    public int playerTag = 1;
    public int hp=100;
    public GameObject shellPrefab;
    public GameObject tankExplosion;
    public KeyCode fireKey = KeyCode.Space;
    public AudioClip fire;
    public AudioClip explosion;
    public AudioClip idle;
    public AudioClip drive;
    public Slider slider;

    private Vector3 targetPos;
    private Rigidbody rigidbody;
    private Transform firePos;
    private AudioSource audio;
    private int winner;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        firePos = transform.Find("FirePosition");
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float h = Input.GetAxis("HorizontalPlayer" + playerTag);
        float v = Input.GetAxis("VerticalPlayer" + playerTag);
        if (GameManager.Instance.isEnd == false)
        {
            slider.value = hp;
            targetPos = transform.position + new Vector3(h, 0, v) * speed * Time.deltaTime;
            transform.LookAt(targetPos);
            if (Input.GetKeyDown(fireKey))
            {
                AudioManager.Instance.Play(fire);
                GameObject temp = GameObject.Instantiate(shellPrefab, firePos.position, firePos.rotation) as GameObject;
                temp.GetComponent<Rigidbody>().velocity = temp.transform.forward * shellSpeed;
            }
            else
            {
                rigidbody.MovePosition(targetPos);
            }
        }
        
        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            audio.clip = drive;
            audio.volume = 0.1f;
            if (audio.isPlaying == false)
            {
                audio.Play();
            }
        }else
        {
            audio.clip = idle;
            audio.volume = 0.1f;
            if (audio.isPlaying == false)
            {
                audio.Play();
            }
        }
	}

    void GetHit()
    {
        if (hp <= 0) return;
        hp -= Random.Range(10, 20);
        if (hp <= 0)
        {
            Die();
            if (playerTag == 1)
            {
                winner = 2;
            }else
            {
                winner = 1;
            }
            GameManager.Instance.GameEnd("Tank"+winner);
        }
    }

    void Die()
    {
        GameObject.Instantiate(tankExplosion, transform.position + Vector3.up, Quaternion.identity);
        AudioManager.Instance.Play(explosion);
        Destroy(this.gameObject);
    }

}
