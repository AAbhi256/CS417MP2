using UnityEngine;

public class MP2Manager : MonoBehaviour
{
    public float baseGrowthRate = 5.0f;     // 기본 생성 속도 [cite: 2026-02-26]
    public float boostedGrowthRate = 3.0f;  // 레이가 닿았을 때의 속도 [cite: 2026-02-26]
    public float medicineAmount = 0.0f;
    
    private bool isHovered = false;         // 현재 레이가 닿아 있는지 확인 [cite: 2026-02-26]

    void Update()
    {
        // 1. 현재 상태에 따라 적용할 속도를 결정합니다. [cite: 2026-02-26]
        float currentRate = isHovered ? boostedGrowthRate : baseGrowthRate; 

        // 2. 오일러 적분으로 약(자원)을 누적시킵니다. [cite: 2026-02-26]
        medicineAmount += currentRate * Time.deltaTime; 
    }

    // [중요] XR Ray Interactor의 Hover 이벤트에서 호출할 함수들입니다. [cite: 2026-02-26]
    public void OnRayEnter()
    {
        isHovered = true; 
        Debug.Log("Protection detected. Cure accelerated.");
    }  // 레이가 닿았을 때 [cite: 2026-02-26]
    public void OnRayExit() { isHovered = false; } // 레이가 떨어졌을 때 [cite: 2026-02-26]
    
    public float GetCurrentRate()
    {
        // 레이가 닿아있으면(isHovered가 true면) boostedGrowthRate를, [cite: 2026-02-26]
        // 아니면 기본 속도인 baseGrowthRate를 돌려줍니다. [cite: 2026-02-26]
        if (isHovered)
        {
            return boostedGrowthRate; 
        }
        else
        {
            return baseGrowthRate; 
        }
    }
}