using Dapper;
using DIDapperAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace DIDapperAPI.service
{

    public class MovieService
    {
        //public readonly string _connectString = @"Server=localhost;Database=work1;Trusted_Connection=True;TrustServerCertificate=true;User ID=sa;Password=Tuz8l4z!;Integrated Security=False;";
        private readonly Conn _conn;

        public MovieService(Conn conn)
        {
            _conn = conn;
        }

        [HttpGet(Name = "Movie")]
        public IEnumerable<Movie> GetList()
        {
            var sql = "SELECT Id,Name,CreateDateTime,UpdateTime FROM Movie";

            using (var conn = new SqlConnection(_conn.ConnectionString))
            {
                var result = conn.Query<Movie>(sql);
                return result;
            }
        }
        //查詢資料Detail
        public Movie Get(int id)
        {
            var sql =
            @"
                SELECT TOP 1 Id,Name,CreateDateTime,UpdateTime
                FROM Movie
                WHERE Id = @id
            ";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Int32);
            using (var conn = new SqlConnection(_conn.ConnectionString))
            {
                var result = conn.QueryFirstOrDefault<Movie>(sql, parameters);
                return result;
            }
        }
        //新增資料
        public int Create(Movie parameter)
        {
            var sql =
            @"
                INSERT INTO Movie
                (
                    [Name]
                   ,[CreateDateTime]
                   ,[UpdateTime]
                )
                VALUES
                (
                    @Name
                   ,@CreateDateTime
                   ,@UpdateTime
                );

                SELECT @@IDENTITY
            ";

            using (var conn = new SqlConnection(_conn.ConnectionString))
            {
                var result = conn.QueryFirstOrDefault<int>(sql, parameter);
                return result;
            }
        }
        //修改資料
       public bool Update(int id, Movie parameter)
        {
            var sql =
            @"
                UPDATE Movie
                SET
                    [Name] = @Name
                   ,[CreateDateTime] = @CreateDateTime
                   ,[UpdateTime] = @UpdateTime
                WHERE
                    Id = @id
            ";

            var parameters = new DynamicParameters(parameter);
            parameters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_conn.ConnectionString))
            {
                var result = conn.Execute(sql, parameters);
                return result > 0;
            }
        }
        //刪除資料
        public void Delete(int id)
        {
            var sql =
            @"
                DELETE FROM Movie
                WHERE Id = @id
            ";

            var paramters = new DynamicParameters();
            paramters.Add("Id", id, System.Data.DbType.Int32);

            using (var conn = new SqlConnection(_conn.ConnectionString))
            {
                var result = conn.Execute(sql, paramters);
            }
        }

    }
}
