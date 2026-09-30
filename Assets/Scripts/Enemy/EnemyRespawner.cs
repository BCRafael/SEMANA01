using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private Player player;
    private int quantityCoin = 0;

    void Update()
    {
        if (quantityCoin < player.coins)
        {
            CreateNewEnemy();
            quantityCoin = quantityCoin + 1;
        }
    }

    public void CreateNewEnemy()
    {
        int respawnPointIndex = Random.Range(0, respawnPoints.Length);
        
        GameObject newEnemy = Instantiate(enemyPrefab, respawnPoints[respawnPointIndex].position, Quaternion.identity);

        newEnemy.GetComponent<Enemy>().player = this.player;
    }

}