using UnityEngine;

public class SpawnBonuses : MonoBehaviour
{
    public GameObject[] bonuses;

    public void SpawnBonus(Vector2 position, bool isMake)
    {
        /*
        if (GameManager.instance.curLevel <= 5)
            return;
        */
        
        Debug.Log(isMake);

        if (!isMake)
        {
            GameManager.instance.isPosibleSpawnBonuse = true;
            int random = Random.Range(0, bonuses.Length);
            GameObject bonus = Instantiate(bonuses[random], position, Quaternion.identity);
        }
        

    }
}
