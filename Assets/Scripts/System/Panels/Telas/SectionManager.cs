using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    [SerializeField] private List<Section> sections;
    public int currentSection = 2;
    public float mouseMinOffset = 1f;
    public float timeMin = 0.25f;

    public void Start()
    {
        PrepareAll();
        sections[currentSection].show(1,0);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(SwipeInput());
    }
    public void SelectSection(int i)
    {
        if (i - currentSection < 0)
        {
            sections[currentSection].hide(1);
            sections[currentSection].UnprepareContent();
            currentSection = i;
            sections[currentSection].show(-1);
            sections[currentSection].PrepareContent();
        }
        else if (i - currentSection > 0)
        {
            sections[currentSection].hide(-1);
            sections[currentSection].UnprepareContent();
            currentSection = i;
            sections[currentSection].show(1);
            sections[currentSection].PrepareContent();
        }
    }
    public void PrepareAll()
    {
        foreach (Section s in sections)
        {
            s.hide(1,0);
        }
    }
    private IEnumerator SwipeInput()
    {
        Vector2 initTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float t = 0;
        while (Input.GetMouseButton(0))
        {
            t += Time.deltaTime;
            yield return null;
        }
        Vector2 finalTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 delta = finalTouch - initTouch;
        if (Mathf.Abs(delta.y) < Mathf.Abs(delta.x))
        {
            if (t >= timeMin || Mathf.Abs(delta.x) < mouseMinOffset)
                yield break;
            if (delta.x < 0)
                SelectSection(Mathf.Clamp(currentSection + 1, 0, sections.Count - 1));
            else if (delta.x > 0)
                SelectSection(Mathf.Clamp(currentSection - 1, 0, sections.Count - 1));
        }
    }
}
