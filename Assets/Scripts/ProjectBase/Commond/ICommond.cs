using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommond
{
    void Execute();

    void Execute<T>(T value);
}

public struct AddCommond : ICommond
{
    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Execute<T>(T value)
    {
        throw new System.NotImplementedException();
    }
}

public struct SubCommond : ICommond
{
    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Execute<T>(T value)
    {
        throw new System.NotImplementedException();
    }
}
