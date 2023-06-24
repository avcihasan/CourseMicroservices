using Course.Services.Discount.DTOs;
using Course.Services.Discount.Entities;
using Course.Services.Discount.Services.Abstractions;
using Course.Shared.DTOs;
using Dapper;
using Npgsql;
using System.Data;
using System.Net;

namespace Course.Services.Discount.Services.Concretes
{
    public class DiscountService : IDiscountService
    {
        readonly IConfiguration _configuration;
        readonly IDbConnection _dbConnection;
        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<ResponseDto<NoContentDto>> CreateDiscountAsync(CreateDiscountDto discount)
        {
            var result = await _dbConnection.ExecuteAsync("insert into discount (userid,rate,code) values (@UserId,@Rate,@Code)", discount);
            return result > 0 ?
                ResponseDto<NoContentDto>.Success(HttpStatusCode.NoContent)
                : ResponseDto<NoContentDto>.Fail("Create discount error", HttpStatusCode.InternalServerError);
        }

        public async Task<ResponseDto<NoContentDto>> DeleteDiscountAsync(int id)
        {
            DiscountDto discountDto = (await GetDiscountByIdAsync(id)).Data;
            if (discountDto is null)
                return ResponseDto<NoContentDto>.Fail("Discount not found", HttpStatusCode.NotFound);
            var result = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });
            return result > 0 ?
                ResponseDto<NoContentDto>.Success(HttpStatusCode.NoContent)
                : ResponseDto<NoContentDto>.Fail("Delete discount error",HttpStatusCode.InternalServerError);
        }

        public async Task<ResponseDto<List<DiscountDto>>> GetAllDiscountsAsync()
            => ResponseDto<List<DiscountDto>>.Success((await _dbConnection.QueryAsync<DiscountDto>("select * from discount")).ToList(), HttpStatusCode.OK);

        public async Task<ResponseDto<DiscountDto>> GetDiscountByCodeAndUserIdAsync(string code, string userId)
        {
            DiscountDto discount = (await _dbConnection.QueryAsync<DiscountDto>("select * from discount where code=@Code and userid=@UserId", new { Code = code, UserId=userId})).FirstOrDefault();

            return discount is null ?
                ResponseDto<DiscountDto>.Fail("Discount not found", HttpStatusCode.NotFound)
                : ResponseDto<DiscountDto>.Success(discount, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<DiscountDto>> GetDiscountByIdAsync(int id)
        {
            DiscountDto discount = (await _dbConnection.QueryAsync<DiscountDto>("select * from discount where id=@Id", new { Id = id })).FirstOrDefault();

            return discount is null ?
                ResponseDto<DiscountDto>.Fail("Discount not found", HttpStatusCode.NotFound)
                : ResponseDto<DiscountDto>.Success(discount, HttpStatusCode.OK);
        }

        public async Task<ResponseDto<NoContentDto>> UpdateDiscountAsync(UpdateDiscountDto discount)
        {
            DiscountDto discountDto = (await GetDiscountByIdAsync(discount.Id)).Data;
            if (discountDto is null)
                return ResponseDto<NoContentDto>.Fail("Discount not found", HttpStatusCode.NotFound);

            var result = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id=@Id", discount);

            return
                result > 0 ?
                ResponseDto<NoContentDto>.Success(HttpStatusCode.NoContent)
                : ResponseDto<NoContentDto>.Fail("Update discount error", HttpStatusCode.InternalServerError);
        }
    }
}
