using System;
using System.Collections.Generic;
using System.Text;

namespace jamesMont.Model
{
    public static class Configuration
    {
        /// <summary>
        /// Azure Storage Connection String. UseDevelopmentStorage=true points to the storage emulator.
        /// “Only use Shared Key authentication for testing purposes! Your account name and account key, which give full read/write access to the associated Storage account, 
        /// will be distributed to every person that downloads your app. This is not a good practice as you risk having your key compromised by untrusted clients. 
        /// Please consult following documents to understand and use Shared Access Signatures instead. 
        /// https://docs.microsoft.com/en-us/rest/api/storageservices/delegating-access-with-a-shared-access-signature
        /// https://docs.microsoft.com/en-us/azure/storage/common/storage-dotnet-shared-access-signature-part-1
        /// </summary>
        public const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=commblob2;AccountKey=fU7XsTmlYv6VgnFvYlPxWEcT8KBAineKA5JO+iMoBzAo0x5cB0ELj/m5clUl8X8OhrsVoYXCo4ELyhillPyPIA==;EndpointSuffix=core.windows.net";
    }
}
