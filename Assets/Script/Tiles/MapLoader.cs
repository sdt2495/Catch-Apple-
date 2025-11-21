using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapLoader : MonoBehaviour
{
    [Header("Tilemaps")]
    public Tilemap groundTilemap;   // 地形用
    public Tile groundTile;         // 数字1に対応する地形タイル

    [Header("CSV")]
    public TextAsset groundCSV;

    void Start()
    {
        LoadGround();
    }

    void LoadGround()
    {
        string[] lines = groundCSV.text.Split('\n');

        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y].Trim();

            // 空行をスキップ（FormatException防止）
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // カンマ or タブ両対応！
            string[] cells = line.Split(new char[] { ',', '\t' },
                System.StringSplitOptions.RemoveEmptyEntries);

            for (int x = 0; x < cells.Length; x++)
            {
                // 数値でないものはスキップ（これも安全）
                if (!int.TryParse(cells[x], out int val))
                    continue;

                if (val == 1)
                {
                    groundTilemap.SetTile(new Vector3Int(x, -y, 0), groundTile);
                }
            }
        }
    }
}
