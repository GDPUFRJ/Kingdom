using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingPanel : MonoBehaviour {
    private const float MIN_LOADING_TIME = 1f;
    [SerializeField] private CanvasGroup group;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        group.blocksRaycasts = false;
        group.DOFade(0, 0);
    }
    public void Open(string sceneName)
    {
        group.blocksRaycasts = true;
        group.DOFade(1, 0.5f);
        StartCoroutine(LoadNewScene(sceneName));
    }
    public void Hide()
    {
        group.blocksRaycasts = false;
        group.DOFade(0, 0.5f);
        Destroy(this.gameObject, 0.5f);
    }
    private IEnumerator LoadNewScene(string name)
    {
        yield return new WaitForSeconds(MIN_LOADING_TIME);
        SceneManager.LoadSceneAsync(name);
    }
}
