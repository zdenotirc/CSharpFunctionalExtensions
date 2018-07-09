using static CSharpFunctionalExtensions.F;
using Unit = System.ValueTuple;

namespace CSharpFunctionalExtensions.Examples.ResultExtensions
{
    public class ExampleFromPluralsightCourse
    {
        /*
        public string Promote(long id)
        {
            var gateway = new EmailGateway();

            return GetById(id)
                .ToResult("Customer with such Id is not found: " + id)
                .Ensure(customer => customer.CanBePromoted(), "The customer has the highest status possible")
                .OnSuccess(customer => customer.Promote())
                .OnSuccess(customer => gateway.SendPromotionNotification(customer.Email))
                .OnBoth(result => result.IsSuccess ? "Ok" : result.Error.Message);
        }*/
        
        public string Promote(long id)
        {
            var gateway = new EmailGateway();

            return GetById(id)
                .ToResult("Customer with such Id is not found: " + id)
                .Ensure(customer => customer.CanBePromoted(), "The customer has the highest status possible")
                .Then(customer => customer.Promote())
                .Then(customer => gateway.SendPromotionNotification(customer.Email))
                .Match(
                    result => "Ok",
                    error => error.Message);
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
        }

        public class EmailGateway
        {
            public Result<Unit> SendPromotionNotification(string email)
            {
                return Success(Unit());
            }
        }
    }
}