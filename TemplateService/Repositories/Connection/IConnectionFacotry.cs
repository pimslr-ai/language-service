using System.Data.Common;

namespace TemplateService.Repositories.Connection;

public interface IConnectionFactory
{
    DbConnection TryConnect();
}