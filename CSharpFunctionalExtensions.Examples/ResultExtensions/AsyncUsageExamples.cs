using System.Threading.Tasks;

using static CSharpFunctionalExtensions.F;
using Unit = System.ValueTuple;

namespace CSharpFunctionalExtensions.Examples.ResultExtensions
{
    public class AsyncUsageExamples
    {
        /*
        public async Task<string> Promote_with_async_methods_in_the_beginning_of_the_chain(long id)
        {
            var gateway = new EmailGateway();

            return await GetByIdAsync(id)
                .ToResult("Customer with such Id is not found: " + id)
                .Ensure(customer => customer.CanBePromoted(), "The customer has the highest status possible")
                .OnSuccess(customer => customer.Promote())
                .OnSuccess(customer => gateway.SendPromotionNotification(customer.Email))
                .OnBoth(result => result.IsSuccess ? "Ok" : result.Error.Message);
        }
        */
        
        public async Task<string> Promote_with_async_methods_in_the_beginning_of_the_chain_2(long id)
        {
            var gateway = new EmailGateway();

            string response = await GetByIdAsync2(id)
                .Ensure(customer => customer.CanBePromoted(), "The customer has the highest status possible")
                .Then(customer => customer.Promote())
                .Then(customer => gateway.SendPromotionNotification(customer.Email))
                .Match(
                    customer => "Ok",
                    error => error.Message);

            return response;
        }
        
        public async Task<string> Promote_with_async_methods_in_the_middle_of_the_chain_but_not_starting_the_chain(long id)
        {
            var gateway = new EmailGateway();

            // GetById rather than GetByIdAsync
            return await GetById(id)
                .ToResult("Customer with such Id is not found: " + id)
                .Ensure(customer => customer.CanBePromoted(), "The customer has the highest status possible")
                .OnSuccess(customer => customer.PromoteAsync(), continueOnCapturedContext: false)
                .OnSuccess(customer => gateway.SendPromotionNotificationAsync(customer.Email), continueOnCapturedContext: false)
                .Match(
                    customer => "Ok",
                    error => error.Message);
        }
        
        public async Task<string> Promote_with_async_methods_in_the_middle_of_the_chain_but_not_starting_the_chain2(long id)
        {
            var gateway = new EmailGateway();

            // GetById rather than GetByIdAsync
            return await GetById(id)
                .ToResult("Customer with such Id is not found: " + id)
                .Ensure(customer => customer.CanBePromoted(), "The customer has the highest status possible")
                .OnSuccess(customer => customer.PromoteAsync(), continueOnCapturedContext: false)
                .OnSuccess(customer => gateway.SendPromotionNotificationAsync(customer.Email), continueOnCapturedContext: false)
                .Match(
                    customer => "Ok",
                    error => error.Message);
        }

        /*
        public async Task<string> Promote_with_async_methods_in_the_beginning_and_in_the_middle_of_the_chain(long id)
        {
            var gateway = new EmailGateway();

            return await GetByIdAsync(id)
                .ToResult("Customer with such Id is not found: " + id)
                .Ensure(customer => customer.CanBePromoted(), "The customer has the highest status possible")
                .OnSuccess(customer => customer.PromoteAsync())
                .OnSuccess(customer => gateway.SendPromotionNotificationAsync(customer.Email))
                .OnBoth(result => result.IsSuccess ? "Ok" : result.Error.Message);
        }*/

        public Task<Option<Customer>> GetByIdAsync(long id)
        {
            return Task.FromResult((Option<Customer>)new Customer());
        }
        
        public Task<Result<Customer>> GetByIdAsync2(long id)
        {
            var c = Task.FromResult((Option<Customer>)new Customer());
            return c.ToResult("Customer with such Id is not found: " + id);
        }

        public Option<Customer> GetById(long id)
        {
            return new Customer();
        }

        public class Customer
        {
            public string Email { get; }

            public Customer()
            {
            }

            public bool CanBePromoted()
            {
                return true;
            }

            public void Promote()
            {
            }

            public Task PromoteAsync()
            {
                return Task.FromResult(1);
            }
        }

        public class EmailGateway
        {
            public Result<Unit> SendPromotionNotification(string email)
            {
                return Success(Unit());
            }

            public Task<Result<Unit>> SendPromotionNotificationAsync(string email)
            {
                return Task.FromResult(Success(Unit()));
            }
        }
    }
}
