namespace IWantApp.Infra.Data;

public class QueryAllProductsSold
{
    private readonly IConfiguration configuration;

    public QueryAllProductsSold(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<ProductsSoldResponse>> Execute()
    {
        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);

        var query = @"SELECT
                        p.Id,
                        P.Name,
                        COUNT(*) Amount
                    FROM
                        Orders o INNER JOIN OrderProducts op ON o.Id = op.OrdersId
                        INNER JOIN Products p ON p.Id = op.ProductsId
                    GROUP BY
                        p.Id, p.Name
                    ORDER BY Amount DESC";

        return await db.QueryAsync<ProductsSoldResponse>(query);
    }
}