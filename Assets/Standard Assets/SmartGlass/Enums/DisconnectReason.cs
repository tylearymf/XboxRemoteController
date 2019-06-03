namespace SmartGlass
{
    /// <summary>
    /// Disconnect reason.
    /// </summary>
    public enum DisconnectReason
    {
        Unspecified,
        Error,
        PowerOff,
        Maintenance,
        AppClose,
        SignOut,
        Reboot,
        Disabled,
        LowPower
    }
}
