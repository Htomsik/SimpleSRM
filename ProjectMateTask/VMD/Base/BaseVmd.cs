﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectMateTask.VMD.Base;

public class BaseVmd:INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    protected bool Set<T>(ref T field, T value,[CallerMemberName] string? propertyName = null)
    {
        if (Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    
    public virtual void Dispose() {}
}