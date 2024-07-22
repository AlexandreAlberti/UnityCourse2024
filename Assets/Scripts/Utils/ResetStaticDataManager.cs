using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    void Awake()
    {
        Player.ResetStaticData();
        TrashCounter.ResetStaticData();
        CuttingCounter.ResetStaticData();
        ClearCounter.ResetStaticData();
    }
}
