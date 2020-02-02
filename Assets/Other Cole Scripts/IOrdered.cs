using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOrdered
{
    int GetOrder();
    void OrderedUpdate();
}