namespace Core.Exceptions;

public class MissingConfigurationException(string configurationName)
    : Exception($"Missing configuration provided for {configurationName}")
{ }