using UnityEngine;

public enum BlockType
{
    Ore,        // zÎ
    Chest       // •ó” 
}

[CreateAssetMenu(menuName = "Block/BlockData")]
public class BlockData : ScriptableObject
{
    public Sprite sprite;
    public int hp;
    public int xp;
    public BlockType type;
}
