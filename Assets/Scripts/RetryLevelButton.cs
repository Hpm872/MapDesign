using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RetryLevelButton : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        button.onClick.AddListener(Retry);
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(Retry);
    }

    void Retry()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.RetryCurrentLevel();
        }
        else
        {
            Debug.LogWarning("No hay LevelManager");    
        }
    }
}
