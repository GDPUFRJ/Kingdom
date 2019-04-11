using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SendTroopsBox : Singleton<SendTroopsBox>
{
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private Text numberOfSoldiersText;

    private CanvasGroup canvasGroup;

    private bool isActivaded;
    public bool IsActivated { get { return isActivaded; } }

    private BattleArrowController currentSourceArrow;
    private BattleArrowController currentOpositeArrow;
    private int numberOfSoldiers;

    protected SendTroopsBox() { }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        numberOfSoldiers = 0;
        currentSourceArrow = null;
        currentOpositeArrow = null;
        isActivaded = false;
    }

    public void Activate(BattleArrowController sourceArrow, BattleArrowController opositeArrow)
    {
        canvasGroup.DOFade(1, fadeDuration);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        currentSourceArrow = sourceArrow;
        currentOpositeArrow = opositeArrow;
        isActivaded = true;
        if (sourceArrow.GetSoldiersToBeTransfered() > 0)
        {
            numberOfSoldiers = sourceArrow.GetSoldiersToBeTransfered();
            UpdateText();
        }
    }

    public void ConfirmSoldiers()
    {
        // Executa as movimentações de soldados supondo que o número é válido
        switch (currentSourceArrow.GetTipo())
        {
            case ArrowType.Battle:
            case ArrowType.Arrow:
                currentSourceArrow.Source.SetSoldiers(SoldierType.ToGetOut, numberOfSoldiers);
                currentSourceArrow.SetSoldiersToBeTransfered(numberOfSoldiers);
                currentSourceArrow.UpdateArrowText();
                break;
            case ArrowType.Abort:
                currentOpositeArrow.SetSoldiersToBeTransfered(currentOpositeArrow.GetSoldiersToBeTransfered() - numberOfSoldiers);
                currentOpositeArrow.Source.RemoveSoldiers(SoldierType.ToGetOut, numberOfSoldiers);
                currentOpositeArrow.UpdateArrowText();
                break;
        }

        canvasGroup.DOFade(0, fadeDuration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        currentSourceArrow.Source.UpdateSoldierInfo();
        currentOpositeArrow.Source.UpdateSoldierInfo();
        currentSourceArrow = null;
        currentOpositeArrow = null;
        numberOfSoldiers = 0;
        UpdateText();
        isActivaded = false;
    }

    public void CancelOperation()
    {
        canvasGroup.DOFade(0, fadeDuration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        numberOfSoldiers = 0;
        UpdateText();
        currentSourceArrow = null;
        currentOpositeArrow = null;
        isActivaded = false;
    }

    public void AddSoldiers(int nSoldiers)
    {
        switch(currentSourceArrow.GetTipo())
        {
            case ArrowType.Battle:
            case ArrowType.Arrow:
                if (nSoldiers > 0)
                {
                    if (currentSourceArrow.Source.GetSoldiers(SoldierType.ToGetOut) + numberOfSoldiers + nSoldiers <=
                        currentSourceArrow.Source.GetSoldiers(SoldierType.InProperty))
                    {
                        numberOfSoldiers += nSoldiers;
                    }
                    else
                    {
                        numberOfSoldiers = currentSourceArrow.Source.GetSoldiers(SoldierType.InProperty);
                    }
                }
                else if (nSoldiers < 0)
                {
                    if (numberOfSoldiers + nSoldiers >= 0)
                    {
                        numberOfSoldiers += nSoldiers;
                    }
                    else
                    {
                        numberOfSoldiers = 0;
                    }
                }
                break;
            case ArrowType.Abort:
                if (nSoldiers > 0)
                {
                    if (currentOpositeArrow.GetSoldiersToBeTransfered() - numberOfSoldiers - nSoldiers >= 0)
                    {
                        numberOfSoldiers += nSoldiers;
                    }
                    else
                    {
                        numberOfSoldiers = currentOpositeArrow.GetSoldiersToBeTransfered();
                    }
                }
                else if (nSoldiers < 0)
                {
                    if (numberOfSoldiers + nSoldiers >= 0)
                    {
                        numberOfSoldiers += nSoldiers;
                    }
                    else
                    {
                        numberOfSoldiers = 0;
                    }
                }
                break;
        }
        UpdateText();
    }

    private void UpdateText()
    {
        numberOfSoldiersText.text = numberOfSoldiers.ToString();
    }
}
