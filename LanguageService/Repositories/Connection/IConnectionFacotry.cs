using System.Data.Common;

namespace LanguageService.Repositories.Connection;

public interface IConnectionFactory
{
    DbConnection TryConnect();
}