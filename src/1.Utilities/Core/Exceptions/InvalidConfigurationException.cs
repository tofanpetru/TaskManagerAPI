namespace Core.Exceptions;

public class InvalidConfigurationException(string configurationName)
    : Exception($"Invalid configuration provided for {configurationName}")
{ }