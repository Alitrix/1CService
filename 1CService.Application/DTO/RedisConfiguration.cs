﻿namespace _1CService.Application.DTO
{
    public class RedisConfiguration
    {
        public int TimeSetRequestAddRole { get; set; }
        public int TimeSetPreRegistrationUser { get; set; }

        public TimeSpan GetTimeRequestAddRight() => TimeSpan.FromMinutes(TimeSetRequestAddRole);
        public TimeSpan GetTimePreRegistrationUser() => TimeSpan.FromMinutes(TimeSetPreRegistrationUser);
    }
}
