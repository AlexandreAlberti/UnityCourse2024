using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectParentAbstract : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform kitchenObjectPlacePoint;

    private KitchenObject kitchenObject;

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectPlacePoint;
    }

    public void SetKichenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject;
    }

    public virtual void Interact(Player player) {
    }
}
