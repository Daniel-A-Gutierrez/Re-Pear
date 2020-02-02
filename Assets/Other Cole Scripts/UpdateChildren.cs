using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderComparer : IComparer<IOrdered>
{
    public int Compare(IOrdered x, IOrdered y)
    {
        return x.GetOrder().CompareTo(y.GetOrder());
    }
}

public class UpdateChildren : MonoBehaviour
{

    private List<IOrdered> orderedComponents = new List<IOrdered>();

    void Start()
    {
        foreach (IOrdered component in gameObject.GetComponentsInChildren<IOrdered>()) {
            orderedComponents.Add(component);
        }
        orderedComponents.Sort(new OrderComparer());
    }

    void LateUpdate()
    {
        foreach (IOrdered component in orderedComponents) {
            component.OrderedUpdate();
        }
    }
}
