﻿using System;

namespace VisitorLog
{
    public class Settings
    {
        public string AppName { get; set; } = "DEFAULT";
        public Version AppVersion { get; set; } = new Version(0, 0, 0);
        public DatabaseSettings Database { get; set; } = new DatabaseSettings();
        public EmailSettings Email { get; set; } = new EmailSettings();
        public JWTAuthSettings Auth { get; set; } = new JWTAuthSettings();

        public class DatabaseSettings
        {
            public string Host { get; set; } = "DEFAULT";
            public int Port { get; set; }
            public string Name { get; set; } = "DEFAULT";
            public string Username { get; set; } = "DEFAULT";
            public string Password { get; set; } = "DEFAULT";
        }

        public class EmailSettings
        {
            public string Server { get; set; } = "DEFAULT";
            public int Port { get; set; }
            public string Username { get; set; } = "DEFAULT";
            public string Password { get; set; } = "DEFAULT";
            public string FromName { get; set; } = "DEFAULT";
            public string FromEmail { get; set; } = "DEFAULT";
            public int BatchSize { get; set; } = 10;
        }

        public class JWTAuthSettings
        {
            public int TokenValidityMinutes { get; set; } = 10; //default expiry for tests
            public string SigningKey { get; set; } = "ZGVmYXVsdC1zaWduaW5nLWtleQ=="; //has to be a base64 string for tests
        }
    }
}