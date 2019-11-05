public interface IBaseGameEventListener<T> 
{
    void OnEventRaised(T data);
}
