namespace CSharpFunctionalExtensions
{
    public class Error
    {
        public virtual string Message { get; }

        public override string ToString() => Message;

        protected Error()
        {
        }

        internal Error(string message)
        {
            this.Message = message;

        }

        public static implicit operator Error(string m) => new Error(m);
    }
}
