using DG.DemiLib;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MiniBossRoomManager : MonoBehaviour
{

    [SerializeField]
    private GateHandler entrance;
	[SerializeField]
	private ManagerActions actions;

	private HudInfoHandler hudInfo;

	//Phase Change References
	public List<ParticleSystem> fireEffects;
	public List<Collider> collidersToActivate;
	public List<ParticleSystem> miscParticleEffects;
	public Transform runeCircle;
	public ParticleSystem phase2Embers;
	public Material bookMat;
	public Color startColor;
	public Color burntColor;
	public PostProcessVolume postProcVol;
	//Clean Up References

	public Transform bossAttackClones;
	public GameObject cleanUpEffects;
	public GameObject Desk;

	public bool miniBossDead = false;
	private bool miniBossFightStarted = false;

	// Start is called before the first frame update
	void Start()
    {
		hudInfo = GameObject.FindGameObjectsWithTag("HUD")[0].GetComponent<HudInfoHandler>();
		bookMat.color = startColor;
	}

	public void StartFight() 
	{
		if (miniBossFightStarted) 
		{
			return;
		}
		miniBossFightStarted = true;
		hudInfo.ShowBossHealthBar();
		actions.ActivateMiniBoss();
		Desk.SetActive(false);
	}

	public IEnumerator StartSecondPhase()
	{
		runeCircle.DOScale(10,1f);
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < miscParticleEffects.Count; i++)
		{
			miscParticleEffects[i].Play();
		}
		for (int i = 0; i < fireEffects.Count; i++) 
		{
			fireEffects[i].Play();
		}
		for (int i = 0; i < collidersToActivate.Count; i++) 
		{
			collidersToActivate[i].enabled = true;
		}
		yield return new WaitForSeconds(0.5f);
		phase2Embers.Play();
		bookMat.DOColor(burntColor, 1);
		yield return new WaitForSeconds(0.5f);
		DOTween.To(() => postProcVol.weight, x => postProcVol.weight = x, 1, 0.75f);
	}

	public void MiniBossSlayed()
	{
		if (miniBossDead) 
		{
			return;
		}

		GameObject.FindGameObjectWithTag("DeathManager").GetComponent<PlayerProgressManager>().IncreaseStat(PlayerProgressManager.StatTypes.miniBoss, 1);

		miniBossDead = true;
		DOTween.To(() => postProcVol.weight, x => postProcVol.weight = x, 0, 0.75f);
		CleanUpBossAttacks();
		hudInfo.HideBossHealthBar();
		entrance.AllowGatePassage();
	}

	public void CleanUpBossAttacks() 
	{
		runeCircle.DOScale(0, 1f);
		cleanUpEffects.SetActive(true);
		phase2Embers.Stop();
		for (int i = 0; i < fireEffects.Count; i++)
		{
			StartCoroutine(fireEffects[i].GetComponentInParent<FireDamageAOEDespawner>().ForceDespawn());
		}
		foreach (Transform child in bossAttackClones) 
		{
			StartCoroutine(child.GetComponent<FireDamageAOEDespawner>().ForceDespawn());
		}
		for (int i = 0; i < miscParticleEffects.Count; i++)
		{
			miscParticleEffects[i].Stop();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && !miniBossDead && !miniBossFightStarted) 
		{
			StartFight();
		}
	}
}


