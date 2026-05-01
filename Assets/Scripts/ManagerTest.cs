using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class ManagerTest : MonoBehaviour
{
    public static ManagerTest Instance;

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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        Checkpoint.hasCheckpoint = false;
        confiner = cinemachineCamera.GetComponent<CinemachineConfiner2D>();
        LoadLevel(startingLevel);
    }

    public void LoadLevel(LevelConfig config)
    {
        if (config == null)
        {
            Debug.Log("No hay mas niveles. Fin del juego.");
            return;
        }

        if (currentLevelInstance != null)
            Destroy(currentLevelInstance);

        currentConfig = config;
        currentLevelInstance = Instantiate(config.tilemapPrefab);

        StartCoroutine(MovePlayerAfterLoad());
    }

    private IEnumerator MovePlayerAfterLoad()
    {
        yield return null;

        var player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("LevelManager: No se encontro objeto con tag 'Player'. " +
                             "Verifica que el jugador existe en la escena y tiene el tag asignado.");
            yield break;
        }

        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        if (Checkpoint.hasCheckpoint)
        {
            player.transform.position = Checkpoint.lastCheckpointPosition;
        }
        else
        {
            var spawn = GameObject.FindWithTag("PlayerSpawn");
            if (spawn != null)
            {
                player.transform.position = spawn.transform.position;
            }
            else
            {
                Debug.LogWarning("LevelManager: No se encontro 'PlayerSpawn' en el nivel '" +
                                 currentConfig.levelName + "'. " +
                                 "Verifica que el objeto PlayerSpawn existe dentro del Prefab " +
                                 "y tiene el tag PlayerSpawn asignado.");
            }
        }

        var cameraConfinerObject = GameObject.FindWithTag("CameraConfiner");
        if (cameraConfinerObject == null)
        {
            Debug.LogWarning("Objeto de confiner no se pudo encontrar");
        }

        var cameraConfiner = cameraConfinerObject.GetComponent<PolygonCollider2D>();
        if (confiner != null && cameraConfiner != null)
        {
            confiner.BoundingShape2D = cameraConfiner;
            confiner.InvalidateBoundingShapeCache();
        } 
        else
        {
            Debug.LogWarning("No se pudo encontrar el confiner");
        }

        Debug.Log("Nivel cargado: " + currentConfig.levelName);
    }

    public void GoToNextLevel()
    {
        LoadLevel(currentConfig.nextLevel);
    }
}