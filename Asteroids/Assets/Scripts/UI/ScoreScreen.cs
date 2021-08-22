using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameController;
public class ScoreScreen : MonoBehaviour
{
	public event System.Action EventComplete;

	[SerializeField]
	private TextMeshProUGUI scoreText;

	[SerializeField]
	private TextMeshProUGUI waveText;

	private bool isComplete;

	void OnEnable()
	{
		isComplete = false;
		scoreText.text = GameManager.Instance.Points.ToString();
		waveText.text = GameManager.Instance.Level.ToString();

		GameManager.Instance.ResetGame();
	}
	void Update()
	{
		if (!isComplete)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				StartPressed();
			}
		}
	}
	private void StartPressed()
	{
		isComplete = true;
		if (EventComplete != null)
		{
			EventComplete();
		}
	}
}
