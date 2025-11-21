using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class DigController : MonoBehaviour
{
    [Header("Tilemaps")]
    public Tilemap groundTilemap;    // 土マップ
    public Tilemap treasureTilemap;  // 宝マップ
    public Tilemap pathTilemap;      // 道マップ

    [Header("Tiles")]
    public Tile groundTile;          // 土
    public Tile pathTile;            // 道
    public Tile[] treasureTiles;     // 宝マップ用タイル: 0=未使用,1=鉱石A,2=鉱石B,3=鉱石C,4=宝

    [Header("CSV Files (Resourcesフォルダ)")]
    public string[] csvFileNames = { "土１", "宝マップ", "道１" };

    // CSVの値を格納
    public Dictionary<string, int[,]> mapData = new Dictionary<string, int[,]>();

    private void Start()
    {
        LoadAllCSVs();
        DrawAllMaps();
    }

    /// <summary>
    /// CSVを全て読み込む
    /// </summary>
    void LoadAllCSVs()
    {
        foreach (var name in csvFileNames)
        {
            TextAsset csvFile = Resources.Load<TextAsset>(name);
            if (csvFile == null)
            {
                Debug.LogError($"CSV file not found in Resources: {name}");
                continue;
            }

            string[] lines = csvFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
            int height = lines.Length;
            int width = lines[0].Split(',').Length;
            int[,] data = new int[height, width];

            for (int y = 0; y < height; y++)
            {
                string[] cells = lines[y].Split(',');
                for (int x = 0; x < width; x++)
                {
                    if (int.TryParse(cells[x], out int value))
                        data[y, x] = value;
                    else
                        data[y, x] = 0;
                }
            }

            mapData[name] = data;
        }
    }

    /// <summary>
    /// 全てのマップを Tilemap に描画
    /// </summary>
    void DrawAllMaps()
    {
        foreach (var kvp in mapData)
        {
            string name = kvp.Key;
            int[,] data = kvp.Value;
            int height = data.GetLength(0);
            int width = data.GetLength(1);

            Tilemap targetTilemap = GetTilemapForName(name);
            if (targetTilemap == null) continue;

            targetTilemap.ClearAllTiles();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector3Int cell = new Vector3Int(x, -y, 0); // Y軸反転

                    if (data[y, x] == 0) continue;

                    if (name == "宝マップ")
                    {
                        Tile tile = GetTreasureTile(data[y, x]);
                        if (tile != null)
                            targetTilemap.SetTile(cell, tile);
                    }
                    else
                    {
                        Tile tile = GetTileForName(name);
                        if (tile != null)
                            targetTilemap.SetTile(cell, tile);
                    }
                }
            }
        }
    }

    /// <summary>
    /// CSV名に対応するTilemap
    /// </summary>
    Tilemap GetTilemapForName(string name)
    {
        switch (name)
        {
            case "土１": return groundTilemap;
            case "宝マップ": return treasureTilemap;
            case "道１": return pathTilemap;
            default: return null;
        }
    }

    /// <summary>
    /// 土や道用タイル
    /// </summary>
    Tile GetTileForName(string name)
    {
        switch (name)
        {
            case "土１": return groundTile;
            case "道１": return pathTile;
            default: return null;
        }
    }

    /// <summary>
    /// 宝マップ用のタイル取得
    /// </summary>
    Tile GetTreasureTile(int value)
    {
        if (value > 0 && value < treasureTiles.Length)
            return treasureTiles[value];
        return null;
    }

    /// <summary>
    /// 掘る処理（例として土マップ）
    /// </summary>
    public void Dig(Vector3 worldPos)
    {
        Vector3Int cell = groundTilemap.WorldToCell(worldPos);
        int x = cell.x;
        int y = -cell.y;

        if (!mapData.ContainsKey("土１")) return;

        int[,] groundData = mapData["土１"];
        int height = groundData.GetLength(0);
        int width = groundData.GetLength(1);

        if (x < 0 || x >= width || y < 0 || y >= height) return;
        if (groundData[y, x] == 0) return;

        // 土を消して道タイルに置き換え
        groundTilemap.SetTile(cell, null);
        pathTilemap.SetTile(cell, pathTile);
        groundData[y, x] = 0;

        Debug.Log($"Dig success! x={x}, y={y}");
    }

    /// <summary>
    /// 掘る処理（宝マップ用）
    /// CSVの値によってアイテム取得など処理可能
    /// </summary>
    public void DigTreasure(Vector3 worldPos)
    {
        if (!mapData.ContainsKey("宝マップ")) return;

        Vector3Int cell = treasureTilemap.WorldToCell(worldPos);
        int x = cell.x;
        int y = -cell.y;

        int[,] treasureData = mapData["宝マップ"];
        int height = treasureData.GetLength(0);
        int width = treasureData.GetLength(1);

        if (x < 0 || x >= width || y < 0 || y >= height) return;
        int value = treasureData[y, x];
        if (value == 0) return;

        // タイルを消す
        treasureTilemap.SetTile(cell, null);
        treasureData[y, x] = 0;

        // ここで value に応じて鉱石や宝の処理を追加可能
        Debug.Log($"Treasure collected! value={value} x={x} y={y}");
    }
}
