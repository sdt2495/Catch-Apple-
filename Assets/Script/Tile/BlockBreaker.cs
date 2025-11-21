using UnityEngine;

public class BlockBreaker : MonoBehaviour
{
    public Camera cam;
    public BreakableTilemap breakable;
    public PlayerStats player;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 wp = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cell = breakable.tilemap.WorldToCell(wp);

            breakable.DamageTile(cell, player.digDamage);
        }
    }
}
