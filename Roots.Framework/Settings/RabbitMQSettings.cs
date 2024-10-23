﻿using RabbitMQ.Client;

namespace Roots.Framework.Settings;

public class RabbitMQSettings
{
    public string HostName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}