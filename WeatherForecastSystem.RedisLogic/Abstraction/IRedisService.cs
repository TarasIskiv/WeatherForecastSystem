namespace WeatherForecastSystem.RedisLogic.Abstraction;

public interface IRedisService
{
    Task SetData<T>(string key, T data);
    Task<T> GetData<T>(string key);
    string GetKey(string cityName);
}