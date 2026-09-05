using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NextLevelButton : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        button.onClick.AddListener(GoToNextLevel);
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(GoToNextLevel);
    }

    void GoToNextLevel()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.GoToNextLevel();
        }
        else
        {
            Debug.LogWarning("No hay LevelManager");    
        }
    }
}
