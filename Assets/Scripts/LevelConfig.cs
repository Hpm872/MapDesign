using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig",
menuName = "DungeonLabyrinth/Level Config")]
public class LevelConfig : ScriptableObject
{
    [Header("Identificacion")]
    public string levelName;
    public int levelNumber;

    [Header("Assets del nivel")]
    public GameObject tilemapPrefab;

    [Header("Navegacion")]
    public LevelConfig nextLevel;

 }