using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserDraw : MonoBehaviour {

	public Transform startPoint;
	public Transform endPoint;
	public GameObject particleBeam;
	public GameObject bottomDyson;
	public GameObject topDyson;

	public float forca = 10;
	public float countDown = 3;
	LineRenderer laserLine;
	private bool unlocked = false;

	public Text countdownText = null;

	// Use this for initialization
	void Start () {
		laserLine = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		laserLine.SetPosition (0, startPoint.position);
		laserLine.SetPosition (1, endPoint.position);
		particleBeam.transform.LookAt(endPoint.transform);
		Rigidbody rb = endPoint.GetComponent<Rigidbody>();
		
		if (unlocked) {
			countdownText.text = "YOU WIN";
			bottomDyson.transform.Rotate(0, 0, 2);
			topDyson.transform.Rotate(0, 0, 2);
			bottomDyson.transform.Translate(-Vector3.forward*1*Time.deltaTime);
			topDyson.transform.Translate(-Vector3.forward*1*Time.deltaTime);
			rb.AddForce(-rb.transform.up * (forca/50), ForceMode.Impulse);
			if (laserLine.startWidth > 0.0 ) {
				laserLine.startWidth -= 0.02f;
				laserLine.endWidth -= 0.02f;
			}
		} else {
			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				if (Physics.Raycast(ray, out hit, 100.0f))
				
				{
					
					if (hit.transform != null)
					{
						LaunchIntoAir(rb);
						

						if (laserLine.GetPosition(1).y > 2) {
							
							if (laserLine.endWidth < 0.9
							) {
								laserLine.startWidth += 0.2f;
								laserLine.endWidth += 0.2f;
							}

						}
					}
				}
			}
			if (endPoint.transform.position.y > 0.05 ) {
				rb.AddForce(-rb.transform.up * (forca/100), ForceMode.Impulse);
				if (laserLine.startWidth > 0.0 ) {
					laserLine.startWidth -= 0.01f;
					laserLine.endWidth -= 0.01f;
				}
			}

			if (laserLine.startWidth > 0.80) {
				countDown -= Time.deltaTime;
				countdownText.text = countDown.ToString("#");
			} else {
				countDown = 3;
				countdownText.text = "";
			}

			

			if (countDown <= 0) {
				unlocked = true;
				
			}
		}
		
	}

    private void LaunchIntoAir(Rigidbody rig)
    {
        rig.AddForce(rig.transform.up * forca, ForceMode.Impulse);
    }
}
