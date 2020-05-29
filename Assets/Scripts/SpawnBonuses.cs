using UnityEngine;

public class SpawnBonuses : MonoBehaviour
{
    public GameObject[] bonuses;

    public void SpawnBonus(Vector2 position)
    {
        if (GameManager.instance.curLevel < 3)
            return;

        int random = Random.Range(0, bonuses.Length);
        GameObject bonus = Instantiate(bonuses[random], position, Quaternion.identity);
    }
}
