
using System;

public interface ITrigger
{
    void Invoke();
    bool isCompleted { get; set; }
}