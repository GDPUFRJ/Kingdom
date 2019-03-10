using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KHappening : ScriptableObject {

    public string PortugueseName = "Novo Acontecimento";
    public string EnglishName = "New Happenning";
    public string PortugueseDescription = "Descreva-o aqui";
    public string EnglishDescription = "Describe it here";
    public string PortugueseQuestion = "Faça uma pergunta";
    public string EnglishQuestion = "Ask a question";
    
    public Chance chance = Chance.Normal;
    public List<KAnswer> Answers = new List<KAnswer>();

    public bool showInInspector = true;
    public bool showAnswers = true;
    public bool showDescription = true;
}
