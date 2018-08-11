using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleWindow : MonoBehaviour {
	[Header("Animation Preferences")]
	[SerializeField] private float timeToShowAndHideShield;
	[SerializeField] private float shieldDelay;
	[SerializeField] private float timeToGrowBattleScene;
	[SerializeField] private float backgroundInitialPosition;
	[SerializeField] private float backgroundFinalPosition;
	[SerializeField] private float timeToShowPlayerBattlePoints;
	[SerializeField] private float timeToShowEnemyBattlePoints;

	[Header("UI Objects")]
	[SerializeField] private CanvasGroup shield;
	[SerializeField] private CanvasGroup background;
	[SerializeField] private RectTransform maskBackground;
	[SerializeField] private RectTransform battleScene;
	[SerializeField] private Text playerBattlePointsText;
	[SerializeField] private Text enemyBattlePointsText;
	[SerializeField] private Transform player;
	[SerializeField] private Transform enemy;
	[SerializeField] private CanvasGroup results;

	[Header("Custom")]
	[SerializeField] private List<Sprite> backgrounds;
	[SerializeField] private List<Sprite> characters;

	private int playerBattlePoints;
	private int enemyBattlePoints;

	private void Start()
	{
		//Show(3,1);
	}
	public void Show(int playerBattlePoints,int enemyBattlePoints)
	{
		GetComponent<CanvasGroup>().DOFade(1,0);
		this.playerBattlePoints = playerBattlePoints;
		this.enemyBattlePoints = enemyBattlePoints;
		StartCoroutine( ShowBattleScene() );
	}
	private IEnumerator ShowBattleScene()
	{
		InitializeAllValues();
		yield return ShowAndHideShield();
		yield return AnimateBattleSceneAndBackground();
		yield return ShowBattlePoints();
		yield return Battle();
		yield return ShowResults();
	}
	private void InitializeAllValues()
	{
		battleScene.DOSizeDelta(new Vector2(720,0),0);
		shield.transform.DOScale(0,0);
		results.DOFade(0,0);
		results.blocksRaycasts = false;
		background.DOFade(0,0);
		background.blocksRaycasts = false;
		maskBackground.DOAnchorPosX(backgroundInitialPosition,0.5f);
		playerBattlePointsText.transform.DOScale(0,0);
		enemyBattlePointsText.transform.DOScale(0,0);
		playerBattlePointsText.text = playerBattlePoints.ToString();
		enemyBattlePointsText.text = enemyBattlePoints.ToString();
	}
	private IEnumerator ShowAndHideShield()
	{
		background.DOFade(1,0.5f);
		background.blocksRaycasts = true;
		shield.transform.DOScale(1,timeToShowAndHideShield/2);
		yield return new WaitForSeconds(timeToShowAndHideShield/2 + shieldDelay);
		shield.transform.DOScale(0,timeToShowAndHideShield/2);
	}
	private IEnumerator AnimateBattleSceneAndBackground()
	{
		battleScene.DOSizeDelta(new Vector2(720,680),timeToGrowBattleScene);
		maskBackground.DOAnchorPosX(backgroundFinalPosition,1f);
		yield return new WaitForSeconds(timeToShowAndHideShield/2);
		yield return new WaitForSeconds(Mathf.Abs( (timeToShowAndHideShield/2) - timeToGrowBattleScene ));
	}
	private IEnumerator ShowBattlePoints()
	{
		playerBattlePointsText.transform.DOScale(1,timeToShowPlayerBattlePoints);
		yield return new WaitForSeconds(0.5f);
		enemyBattlePointsText.transform.DOScale(1,timeToShowPlayerBattlePoints);
	}
	private IEnumerator Battle()
	{
		yield return new WaitForSeconds(1f);
		if(PlayerIsTheWinner()){
			player.DOScale(1.1f,0.3f);
			enemy.DOScale(0.9f,0.3f);
		}else{
			enemy.DOScale(1.1f,0.3f);
			player.DOScale(0.9f,0.3f);
		}
	}
	private IEnumerator ShowResults()
	{
		results.transform.GetChild(0).GetComponent<Text>().text = PlayerIsTheWinner() ? "VENCEDOR" : "PERDEDOR";
		results.DOFade(1,0.5f);
		results.blocksRaycasts = true;
		yield return new WaitForSeconds(1f);
		GetComponent<CanvasGroup>().DOFade(0, 0.5f);
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		Destroy(this.gameObject,0.5f);
	}
	private bool PlayerIsTheWinner(){
		if(playerBattlePoints >= enemyBattlePoints){
			return true;
		}else{
			return false;
		}
	}
}
