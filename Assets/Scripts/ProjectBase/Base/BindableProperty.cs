using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindableProperty<T> where T : IEquatable<T>
{
    private T mValue = default;
    public T value
    {
        get { return mValue; }
        set
        {
            if (!mValue.Equals(value))
            {
                mValue = value;

                OnValueChange?.Invoke(value);
            }
        }
    }

    public Action<T> OnValueChange;
}
