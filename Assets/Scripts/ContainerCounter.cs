using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : KitchenObjectParentAbstract {

    [SerializeField] private KitchenObjectSO itemPrefab;
    [SerializeField] private SpriteRenderer[] itemSpritesOnDoor;

    public event EventHandler OnPlayerGrabbedObject;

    private void Start() {
        foreach (SpriteRenderer itemSpriteOnDoor in itemSpritesOnDoor) {
            itemSpriteOnDoor.sprite = itemPrefab.sprite;
        }
    }

    public override void Interact(Player player) {

        if (!player.HasKitchenObject()) {
            // Spawn new KO
            KitchenObject kitchenObject = KitchenObject.SpawnKitchenObject(itemPrefab, this);
            kitchenObject.SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);

        } else if (player.HasKitchenObject() && player.GetKitchenObject() is PlateKitchenObject plateKitchenObject) {
            // Grab by player in a plate if we can (raw item is ok only if in list)
            if (plateKitchenObject.TryAddIngredient(itemPrefab)) {
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
