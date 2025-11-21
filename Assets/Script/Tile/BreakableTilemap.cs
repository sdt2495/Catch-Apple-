using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class BreakableTilemap : MonoBehaviour
{
    public Tilemap tilemap;
    public PlayerStats player;
    public UpgradeUI upgradeUI; // 宝箱用UI

    // 各座標ごとの現在HP
    private Dictionary<Vector3Int, int> currentHP = new Dictionary<Vector3Int, int>();

    public void DamageTile(Vector3Int pos, int damage)
    {
        if (!tilemap.HasTile(pos)) return;

        BlockTile tile = tilemap.GetTile<BlockTile>(pos);
        if (tile == null) return;

        BlockData data = tile.blockData;
        if (data == null) return;

        // 初回攻撃ならHPセット
        if (!currentHP.ContainsKey(pos))
            currentHP[pos] = data.hp;

        currentHP[pos] -= damage;

        // 破壊判定
        if (currentHP[pos] <= 0)
        {
            tilemap.SetTile(pos, null);
            currentHP.Remove(pos);

            // XP付与
            player.AddXP(data.xp);

            // 宝箱ならイベント発動
            if (data.type == BlockType.Chest)
            {
                if (upgradeUI != null)
                {
                    upgradeUI.Open();
                }
            }
        }
    }
}
