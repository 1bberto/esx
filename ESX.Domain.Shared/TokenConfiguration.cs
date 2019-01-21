using System;
using System.Collections.Generic;
using System.Text;

namespace ESX.Domain.Shared
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpireIn { get; set; }
        public string SigningKey { get; set; }
    }
}