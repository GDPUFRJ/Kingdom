using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleWindow : MonoBehaviour {
	[Header("Animation Preferences")]
	[SerializeField] private float backgroundInitialPosition;
	[SerializeField] private float backgroundFinalPosition;
	[SerializeField] private float timeToShowPlayerBattlePoints;
	[SerializeField] private float timeToShowEnemyBattlePoints;

	[Header("UI Objects")]
    [SerializeField] private Image sword1;
    [SerializeField] private Image sword2;
    [SerializeField] private Animator swordsCrossingAnimator;
	[SerializeField] private Image background;
    [SerializeField] private List<Image> scoreFlags;
	[SerializeField] private Text playerBattlePointsText;
	[SerializeField] private Text enemyBattlePointsText;
	[SerializeField] private Transform player;
	[SerializeField] private Transform enemy;
	[SerializeField] private CanvasGroup results;

    [Header("Flags")]
    [SerializeField] private Sprite redFlag;
    [SerializeField] private Sprite blueFlag;
    [SerializeField] private Sprite purpleFlag;
    [SerializeField] private Sprite greenFlag;
    [SerializeField] private Sprite orangeFlag;

	[Header("Custom")]
	[SerializeField] private List<Sprite> backgrounds;
	[SerializeField] private List<Sprite> characters;

	private int attackerSoldiers;
	private int defenderSoldiers;
    private int attackerBattlePoints;
    private int defenderBattlePoints;
    private int remainingAttackerSoldiers;
    private int remainingDefenderSoldiers;

    public Coroutine currentBattle;

	private void Start()
	{
		//Show(3,1);
	}
	public void Show(int attackerSoldiers, int defenderSoldiers, int attackerBattlePoints, int defenderBattlePoints, BattleInformation battleInformation,
                     int remainingAttackerSoldiers, int remainingDefenderSoldiers)
	{
		GetComponent<CanvasGroup>().DOFade(1,0);
		this.attackerSoldiers = attackerSoldiers;
		this.defenderSoldiers = defenderSoldiers;
        this.attackerBattlePoints = attackerBattlePoints;
        this.defenderBattlePoints = defenderBattlePoints;
        this.remainingAttackerSoldiers = remainingAttackerSoldiers;
        this.remainingDefenderSoldiers = remainingDefenderSoldiers;
        SetUpFlags(battleInformation);
		currentBattle = StartCoroutine( ShowBattleScene() );
	}
    private void SetUpFlags(BattleInformation battleInformation)
    {
        switch (battleInformation.attackingKingdom)
        {
            case Kingdom.Blue:   scoreFlags[0].sprite = blueFlag;   break;
            case Kingdom.Green:  scoreFlags[0].sprite = greenFlag;  break;
            case Kingdom.Orange: scoreFlags[0].sprite = orangeFlag; break;
            case Kingdom.Purple: scoreFlags[0].sprite = purpleFlag; break;
            case Kingdom.Red:    scoreFlags[0].sprite = redFlag;    break;
        }
        switch (battleInformation.defendingKingdom)
        {
            case Kingdom.Blue:   scoreFlags[1].sprite = blueFlag;   break;
            case Kingdom.Green:  scoreFlags[1].sprite = greenFlag;  break;
            case Kingdom.Orange: scoreFlags[1].sprite = orangeFlag; break;
            case Kingdom.Purple: scoreFlags[1].sprite = purpleFlag; break;
            case Kingdom.Red:    scoreFlags[1].sprite = redFlag;    break;
        }
    }
	private IEnumerator ShowBattleScene()
	{
		InitializeAllValues();
        yield return ShowSwordsCrossing();
        yield return ShowNumberOfSoldiers();
        //yield return ShowBattlePoints();
        yield return Battle();
        yield return ShowUpdatedBattlePoints();
        yield return ShowResult();
    }
    private void InitializeAllValues()
	{
        sword1.DOColor(Color.white, 0);
        sword2.DOColor(Color.white, 0);
        sword1.DOFade(0, 0);
        sword2.DOFade(0, 0);
		playerBattlePointsText.transform.DOScale(0,0);
		enemyBattlePointsText.transform.DOScale(0,0);
		
        foreach(Image im in scoreFlags)
        {
            im.DOFade(0, 0);
        }
    }
	private IEnumerator ShowNumberOfSoldiers()
	{
        playerBattlePointsText.text = attackerSoldiers.ToString();
        enemyBattlePointsText.text = defenderSoldiers.ToString();

        foreach (Image im in scoreFlags)
        {
            im.DOFade(1, 0.3f);
        }
        enemy.DOScale(1f, 0.3f);
        player.DOScale(1f, 0.3f);
        yield return new WaitForSeconds(0.5f);
        playerBattlePointsText.transform.DOScale(1,timeToShowPlayerBattlePoints);
		yield return new WaitForSeconds(0.5f);
		enemyBattlePointsText.transform.DOScale(1,timeToShowPlayerBattlePoints);
        yield return new WaitForSeconds(0.5f);
	}
    private IEnumerator ShowBattlePoints()
    {
        playerBattlePointsText.text = attackerBattlePoints.ToString();
        enemyBattlePointsText.text = defenderBattlePoints.ToString();

        playerBattlePointsText.transform.DOPunchScale(playerBattlePointsText.transform.localScale, 0.5f, 5, 0);
        enemyBattlePointsText.transform.DOPunchScale(enemyBattlePointsText.transform.localScale, 0.5f, 5, 0);
        yield return new WaitForSeconds(0.5f);
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
	}
    private IEnumerator ShowSwordsCrossing()
    {
        sword1.DOFade(1, 0.5f);
        sword2.DOFade(1, 0.5f);
        swordsCrossingAnimator.Play("SwordsCrossing_show");
        yield return new WaitForSeconds(0.5f);
    }
    private IEnumerator ShowUpdatedBattlePoints()
    {
        //if (attackerBattlePoints > defenderBattlePoints)
        //{
        //    attackerBattlePoints = attackerBattlePoints - defenderBattlePoints;
        //    defenderBattlePoints = 0;
        //}
        //else
        //{
        //    defenderBattlePoints = defenderBattlePoints - attackerBattlePoints;
        //    attackerBattlePoints = 0;
        //}
        playerBattlePointsText.text = remainingAttackerSoldiers.ToString();
        enemyBattlePointsText.text = remainingDefenderSoldiers.ToString();
        yield return new WaitForSeconds(0.5f);
    }
    private IEnumerator ShowResult()
    {
        if (PlayerIsTheWinner()) swordsCrossingAnimator.Play("SwordsCrossing_win");
        else swordsCrossingAnimator.Play("SwordsCrossing_lose");
        yield return new WaitForSeconds(1f);
        GetComponent<CanvasGroup>().DOFade(0, 0.5f);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        yield return new WaitForSeconds(0.5f);
        swordsCrossingAnimator.Play("Default");
    }
    private bool PlayerIsTheWinner(){
		if(remainingAttackerSoldiers > 0)
        {
			return true;
		}else{
			return false;
		}
	}
}
