using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : KitchenObjectParentAbstract {

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    [SerializeField] private float plateSpawnTimeFrequency;
    [SerializeField] private float maxPlatesInPileCount;

    public event EventHandler<int> OnPlatesAmountChange;

    private float plateSpawnTimer;
    private int platesSpawned;

    private void Start() {
        plateSpawnTimer = 0.0f;
        platesSpawned = 0;
    }
    private void Update() {
        if (platesSpawned < maxPlatesInPileCount) {

            plateSpawnTimer += Time.deltaTime;

            if (plateSpawnTimeFrequency <= plateSpawnTimer) {
                plateSpawnTimer -= plateSpawnTimeFrequency;
                ++platesSpawned;
                OnPlatesAmountChange?.Invoke(this, platesSpawned);
            }
        }
    }

    public override void Interact(Player player) {

        if (!player.HasKitchenObject() && platesSpawned > 0) {
            // Spawn new KO
            KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, this);
            kitchenObject.SetKitchenObjectParent(player);
            --platesSpawned;
            OnPlatesAmountChange?.Invoke(this, platesSpawned);
        }
    }

}
