using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using static System.TimeZoneInfo;
using UnityEngine.Device;
using Michsky.LSS;


public class CameraTargetController : MonoBehaviour
{
	public enum targetState
	{
		menuHold,
		menuStart,
		playingGame
	}
	[Header("General Variables")]
	public targetState currentTargetState;
	public CinemachineVirtualCamera virtualCamera;

	[Header("Menu Variables")]
	public Transform cameraFollowTargetPos1;
	public Transform cameraLookTargetPos1;
	public int rotationSpeed;

	[Header("Cutscene Variables")]
	public Transform cameraFollowTargetPos2;
	public Transform cameraLookTargetPos2;
	public float timeRot;
	public float timeUp;

	[Header("Playing Variables")]
	public Transform cameraFollowTargetPos3;
	public Transform cameraLookTargetPos3;

	[Header("Camera Fade UI")]
	public CanvasGroup uiObject;
	public CanvasGroup uiFadeObject;
	public float uiFadeTime;

	[Header("Load Screen info")]
	[SerializeField]
	private LSS_Manager lsmManager;



	// Start is called before the first frame update
	void Start()
	{
		uiFadeObject.alpha = 0f;
		UpdateCameraPos();
	}

	private void Update()
	{
		switch (currentTargetState)
		{
			case targetState.menuHold:
				cameraFollowTargetPos1.transform.Rotate(0, (1 * Time.deltaTime * rotationSpeed), 0);
				break;
		}
	}

	[ContextMenu("StartCutscene")]
	public void startGame() 
	{
		StartCoroutine(menuStartCutscene());
	}

	IEnumerator menuStartCutscene()
	{
		DOTween.To(() => uiObject.GetComponent<CanvasGroup>().alpha, x => uiObject.GetComponent<CanvasGroup>().alpha = x, 0, 1f);
		cameraFollowTargetPos1.transform.DORotate(new Vector3(0,-135,0), timeRot).OnComplete(() => {
			currentTargetState = targetState.menuStart;
			UpdateCameraPos();
			CutscenePlayerHandler.instance.testPlayerCutsceneMover();
		});
		yield return new WaitForSeconds(10f);
		cameraFollowTargetPos2.transform.DOMove(cameraFollowTargetPos3.transform.position, timeUp).OnComplete(() => {
			currentTargetState = targetState.playingGame;
			UpdateCameraPos();
		});
		DOTween.To(() => uiFadeObject.GetComponent<CanvasGroup>().alpha, x => uiFadeObject.GetComponent<CanvasGroup>().alpha = x, 1, uiFadeTime).OnComplete(() => {
			lsmManager.LoadScene("Level1");
		});
	}

	public void UpdateCameraPos()
	{
		switch (currentTargetState)
		{
			case targetState.menuHold:
				virtualCamera.Follow = cameraFollowTargetPos1.transform;
				virtualCamera.LookAt = cameraLookTargetPos1.transform;
				break;
			case targetState.menuStart:
				virtualCamera.Follow = cameraFollowTargetPos2.transform;
				virtualCamera.LookAt = cameraLookTargetPos2.transform;
				break;
			case targetState.playingGame:
				virtualCamera.Follow = cameraFollowTargetPos3.transform;
				virtualCamera.LookAt = cameraLookTargetPos3.transform;
				break;
		}
	}
}
