using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    #region Methods

    public void OpenSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    #endregion Methods

}
