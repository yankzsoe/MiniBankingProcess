namespace Identity.Framework.Helpers {
    public class AppSettings {
        public RabbitMQ RabbitMQ { get; set; }
    }

    public record RabbitMQ {
        public string Hostname { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public string Arguments { get; set; }
        public string ConsumerTag { get; set; }
        public int Delay { get; set; }
    }
}
