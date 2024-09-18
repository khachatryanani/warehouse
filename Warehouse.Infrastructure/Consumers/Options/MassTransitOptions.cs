namespace Warehouse.Infrastructure.Consumers.Options
{
    public class MassTransitOptions
    {
        public const string Section = nameof(MassTransitOptions);
        public string Host { get; set; }
        public string VirtualHost { get; set; }
        public ushort Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
