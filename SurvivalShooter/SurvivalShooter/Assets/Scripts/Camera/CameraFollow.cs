using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float smoothing = 5f;
	public bool shake = false;
	private Vector3 offset;

	void Start()
	{
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		if (!shake)
		{
			Vector3 targetCamPos = target.position + offset;
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
		}
		if (shake)
        {
			Timer myTimer1 = new Timer(.1f, StopShake);
			TimeManager.instance.timers.Add(myTimer1);

			float x = Random.Range(-3, 3);
			float y = Random.Range(-3, 3);
			Debug.Log("Shaking Camera");
			Vector3 targetCamPos = target.position + offset;
			targetCamPos.x += x;
			targetCamPos.y += y;
			transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
	}

	void StopShake()
    {
		shake = false;
	}
}
