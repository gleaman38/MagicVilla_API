namespace MagicVilla_VillaAPI.Logging
{
    //comments done
    //interface for logging, can be implemented by any class that wants to provide logging functionality, such as Logging class
    public interface ILogging
    {
        public void Log(string message, string type);
    }
}
