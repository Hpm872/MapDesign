using Unity.Cinemachine;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Singleton: solo existe UN LevelManager
    public static LevelManager Instance;

    [Header("Configuracion")]
    public LevelConfig startingLevel;
    public CinemachineCamera cinemachineCamera;
    private CinemachineConfiner2D confiner;
    private LevelConfig currentConfig;
    private GameObject currentLevelInstance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        confiner = cinemachineCamera.GetComponent<CinemachineConfiner2D>();
        LoadLevel(startingLevel);
    }

    public void LoadLevel(LevelConfig config)
    {
        if (config == null)
        {
            Debug.Log("No hay mas niveles.");
            return;
        }

        // 1. Destruir el nivel anterior
        if (currentLevelInstance != null)
            Destroy(currentLevelInstance);

        // 2. Instanciar el nuevo nivel
        currentConfig = config;
        currentLevelInstance = Instantiate(config.tilemapPrefab);

        // 3. Mover al jugador al spawn point
        var player = GameObject.FindWithTag("Player");
        var spawnPoint = GameObject.FindWithTag("PlayerSpawn");
        
        if (player != null && spawnPoint != null)
            player.transform.position = spawnPoint.transform.position;

        // 4. Cambiar el collider2D de la Cinemachine Camera
        Collider2D cameraConfiner = GameObject.FindWithTag("CameraConfiner").GetComponent<PolygonCollider2D>();
        if (confiner != null && cameraConfiner != null)
        {
            confiner.BoundingShape2D = cameraConfiner;
            confiner.InvalidateBoundingShapeCache();
        }

        Debug.Log("Nivel cargado: " + config.levelName);
    }
    public void GoToNextLevel()
    {
        LoadLevel(currentConfig.nextLevel);
    }
}