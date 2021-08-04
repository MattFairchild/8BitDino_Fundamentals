using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    private AsyncOperation m_async_loading;

    /// <summary>
    /// load level with a given name synchronously
    /// </summary>
    /// <param name="n_level_name"></param>
    public void load_level(string n_level_name)
    {
        SceneManager.LoadScene(n_level_name);
    }

    /// <summary>
    /// load level with the given scene index synchronously
    /// </summary>
    /// <param name="n_level_name"></param>
    public void load_level(int n_level_index)
    {
        SceneManager.LoadScene(n_level_index);
    }

    /// <summary>
    /// load level with a given name asynchronously
    /// </summary>
    /// <param name="n_level_name"></param>
    public void load_level_async(string n_level_name)
    {
        m_async_loading = SceneManager.LoadSceneAsync(n_level_name);
        m_async_loading.allowSceneActivation = false;
    }

    private IEnumerator async_loading_coroutine()
    {
        while (m_async_loading.progress < 1.0f)
        {
            yield return null;
        }
    }
}
