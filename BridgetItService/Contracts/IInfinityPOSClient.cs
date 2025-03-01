﻿using BridgetItService.Models;
using ShopifySharp;

namespace BridgetItService.Contracts
{
    public interface IInfinityPOSClient
    {
        Task<string> GetAuthentication();
        Task<InfinityPosProducts> GetProducts(string startDate);
        Task PutProductInInfinity(InfinityPOSProduct product);
        Task<IList<InfinityPOSProduct>> AddStock(string startDate);
    }
}
