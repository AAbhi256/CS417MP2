using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject objectToSpawn; // 하늘에서 떨어질 프리팹 (예: 사과, 보석 등) [cite: 2026-02-26]
    [SerializeField] private float spawnInterval = 2.0f; // 생성 간격 (2초) [cite: 2026-02-26]
    public MP2Manager gameManager;
    
    private float timer = 0f; // 시간을 잴 타이머 변수 [cite: 2026-02-26]

    void Update()
    {
        // 1. 매 프레임마다 흐른 시간을 타이머에 더해줍니다. [cite: 2026-02-26]
        timer += Time.deltaTime;
        float currentSpeed = gameManager.GetCurrentRate();

        // 2. 타이머가 설정한 간격보다 커지면 물체를 생성합니다. [cite: 2026-02-26]
        if (timer >= currentSpeed)
        {
            SpawnObject();
            timer = 0f; // 생성 후 타이머를 다시 0으로 초기화합니다. [cite: 2026-02-26]
        }
    }

    void SpawnObject()
    {
        if (objectToSpawn != null)
        {
            // 현재 Spawner의 위치에서 회전값 없이 생성합니다. [cite: 2026-02-26]
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        }
    }
}