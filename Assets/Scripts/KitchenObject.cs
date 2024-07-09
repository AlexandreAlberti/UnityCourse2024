using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent parent) {

        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = parent;

        if (this.kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("Already asigning to a counter that has an object");
        }

        this.kitchenObjectParent.SetKichenObject(this);
        transform.parent = parent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return this.kitchenObjectParent;
    }

    public void DestroySelf() {
        this.kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, KitchenObjectParentAbstract parent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, parent.transform);
        kitchenObjectTransform.localPosition = Vector3.zero;
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(parent);
        return kitchenObject;
    }
}
