using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PropertyPanel : AMainPanel {
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public override void PrepareContent()
    {
        //Pega de um lugar X uma lista de todas as propriedades atuais e instacia objetos representando isso na lista. (Claro que apaga os antigos antes)
        //Essa lista de propriedades por ser algo como uma List<Propriedades>. Ai na hora de instanciar eu acesso os campos nome, nivel, producao, etc.
    }
}
