namespace Module.Core.Systems.Pool
{
    public interface IPoolable
    {
        //Release를 사용해 Item이 풀로 반환되면 호출된다.
        void OnReturnedToPool();
        //Get을 사용해 풀에서 Item을 가져올때 호출된다.
        void OnTakeFromPool();
    }
}