using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    private Vector3 offset;
    private Transform player1;
    private Transform player2;
    private Vector3 cameraPos=new Vector3(0,56.81f,-54.68f);

	// Use this for initialization
	void Start () {
        ResetCamera();
	}
	
	// Update is called once per frame
	void Update () {
        if (player1 == null || player2 == null) return;
        transform.position = offset + (player1.position + player2.position) / 2;
        float distance = Vector3.Distance(player1.position, player2.position);
        if (distance > 10 && distance < 40)
        {
            this.gameObject.GetComponent<Camera>().orthographicSize = distance * 0.5f+5;
        }
    }

    public void ResetCamera()
    {
        transform.position = cameraPos;
        this.gameObject.GetComponent<Camera>().orthographicSize = 25f;
        player1 = GameManager.Instance.tank1.transform;
        player2 = GameManager.Instance.tank2.transform;
        offset = transform.position - (player1.position + player2.position) / 2;
        float distance = Vector3.Distance(player1.position, player2.position);
        if (distance > 15 && distance < 50)
        {
            this.gameObject.GetComponent<Camera>().orthographicSize = distance * 0.8f;
        }
    }
}
