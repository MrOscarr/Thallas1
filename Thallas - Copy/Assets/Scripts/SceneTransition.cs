using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator fadeAnimator;
    public string nextScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Next"))
        {
            StartCoroutine(LoadNextScene());
        }   
    }

    private IEnumerator LoadNextScene()
    {
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextScene);
        
    }
}
    