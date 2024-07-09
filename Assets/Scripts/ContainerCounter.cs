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
        }
    }
}
