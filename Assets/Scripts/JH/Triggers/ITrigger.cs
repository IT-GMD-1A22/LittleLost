
/*
 * Interface to handle action when a trigger is being triggerd
 */

public interface ITrigger
{
    void Invoke();
    bool isCompleted { get; set; }
}