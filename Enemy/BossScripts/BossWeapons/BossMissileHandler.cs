using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissileHandler : MonoBehaviour
{
	[Header("REFERENCES")]
	[SerializeField] private Rigidbody _rb;
	[SerializeField] public GameObject _target;
	[SerializeField] private GameObject _explosionPrefab;

	[Header("MOVEMENT")]
	[SerializeField] private float _speed = 15;
	[SerializeField] private float _rotateSpeed = 95;

	[Header("PREDICTION")]
	[SerializeField] private float _maxDistancePredict = 100;
	[SerializeField] private float _minDistancePredict = 5;
	[SerializeField] private float _maxTimePrediction = 5;
	private Vector3 _standardPrediction, _deviatedPrediction;

	[Header("DEVIATION")]
	[SerializeField] private float _deviationAmount = 50;
	[SerializeField] private float _deviationSpeed = 2;

	[Header("STATS")]
	[HideInInspector][SerializeField] public int _AOEDamage = 1;

	private void FixedUpdate()
	{
		_rb.velocity = transform.forward * _speed;

		var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, _target.transform.position));

		PredictMovement(leadTimePercentage);

		AddDeviation(leadTimePercentage);

		RotateRocket();
	}

	private void PredictMovement(float leadTimePercentage)
	{
		var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);

		_standardPrediction = _target.transform.position + (new Vector3(Mathf.Cos(Time.time * 10) * 10, Mathf.Sin(Time.time * 10) * 10)) * predictionTime;
	}

	private void AddDeviation(float leadTimePercentage)
	{
		var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);

		var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;

		_deviatedPrediction = _standardPrediction + predictionOffset;
	}

	private void RotateRocket()
	{
		var heading = _deviatedPrediction - transform.position;

		var rotation = Quaternion.LookRotation(heading);
		_rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (_explosionPrefab) 
		{
			GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
			explosion.GetComponent<FireBallExplosionHandler>().explosionDamage = _AOEDamage;
		}

		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, _standardPrediction);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);
	}
}
