using UnityEngine;

public class TileGetter : MonoBehaviour {
    public Transform GetTile(int row, int column) {
        return transform.GetChild(row).GetChild(column);
    }
}
