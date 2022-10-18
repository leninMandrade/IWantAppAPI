namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
	private readonly IConfiguration configuration;
	
	public QueryAllUsersWithClaimName(IConfiguration configuration)
	{
		this.configuration = configuration;
	}

	public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows)
	{
        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);

        var query = @"SELECT Email, ClaimValue as Name
            from AspNetUsers u INNER JOIN AspNetUserClaims c
            on u.id = c.UserId and ClaimType = 'Name'
            order by Name
            OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        return await db.QueryAsync<EmployeeResponse>(query, new { page, rows });
    }
}
