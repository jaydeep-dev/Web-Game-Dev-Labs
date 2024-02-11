using System.Collections.Generic;
using UnityEngine;

public class Subject<T>
{
    private List<IObserver<T>> observersList = new List<IObserver<T>>();


    public void AddObserver(IObserver<T> observer) => observersList.Add(observer);

    public void RemoveObserver(IObserver<T> observer) => observersList.Remove(observer);

    public void NotifyObservers(T t) => observersList.ForEach(ob => ob.OnNotify(t));
}
