using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransitionManagerKa : MonoBehaviour
{

    public FadeScreenKA fadeScreenka;

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }
    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreenka.FadeOut();
        yield return new WaitForSeconds(fadeScreenka.fadeDuration);

        //inicio de nueva scena
        SceneManager.LoadScene(sceneIndex);
    }
}
