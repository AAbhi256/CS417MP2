using UnityEngine;

public class ResourceGrower : MonoBehaviour
{
    [Header("Resource Settings")]
    [SerializeField] private float resourceAmount = 0.0f; // 현재 쌓인 자원의 양 [cite: 2026-02-26]
    [SerializeField] private float growthRate = 1.0f;    // 초당 자원 증가 속도 (Euler [cite: 2026-02-26])

    [Header("Visual Settings")]
    [SerializeField] private float sizeMultiplier = 0.1f; // 자원량에 따른 크기 증가 비율 [cite: 2026-02-26]
    [SerializeField] private Vector3 initialScale;        // 시작할 때의 원래 크기 [cite: 2026-02-26]

    void Start()
    {
        // 현재 오브젝트의 시작 크기를 저장해둡니다. [cite: 2026-02-26]
        initialScale = transform.localScale;
    }

    void Update()
    {
        // 1. 오일러 적분 방식으로 자원을 시간에 따라 누적시킵니다. [cite: 2026-02-26]
        resourceAmount += growthRate * Time.deltaTime; 

        // 2. 누적된 자원량에 비례하여 물체의 크기를 실시간으로 업데이트합니다. [cite: 2026-02-26]
        // (자원 양이 많아질수록 initialScale에 더해져서 커집니다.) [cite: 2026-02-26]
        transform.localScale = initialScale + (Vector3.one * resourceAmount * sizeMultiplier); 
    }

    // 나중에 Generator나 Power-up에서 속도를 높일 때 사용할 함수입니다. [cite: 2026-02-26]
    public void IncreaseGrowthRate(float amount)
    {
        growthRate += amount; 
    }
}