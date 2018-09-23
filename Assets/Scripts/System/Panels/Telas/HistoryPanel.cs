using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryPanel : Section {
    [SerializeField] private GameObject dateBoxPrefab;
    [SerializeField] private GameObject txtBoxPrefab;

    [SerializeField] private Transform root;

    [SerializeField] private List<SOMoment> moments;

    public override void PrepareContent()
    {
        foreach(SOMoment m in moments)
        {
            string text = "Erro";
            if (m.type == MomentType.EVENT)
                text = "Evento";
            else if (m.type == MomentType.HAPPENING)
                text = "Acontecimento";
            else if (m.type == MomentType.BATTLE)
                text = "Batalha";
            var go = Instantiate(txtBoxPrefab, root);
            go.GetComponent<TextBox>().SetInformation(text);
        }
    }
}
