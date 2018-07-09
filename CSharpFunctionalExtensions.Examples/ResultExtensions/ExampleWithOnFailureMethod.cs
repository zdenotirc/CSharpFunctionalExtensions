using System.Threading.Tasks;
using static CSharpFunctionalExtensions.F;
using Unit = System.ValueTuple;

namespace CSharpFunctionalExtensions.Examples.ResultExtensions
{
    public class ExampleWithOnFailureMethod
    {
        /*
        public string OnFailure_non_async(int customerId, decimal moneyAmount)
        {
            var paymentGateway = new PaymentGateway();
            var database = new Database();

            return GetById(customerId)
                .ToResult("Customer with such Id is not found: " + customerId)
                .OnSuccess(customer => customer.AddBalance(moneyAmount))
                .OnSuccess(customer => paymentGateway.ChargePayment(customer, moneyAmount).Map(() => customer))
                .OnSuccess(
                    customer => database.Save(customer)
                        .OnFailure(() => paymentGateway.RollbackLastTransaction()))
                .OnBoth(result => result.IsSuccess ? "OK" : result.Error.Message);
        }*/
        
        public string OnFailure_non_async(int customerId, decimal moneyAmount)
        {
            var paymentGateway = new PaymentGateway();
            var database = new Database();

            return GetById(customerId)
                .ToResult("Customer with such Id is not found: " + customerId)
                .Then(customer => customer.AddBalance(moneyAmount))
                .Then(customer => paymentGateway.ChargePayment(customer, moneyAmount).Map((c) => customer))
                .Then(customer => database.Save(customer)
                        .OnFailure(() => paymentGateway.RollbackLastTransaction()))
                .Match(
                    result => "Ok",
                    error => error.Message);
        }

        private Option<Customer> GetById(long id)
        {
            return new Customer();
        }

        private class Customer
        {
            public void AddBalance(decimal moneyAmount)
            {
                
            }
        }

        private class PaymentGateway
        {
            public Result<Unit> ChargePayment(Customer customer, decimal moneyAmount)
            {
                return Success(Unit());
            }

            public void RollbackLastTransaction()
            {
                
            }

            public Task RollbackLastTransactionAsync()
            {
                return Task.FromResult(1);
            }
        }

        private class Database
        {
            public Result<Unit> Save(Customer customer)
            {
                return Success(Unit());
            }
        }
    }
}
