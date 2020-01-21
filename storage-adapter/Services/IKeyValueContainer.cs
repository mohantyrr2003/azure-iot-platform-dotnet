using System.Collections.Generic;
using System.Threading.Tasks;
using Mmm.Platform.IoT.Common.Services;
using Mmm.Platform.IoT.StorageAdapter.Services.Models;

namespace Mmm.Platform.IoT.StorageAdapter.Services
{
    /// <summary>
    /// Common interface for underlying key-value storage services, such as Cosmos DB, Azure Storage Table and so on
    /// </summary>
    public interface IKeyValueContainer : IStatusOperation
    {
        string DocumentDataType { get; }
        string DocumentDatabaseSuffix { get; }
        string TenantId { get; }
        string DocumentDbDatabaseId { get; }
        string DocumentDbCollectionId { get; }

        /// <summary>
        /// Get single key-value pair
        /// </summary>
        /// <param name="collectionId">Collection ID</param>
        /// <param name="key">Key</param>
        /// <returns>Key-value pair including key, data, etag and timestamp</returns>
        Task<ValueServiceModel> GetAsync(string collectionId, string key);

        /// <summary>
        /// Get all key-value pairs in given collection
        /// </summary>
        /// <param name="collectionId">Collection ID</param>
        /// <returns>List of key-value pairs</returns>
        Task<IEnumerable<ValueServiceModel>> GetAllAsync(string collectionId);

        /// <summary>
        /// Create key-value pair
        /// </summary>
        /// <param name="collectionId">Collection ID</param>
        /// <param name="key">Key</param>
        /// <param name="input">Data</param>
        /// <returns>Created key-value pair</returns>
        Task<ValueServiceModel> CreateAsync(string collectionId, string key, ValueServiceModel input);

        /// <summary>
        /// Update key-value pair (create if pair does not exist)
        /// </summary>
        /// <param name="collectionId">Collection ID</param>
        /// <param name="key">Key</param>
        /// <param name="input">Data plus etag</param>
        /// <returns>Updated key-value pair</returns>
        Task<ValueServiceModel> UpsertAsync(string collectionId, string key, ValueServiceModel input);

        /// <summary>
        /// Delete key-value pair
        /// </summary>
        /// <param name="collectionId">Collection ID</param>
        /// <param name="key">Key</param>
        /// <returns></returns>
        Task DeleteAsync(string collectionId, string key);
    }
}
